using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public abstract class Result
    { }

    public abstract class Unhandled : Result
    { }

    public abstract class Succeeded : Result
    { }

    public abstract class Failed : Result
    { }
}
