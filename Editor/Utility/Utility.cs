using System;
using System.Collections.Generic;
using System.Reflection;

namespace Node_based_texture_generator.Editor.Utility
{
    public class Utility
    {
        public static List<Type> FindAttributeUsers(Type attr)
        {
            List<Type> result = new List<Type>();
            Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in allTypes)
            {
                if (Attribute.GetCustomAttributes(type, attr).Length > 0)
                {
                    result.Add(type);
                }
            }

            return result;
        }
    }
}