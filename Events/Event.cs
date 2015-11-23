using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Events
{
    public static class Event
    {
        public static IDisposable Subscribe<T>(Func<T, Task<bool>> handler, bool weakReference = false)
        {
            Contract.Requires<ArgumentNullException>(handler != null);
            Contract.Ensures(Contract.Result<IDisposable>() != null);

            if (weakReference)
                return Subscribe(new WeakHandler<T>(handler));
            else
                return Subscribe(new Handler<T>(handler));
        }

        public static IDisposable Subscribe<T>(IWeakHandler<T> handler)
        {
            Contract.Requires<ArgumentNullException>(handler != null);
            Contract.Ensures(Contract.Result<IDisposable>() != null);

            return new WeakSubscription<T>(handler);
        }

        public static IDisposable Subscribe<T>(IHandler<T> handler)
        {
            Contract.Requires<ArgumentNullException>(handler != null);
            Contract.Ensures(Contract.Result<IDisposable>() != null);

            return new Subscription<T>(handler);
        }

        [DebuggerHidden]
        public static IEventScope Start<E>(this E e)
        {
            Contract.Requires<ArgumentNullException>(e != null);
            Contract.Ensures(Contract.Result<IEventScope>() != null);
            return new EventScope<E>(e);
        }

        class EventScope<E> : IEventScope
        {
            public EventScope(E e)
            {
                Contract.Requires<ArgumentNullException>(e != null);
                Contract.Ensures(Event != null);
                Event = e;
            }

            E Event { get; }
            HashSet<Exception> Exceptions { get; } = new HashSet<Exception>();

            public async void Dispose()
            {
                Exception[] exceptions;
                lock(Exceptions)
                    exceptions = Exceptions.ToArray();

                switch (exceptions.Length)
                {
                    case 0:
                        await Subscription.NotifyAsync(new Notification<E, Succeeded>(Event));
                        break;
                        
                    case 1:
                        await Subscription.NotifyAsync(new Notification<E, Succeeded>(Event, exceptions[0]));
                        break;

                    default:
                        await Subscription.NotifyAsync(new Notification<E, Succeeded>(Event, new AggregateException(exceptions)));
                        break;
                };
            }

            public void Fail(Exception ex)
            {
                lock(Exceptions)
                    Exceptions.Add(ex);
            }
        }


        [DebuggerHidden]
        public static async Task SendAsync<T>(this T e)
        {
            Contract.Requires<ArgumentNullException>(e != null);
            Contract.Ensures(Contract.Result<Task>() != null);

            if (!await RaiseAsync(e))
                throw new NotImplementedException("Required " + typeof(T).Name + " event handler is not registered.");
        }

        [DebuggerHidden]
        public static async Task SendAsync<T, EX>(this T e, EX ex)
            where EX : Exception
        {
            Contract.Requires<ArgumentNullException>(e != null);
            Contract.Requires<ArgumentNullException>(ex != null);
            Contract.Ensures(Contract.Result<Task>() != null);

            if (!await RaiseAsync(e))
                throw new NotImplementedException("Required " + typeof(T).Name + " event handler is not registered.");
        }

        [DebuggerHidden]
        public async static Task<bool> RaiseAsync<T, EX>(this T e, EX ex)
            where EX : Exception
        {
            Contract.Requires<ArgumentNullException>(e != null);
            Contract.Requires<ArgumentNullException>(ex != null);
            Contract.Ensures(Contract.Result<Task<bool>>() != null);

            return await Subscription.NotifyAsync(new Notification<T, Failed>(e, ex));
        }

        [DebuggerHidden]
        public async static Task<bool> RaiseAsync<T>(this T e)
        {
            Contract.Requires<ArgumentNullException>(e != null);
            Contract.Ensures(Contract.Result<Task<bool>>() != null);

            try
            {
                if(await Subscription.NotifyAsync(e))
                {
                    await Subscription.NotifyAsync(new Notification<T, Succeeded>(e));                    
                    return true;
                }
                else
                {
                    await Subscription.NotifyAsync(new Notification<T, Unhandled>(e));                    
                    return false;
                }
            }
            catch(Exception ex)
            {
                await Subscription.NotifyAsync(new Notification<T, Failed>(e, ex));
                throw;
            }
        }

        abstract class Subscription : IDisposable
        {
            static ReaderWriterLockSlim Lock { get; } = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            static HashSet<Subscription> Instances { get; } = new HashSet<Subscription>();

            protected Subscription()
            {
                Lock.EnterWriteLock();
                try
                {
                    Instances.Add(this);
                }
                finally
                {
                    Lock.ExitWriteLock();
                }
            }

            public void Dispose()
            {
                Lock.EnterWriteLock();
                try
                {
                    Instances.Remove(this);
                }
                finally
                {
                    Lock.ExitWriteLock();
                }
            }

            [DebuggerHidden]
            public static async Task<bool> NotifyAsync(object e)
            {
                Contract.Requires<ArgumentNullException>(e != null);
                Contract.Ensures(Contract.Result<Task<bool>>() != null);

                Subscription[] instances;
                Lock.EnterReadLock();                
                try
                {
                    instances = Instances.ToArray();                    
                }
                finally
                {
                    Lock.ExitReadLock();
                }

                var matches = Task.WhenAll(from s in instances
                                           select s.NotifyCoreAsync(e));

                return (await matches)
                    .Contains(true);
            }

            protected abstract Task<bool> NotifyCoreAsync(object e);
        }

        class Subscription<T> : Subscription
        {
            readonly IHandler<T> _handler;

            public Subscription(IHandler<T> handler)
            {
                Contract.Requires<ArgumentNullException>(handler != null);
                Contract.Ensures(_handler != null);
                _handler = handler;
            }

            protected override Task<bool> NotifyCoreAsync(object e)
            {
                if (e is T)
                    return _handler.HandleAsync((T)e);
                else
                    return Task.FromResult(false);
            }
        }

        class WeakSubscription<T> : Subscription
        {
            readonly WeakReference<IWeakHandler<T>> _reference;

            public WeakSubscription(IWeakHandler<T> handler)
            {
                Contract.Requires<ArgumentNullException>(handler != null);
                Contract.Ensures(_reference != null);

                _reference = new WeakReference<IWeakHandler<T>>(handler);
            }

            protected override Task<bool> NotifyCoreAsync(object e)
            {
                if (e is T)
                {
                    IWeakHandler<T> handler;
                    if (_reference.TryGetTarget(out handler))
                        return handler.HandleAsync((T)e);
                    
                    Dispose();
                }
                
                return Task.FromResult(false);
            }
        }
    }
}
