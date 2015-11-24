using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public class EventScope
    {
        const string Slot = "CF2A3FD9-7167-4EAF-934E-3CE641827F1D";

        static EventScope()
        {
            ContextEvent = Empty;
        }

        public EventScope()
        {
            PreviosEvent = ContextEvent;
            ContextEvent = Empty;
        }

        object PreviosEvent { get; }

        public static object ContextEvent
        {
            get { return CallContext.LogicalGetData(Slot); }
            set
            {
                CallContext.LogicalSetData(Slot, value);
                if (ContextEvent != null && ContextEvent != Empty)
                    Event.NotifyAsync(value);                
            }
        }

        public static object Empty { get; } = new object();

        public async void Complete()
        {
            if (ContextEvent != Empty && ContextEvent != null)
                await Event.NotifyAsync<Succeeded>(ContextEvent);

            ContextEvent = PreviosEvent;
        }

        public async void Fail(Exception ex)
        {
            if (ContextEvent != Empty && ContextEvent != null)
                await Event.NotifyAsync<Failed>(ContextEvent, ex);

            ContextEvent = PreviosEvent;
        }         
    }    
}
