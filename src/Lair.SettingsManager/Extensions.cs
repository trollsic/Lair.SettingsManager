using System;
using System.Linq;
using System.Reflection;

namespace Lair.SettingsManager
{
    public static class Extensions
    {
        public static void ReadAttribute<TAttribute>(this PropertyInfo property, Action<TAttribute> callback)
        {
            var instances = property.GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>();
            foreach (var instance in instances)
            {
                callback(instance);
            }
        }
    }
}
