using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Services.Core.Common
{
    public class CommonErrors
    {
        protected CommonErrors () { }

        public static Exception DataItemFieldNotFound (string name)
        {
            return new KeyNotFoundException(FormatMessage(Resources.DataItemFieldNotFoundFormat, name));
        }
       
        protected static string FormatMessage (string format, params object[] args)
        {
            return String.Format(CultureInfo.InvariantCulture, format, args);
        }
    }
}
