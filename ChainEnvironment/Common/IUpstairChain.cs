using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonElement
{
    public interface IUpstairChain
    {
        object GetValue(Type type, string variableName);
        object SetValue(Type type, string variableName, object value);
        object CreateOrSetValue_Local(Type type, string variableName, object value);
        bool Exists(Type type, string variableName);
        bool Remove(Type type, string variableName);
        bool RemoveAll(Type type, string variableName);
    }
}
