using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class IoCAttribute : Attribute
    {
    }
}
