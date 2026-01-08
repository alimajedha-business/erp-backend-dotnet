using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NGErp.Base.Infrastructure.DataAccess
{
    public static class TypeExtensionMethod
    {
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            var baseType = givenType.BaseType;
            if (baseType == null)
                return false;

            return IsAssignableToGenericType(baseType, genericType);
        }
        public static object GetEnumValue(Type propertyType)
        {
            Random rnd = new Random();
            var lst = Enum.GetValues(propertyType);
            int arrnum = rnd.Next(lst.Length);
            return lst.GetValue(arrnum)!;
        }
      
    }
}

