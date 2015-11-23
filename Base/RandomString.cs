using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public class RandomString
    {
        public RandomString(int length)
        {
            int min = (int)Math.Pow(10, length - 1);
            int max = (int)Math.Pow(10, length) - 1;
            var random = new Random();
            Text = random.Next(min, max).ToString();
        }

        string Text { get; }

        public override string ToString()
        {
            return Text;
        }

        public static implicit operator string(RandomString value)
        {
            return value.ToString();
        }
    }
}
