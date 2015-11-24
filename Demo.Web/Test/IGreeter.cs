using Events;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Web.Test
{
    public interface IGreeter
    {
        string SayHello();
    }

    public class Greeting : ILoggable<Succeeded>, ILoggable<Failed>
    {
        public Greeting(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
