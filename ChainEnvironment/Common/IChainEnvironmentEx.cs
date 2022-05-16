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
            throw new NotImplementedException();
        }
        public static bool TryGetValue<DataType>(this IChainEnvironment env, string variableName, out object value) {
            throw new NotImplementedException();
        }
        public static DataType SetValue<DataType>(this IChainEnvironment env, string variableName, object value) {
            throw new NotImplementedException();
        }
        public static DataType CreateOrSetValue_Locally<DataType>(this IChainEnvironment env, string variableName, object value) {
            throw new NotImplementedException();
        }
        public static bool Exists<DataType>(this IChainEnvironment env, string variableName) {
            throw new NotImplementedException();
        }
        public static bool Remove<DataType>(this IChainEnvironment env, string variableName) {
            throw new NotImplementedException();
        }
        public static void RemoveAll<DataType>(this IChainEnvironment env, string variableName) {
            throw new NotImplementedException();
        }
        //
        public static object GetValue(Type type, string variableName) {
            throw new NotImplementedException();
        }
        public static DataType SetValue<DataType>(this IChainEnvironment env, Type type, string variableName, object value) {
            throw new NotImplementedException();
        }
        public static DataType CreateOrSetValue_Locally<DataType>(this IChainEnvironment env, Type type, string variableName, object value) {
            throw new NotImplementedException();
        }
    }
}
