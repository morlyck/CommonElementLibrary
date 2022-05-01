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


        DataType GetValue<DataType>(string variableName);
        DataType SetValue<DataType>(string variableName, object value);
        DataType CreateOrSetValue_Local<DataType>(string variableName, object value);
        bool Exists<DataType>(string variableName);
        bool Remove<DataType>(string variableName);
        void RemoveAll<DataType>(string variableName);

        object GetValue(Type type, string variableName);
        object SetValue(Type type, string variableName, object value);
        object CreateOrSetValue_Local(Type type, string variableName, object value);
        bool Exists(Type type, string variableName);
        bool Remove(Type type, string variableName);
        bool RemoveAll(Type type, string variableName);


        void Down();
        void PullArguments();
        void Up();

        void Down(List<(Type, string)> returnValues, List<(Type, string)> arguments);
        void PullArguments(List<(Type, string)> variables);
        void Up(List<(Type, string)> returnValues);

    }
}
