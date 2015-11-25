using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mixins
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public class MixinAttribute : Attribute
    {
    }
}
