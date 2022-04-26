using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonElement
{
    public interface IChainEnvironment :ICustomSerialize{
        string Version { get; set; }

        bool MultiBand { get; }

        void SetUpstairEnvironment_LooseConnection(IUpstairEnvironment upstairEnvironment);
        void SetUpstairEnvironment(IUpstairEnvironment upstairEnvironment, int connectionFloorNo);
        void SetUpstairEnvironment_ConnectionToCurrentFloorNo(IUpstairEnvironment upstairEnvironment);
        void ClearUpstairEnvironmentSetting();


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


        void Down();
        void PullArguments();
        void Up();

        void Down(List<(Type, string)> returnValues, List<(Type, string)> arguments);
        void PullArguments(List<(Type, string)> variables);
        void Up(List<(Type, string)> returnValues);

    }
}
