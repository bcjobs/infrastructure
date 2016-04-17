using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Events
{
    public interface IReply<out TOriginal, out TSubject>
    {
        TOriginal Original { get; }
        TSubject Subject { get; }
    }

    class Reply<TOriginal, TSubject> : IReply<TOriginal, TSubject>
    {
        public Reply(TOriginal original, TSubject subject)
        {
            Original = original;
            Subject = subject;
        }

        public TOriginal Original { get; }
        public TSubject Subject { get; }
    }

    public static class ReplyHelper
    {
        public async static Task<bool> ReplyAsync<TOriginal, TSubject>(this TOriginal original, TSubject subject)
        {
            return await new Reply<TOriginal, TSubject>(original, subject)
                .RaiseAsync();
        }

        public static TReply Request<TReply>(this object original)
        {
            return Task.Run(async () => await original.RequestAsync<TReply>()).Result;
        }

        [DebuggerHidden]
        public static async Task<TReply> RequestAsync<TReply>(this object original)
        {
            var sync = new Object();
            var reply = default(TReply);
            Exception ex = new MissingReplyException(original.GetType(), typeof(TReply));
            await original.RequestAsync(
                async (TReply e) =>
                {
                    lock (sync)
                        if (ex is MissingReplyException)
                        {
                            ex = null;
                            reply = e;
                        }
                        else
                            ex = new TooManyRepliesException(original.GetType(), typeof(TReply));

                    return true;
                });

            if (ex == null)
                return reply;
            else
                throw ex;

        }

        public static async Task RequestAsync<TOriginal, T1>(this TOriginal original, 
            Func<T1, Task<bool>> h1)
        {
            using (Event.Subscribe(new ReplayFilter<TOriginal, T1>(original, h1)))
                await original.SendAsync();
        }
        
        public static async Task RequestAsync<TOriginal, T1, T2>(this TOriginal original, 
            Func<T1, Task<bool>> h1, 
            Func<T2, Task<bool>> h2)
        {
            using (Event.Subscribe(new ReplayFilter<TOriginal, T1>(original, h1)))
            using (Event.Subscribe(new ReplayFilter<TOriginal, T2>(original, h2)))
                await original.SendAsync();
        }

        public static async Task RequestAsync<TOriginal, T1, T2, T3>(this TOriginal original, 
            Func<T1, Task<bool>> h1, 
            Func<T2, Task<bool>> h2, 
            Func<T3, Task<bool>> h3)
        {
            using (Event.Subscribe(new ReplayFilter<TOriginal, T1>(original, h1)))
            using (Event.Subscribe(new ReplayFilter<TOriginal, T2>(original, h2)))
            using (Event.Subscribe(new ReplayFilter<TOriginal, T3>(original, h3)))
                await original.SendAsync();
        }

        public static async Task RequestAsync<TOriginal, T1, T2, T3, T4>(this TOriginal original, 
            Func<T1, Task<bool>> h1, 
            Func<T2, Task<bool>> h2, 
            Func<T3, Task<bool>> h3, 
            Func<T4, Task<bool>> h4)
        {
            using (Event.Subscribe(new ReplayFilter<TOriginal, T1>(original, h1)))
            using (Event.Subscribe(new ReplayFilter<TOriginal, T2>(original, h2)))
            using (Event.Subscribe(new ReplayFilter<TOriginal, T3>(original, h3)))
            using (Event.Subscribe(new ReplayFilter<TOriginal, T4>(original, h4)))
                await original.SendAsync();
        }
    }

    class ReplayFilter<TOriginal, TSubject> : IHandler<IReply<TOriginal, TSubject>>
    {
        public ReplayFilter(TOriginal original, Func<TSubject, Task<bool>> handler)
        {
            Original = original;
            Handler = handler;
        }

        TOriginal Original { get; }
        Func<TSubject, Task<bool>> Handler { get; }

        public async Task<bool> HandleAsync(IReply<TOriginal, TSubject> e)
        {
            if (e.Original.Equals(Original))
                return await Handler(e.Subject);

            return false;
        }
    }


    [Serializable]
    public class TooManyRepliesException : Exception
    {
        protected TooManyRepliesException()
        {
        }

        public TooManyRepliesException(Type source, Type replay)
            : base(String.Format(
                "Too many replies of type {0} for event {1}.",
                replay.FullName, source.FullName))
        {
        }

        protected TooManyRepliesException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) 
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class MissingReplyException : Exception
    {
        protected MissingReplyException()
        {
        }

        public MissingReplyException(Type source, Type replay)
            : base(String.Format(
                "Missing reply of type {0} for event {1}.",
                replay.FullName, source.FullName))
        {
        }

        protected MissingReplyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
