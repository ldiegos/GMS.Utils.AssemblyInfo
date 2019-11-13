using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace GMS.Utils.AssemblyInfoUtil
{
    public static class Extensions
    {
        /// <summary>
        /// Wrapper visualbasic's function to know if a string is a number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string value)
        {
            return Information.IsNumeric(value);
        }
    }
}
