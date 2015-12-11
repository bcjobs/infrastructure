using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public class ConnectionStrings
    {
        public static ConnectionStrings Settings { get; } = new ConnectionStrings();
        
        readonly Dictionary<string, string> _values = new Dictionary<string, string>();

        public ConnectionStrings()
        {
            ReadConfig();
        }

        void ReadConfig()
        {
            try
            {
                foreach (System.Configuration.ConnectionStringSettings connection in ConfigurationManager.ConnectionStrings)
                {
                    this[connection.Name] = connection.ConnectionString;
                }

            }
            catch (ConfigurationErrorsException)
            {
            }
        }

        public string this[string name]
        {
            get
            {
                return _values[name];
            }
            set
            {
                _values[name] = value;
            }
        }

    }
}
