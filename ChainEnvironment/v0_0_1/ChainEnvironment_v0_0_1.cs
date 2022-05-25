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
            public Dictionary<string, string> FloorDatas = new Dictionary<string, string>();

        }

        public string Version { get; } = "0.0.1";

        public bool MultiBand => throw new NotImplementedException();

        #region(Serialize)
        public string Serialize(ISerializerAndDeserializer serializer) {
            throw new NotImplementedException();
            ChainEnvironmentSdReady sdReady = new ChainEnvironmentSdReady();
            return serializer.Serialize_Indented(sdReady);
        }
        public void Deserialize(ISerializerAndDeserializer deserializer, string text) {
            throw new NotImplementedException();
        }
        (string, string) GetVersionInfoAndSerializeText(string text) {
            if (text.Substring(0, 1) != "^") return (null, text);
            int index = text.IndexOf(",");
            if (index == -1) return (null, text);

            return (text.Substring(1, index - 1), text.Substring(index + 1));
        }
        (int, string, string) GetTypeTextAndVersionName(string text) {
            int index = text.IndexOf("/~,");
            if (index == -1) return (0, null, text);
            int floorNo = int.Parse(text.Substring(0, index));
            string temp = text.Substring(index + 1);
            index = temp.IndexOf("/~,");

            string typeText = text.Substring(0, index);
            string versionName = text.Substring(index + 1);
            return (floorNo, typeText, versionName);
        }
        string GetAssyTextTypeTextAndVersionName(string typeText, string versionName) {
            return $"{typeText}/~,{versionName}";
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
        bool TrySetVariableValue(Type type, string variableName, object value, bool locally) {
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

        //削除成功時の戻り値 : true
        bool TryRemoveVariable(Type type, string variableName) {
            if (currentFloorNo == -1) return false;

            if(!currentFloor.ContainsKey(type) ||
                !currentFloor[type].ContainsKey(variableName))return false;
            return currentFloor[type].Remove(variableName);
        }

        //削除成功時の戻り値 : true
        bool TryExistsVariable(Type type, string variableName) {
            if (currentFloorNo == -1) return false;

            return currentFloor.ContainsKey(type) && currentFloor[type].ContainsKey(variableName);
        }


        #region(UpstairChain)
        IUpstairChain UpstairEnvironment = null;
        int ConnectionFloorNo;
        bool LooseConnection;
        public void SetUpstairChain(IUpstairChain upstairEnvironment, int connectionFloorNo) {
            UpstairEnvironment = upstairEnvironment;
            ConnectionFloorNo = connectionFloorNo;
            LooseConnection = false;
        }

        public void SetUpstairChain_ConnectionToCurrentFloorNo(IUpstairChain upstairEnvironment) {
            UpstairEnvironment = upstairEnvironment;
            ConnectionFloorNo = currentFloorNo;
            LooseConnection = false;
        }

        public void SetUpstairChain_LooseConnection(IUpstairChain upstairEnvironment) {
            UpstairEnvironment = upstairEnvironment;
            ConnectionFloorNo = -1;
            LooseConnection = true;
        }

        public void ClearUpstairChainSetting() {
            UpstairEnvironment = null;
        }
        #endregion

        #region(from downstair)
        bool IUpstairChain.TryGetValue(Type type, string variableName, out object value) {
            throw new NotImplementedException();
        }
        bool IUpstairChain.TrySetValue(Type type, string variableName, object value) {
            throw new NotImplementedException();
        }
        bool IUpstairChain.TryCreateOrSetValue_Locally(Type type, string variableName, object value) {
            throw new NotImplementedException();
        }
        bool IUpstairChain.Exists(Type type, string variableName) {
            throw new NotImplementedException();
        }
        bool IUpstairChain.Remove(Type type, string variableName) {
            throw new NotImplementedException();
        }
        bool IUpstairChain.RemoveAll() {
            throw new NotImplementedException();
        }
        #endregion


        #region(Type)
        public bool TryGetValue(Type type, string variableName, out object value) {
            return TryGetVariableValue(type, variableName, out value);
        }
        public bool TrySetValue(Type type, string variableName, object value) {
            return TrySetVariableValue(type, variableName, value, false);
        }
        public bool TryCreateOrSetValue_Locally(Type type, string variableName, object value) {
            return TrySetVariableValue(type, variableName, value, true);
        }
        public bool Exists(Type type, string variableName) {
            return TryExistsVariable(type, variableName);
        }
        public bool Remove(Type type, string variableName) {
            return TryRemoveVariable(type, variableName);
        }
        public bool RemoveAll() {
            currentFloorNo = 0;
            FloorDatas.Clear();
            currentFloor = new Dictionary<Type, Dictionary<string, object>>();
            FloorDatas.Add(currentFloor);

            return true;
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
