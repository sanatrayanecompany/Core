using System;
using System.Linq;
using System.Collections.Generic;

namespace Core.Base
{
    internal class Resolver
    {
        private Dictionary<Type, object> _register;
        private Dictionary<Type, object> Register => _register ?? (_register = new Dictionary<Type, object>());

        internal void RegisterType<I, T>()
        {
            Register.Add(typeof(I), Activator.CreateInstance(typeof(T)));
        }

        internal T GetType<T>()
        {
            return (T)Register.FirstOrDefault(x => x.Key.UnderlyingSystemType == typeof(T)).Value;
        }

        internal object GetType(Type type)
        {
            return Register.FirstOrDefault(x => x.Key.UnderlyingSystemType == type).Value;
        }

        internal void Clear()
        {
            Register.Clear();
        }
    }
}
