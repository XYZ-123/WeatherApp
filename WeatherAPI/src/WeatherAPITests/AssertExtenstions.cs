using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPITests
{
    using System.Collections;
    using System.Reflection;

    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Xunit;

    public static class AssertExtenstions
    {
        public static bool DeepEqual(object obj1, object obj2)
        {
            var obj1Type = obj1.GetType();
            var obj2Type = obj2.GetType();

            if (obj1Type != obj2Type) return false;

            if (obj1Type.IsAssignableFrom(typeof(IEnumerable))
                || obj2Type.IsAssignableFrom(typeof(IEnumerable)))
            {
                throw new NotSupportedException("Enumerations are not supported");
            }

            bool result;
            result = AssertProperties(obj1Type.GetProperties(), obj1, obj2Type.GetProperties(), obj2) && AssertFields(obj1Type.GetFields(), obj1, obj2Type.GetFields(), obj2);
            return result;

        }

        private static bool AssertFields(FieldInfo[] obj1Fields, object obj1, FieldInfo[] obj2Fields, object obj2)
        {
            foreach (var field in obj1Fields)
            {
                if (!obj2Fields.Contains(field)) return false;
                if (field.FieldType.IsValueType && Object.Equals(field.GetValue(obj1), field.GetValue(obj2)))
                {
                    continue;
                }
                if (field.FieldType == typeof(string) && string.Equals(field.GetValue(obj1), field.GetValue(obj2)))
                {
                    continue;
                }
                if (field.FieldType.IsClass && DeepEqual(field.GetValue(obj1), field.GetValue(obj2)))
                {
                    continue;
                }
                return false;
            }
            return true;
        }

        private static bool AssertProperties(PropertyInfo[] obj1Properties, object obj1, PropertyInfo[] obj2Properties, object obj2)
        {
            foreach (var property in obj1Properties)
            {
                if (!obj2Properties.Contains(property)) return false;
                if (property.PropertyType.IsValueType && Object.Equals(property.GetValue(obj1), property.GetValue(obj2)))
                {
                    continue;
                }
                if (property.PropertyType == typeof(string) && string.Equals(property.GetValue(obj1), property.GetValue(obj2)))
                {
                    continue;
                }
                if (property.PropertyType.IsClass && DeepEqual(property.GetValue(obj1), property.GetValue(obj2)))
                {
                    continue;
                }
                return false;
            }
            return true;
        }
    }
}
