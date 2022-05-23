using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonElement
{
    public interface IUpstairChain
    {
        bool TryGetValue(Type type, string variableName, out object value);
        bool TrySetValue(Type type, string variableName, object value);
        bool TryCreateOrSetValue_Locally(Type type, string variableName, object value);
        bool Exists(Type type, string variableName);
        bool Remove(Type type, string variableName);
        bool RemoveAll();
    }
}
