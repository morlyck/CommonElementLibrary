using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonElement.ChainEnvironment_v0_0_1;

namespace CommonElement.ChainEnvironment_v0_0_1
{
    public class ChainEnvironment : IChainEnvironment, IUpstairChain
    {
        public class ChainEnvironmentSdReady
        {
            public Dictionary<string, string> floorDatas = new Dictionary<string, string>();

        }

        public string Version { get; } = "0.0.1";

        public bool MultiBand => throw new NotImplementedException();

        #region(Serialize)
        public string Serialize(ISerializerAndDeserializer serializer) {
            throw new NotImplementedException();
        }
        public void Deserialize(ISerializerAndDeserializer deserializer, string text) {
            throw new NotImplementedException();
        }

        #endregion

        int currentFloorNo = 0;
        Dictionary<Type, Dictionary<string, object>> currentFloor = null;

        List<Dictionary<Type, Dictionary<string,object>>> FloorDatas = new List<Dictionary<Type, Dictionary<string, object>>>();

        //有効な値が取得できた場合の戻り値 : true
        bool TryGetVariableValue(Type type,string variableName, out object value) {
            if(currentFloorNo == -1 ||
                !currentFloor.ContainsKey(type)||
                !currentFloor[type].ContainsKey(variableName)) {
                value = null;
                return false;
            }

            value = currentFloor[type][variableName];
            return true;
        }

        //値の更新ないしは新規作成がされた場合の戻り値 : true
        bool SetVariableValue(Type type, string variableName, object value) {
            if (currentFloorNo == -1) return false;

            Dictionary<string, object> variables = null;
            if (!currentFloor.ContainsKey(type)) {
                variables = new Dictionary<string, object>();
                currentFloor.Add(type, variables);
            } else {
                variables = currentFloor[type];
            }

            if (!variables.ContainsKey(variableName)) {
                variables.Add(variableName, value);
            } else {
                variables[variableName] = value;
            }
            return true;
        }


        #region(UpstairChain)
        public void SetUpstairChain(IUpstairChain upstairEnvironment, int connectionFloorNo) {
            throw new NotImplementedException();
        }

        public void SetUpstairChain_ConnectionToCurrentFloorNo(IUpstairChain upstairEnvironment) {
            throw new NotImplementedException();
        }

        public void SetUpstairChain_LooseConnection(IUpstairChain upstairEnvironment) {
            throw new NotImplementedException();
        }
        public void ClearUpstairChainSetting() {
            throw new NotImplementedException();
        }
        #endregion

        #region(from downstair)
        object IUpstairChain.GetValue(Type type, string variableName) {
            throw new NotImplementedException();
        }
        object IUpstairChain.SetValue(Type type, string variableName, object value) {
            throw new NotImplementedException();
        }
        object IUpstairChain.CreateOrSetValue_Local(Type type, string variableName, object value) {
            throw new NotImplementedException();
        }
        bool IUpstairChain.Exists(Type type, string variableName) {
            throw new NotImplementedException();
        }
        bool IUpstairChain.Remove(Type type, string variableName) {
            throw new NotImplementedException();
        }
        bool IUpstairChain.RemoveAll(Type type, string variableName) {
            throw new NotImplementedException();
        }
        #endregion


        #region(Type)
        public bool TryGetValue(Type type, string variableName, out object value) {
            throw new NotImplementedException();
        }
        public bool TrySetValue(Type type, string variableName, object value) {
            throw new NotImplementedException();
        }
        public bool TryCreateOrSetValue_Locally(Type type, string variableName, object value) {
            throw new NotImplementedException();
        }
        public bool Exists(Type type, string variableName) {
            throw new NotImplementedException();
        }
        public bool Remove(Type type, string variableName) {
            throw new NotImplementedException();
        }
        public bool RemoveAll(Type type, string variableName) {
            throw new NotImplementedException();
        }
        #endregion



        public void Down() {
            throw new NotImplementedException();
        }
        public void PullArguments() {
            throw new NotImplementedException();
        }
        public void Up() {
            throw new NotImplementedException();
        }

        public void Down(List<(Type, string)> returnValues, List<(Type, string)> arguments) {
            throw new NotImplementedException();
        }
        public void PullArguments(List<(Type, string)> variables) {
            throw new NotImplementedException();
        }
        public void Up(List<(Type, string)> returnValues) {
            throw new NotImplementedException();
        }
    }
}
