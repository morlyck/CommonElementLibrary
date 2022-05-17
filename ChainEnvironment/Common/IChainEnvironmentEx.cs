using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonElement
{
    public static class IChainEnvironmentEx
    {
        public static DataType GetValue<DataType>(this IChainEnvironment env, string variableName) {
            env.TryGetValue(typeof(DataType), variableName, out object value);
            return (DataType)value;
        }
        public static bool TryGetValue<DataType>(this IChainEnvironment env, string variableName, out DataType value) {
            var result = env.TryGetValue(typeof(DataType), variableName, out object temp);
            value = (DataType)temp;
            return result;
        }
        public static DataType SetValue<DataType>(this IChainEnvironment env, string variableName, DataType value) {
            env.TrySetValue(typeof(DataType), variableName, value);
            return value;
        }
        public static DataType CreateOrSetValue_Locally<DataType>(this IChainEnvironment env, string variableName, DataType value) {
            env.TryCreateOrSetValue_Locally(typeof(DataType), variableName, value);
            return value;
        }
        public static bool Exists<DataType>(this IChainEnvironment env, string variableName) {
            return env.Exists(typeof(DataType), variableName);
        }
        public static bool Remove<DataType>(this IChainEnvironment env, string variableName) {
            return env.Remove(typeof(DataType), variableName);
        }
        //
        public static object GetValue(this IChainEnvironment env, Type type, string variableName) {
            env.TryGetValue(type, variableName, out object value);
            return value;
        }
    }
}
