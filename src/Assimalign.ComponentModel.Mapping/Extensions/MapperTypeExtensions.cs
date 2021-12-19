using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping
{
    internal static class MapperTypeExtensions
    {
        public static bool IsSystemValueType(this Type type)
        {
            var valueTypes = new[]
            {
                typeof(string),
                typeof(int)
            };

            foreach(var valueType in valueTypes)
            {
                if (valueType == type)
                {
                    return true;
                }
            }

            return false;
        }


        public static bool IsComplexType(this Type type)
        {


            return true;
        }

        public static bool IsEnumerableType(this Type type, out Type implementation)
        {
            implementation = null;


            return true;
        }
    }
}
