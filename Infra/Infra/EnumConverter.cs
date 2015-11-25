using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public class EnumConverter<T> : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                var text = (string)value;
                var type = typeof(T);
                var field = type.GetFields()
                    .SingleOrDefault(f =>
                        f.Name == text ||
                        f.GetCustomAttributes(false)
                            .OfType<EnumMemberAttribute>()
                            .Any(a => a.Value == text));

                if (field != null)
                    return field.GetValue(null);
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
