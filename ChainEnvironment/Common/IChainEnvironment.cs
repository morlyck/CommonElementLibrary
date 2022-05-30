using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonElement
{
    public interface IChainEnvironment :ICustomSerialize{
        string Version { get; }

        bool MultiBand { get; }

        void SetUpstairChain_LooseConnection(IUpstairChain upstairChain);
        void SetUpstairChain(IUpstairChain upstairChain, int connectionFloorNo);
        void SetUpstairChain_ConnectionToCurrentFloorNo(IUpstairChain upstairChain);
        void ClearUpstairChainSetting();


        bool TryGetValue(Type type, string variableName, out object value);
        bool TrySetValue(Type type, string variableName, object value);
        bool TryCreateOrSetValue_Locally(Type type, string variableName, object value);
        bool Exists(Type type, string variableName);
        bool Remove(Type type, string variableName);
        bool RemoveAll();


        void Down();
        void PullArguments();
        void Up();

        void Down(List<(Type, string)> returnValues, List<(Type, string, object)> arguments);
        void PullArguments(List<(Type, string)> variables);
        void Up(List<(Type, string)> returnValues);

    }
}
