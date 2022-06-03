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
            public List<List<string>> FloorDatas ;

        }

        public string Version { get; } = "0.0.1";

        public bool MultiBand => throw new NotImplementedException();

        #region(Serialize)
        public string Serialize(ISerializerAndDeserializer serializer) {
            var sdReady = new ChainEnvironmentSdReady();
            for(int count = 0; count < FloorDatas.Count; count++) {
                List<string> versions = new List<string>();
                foreach (var floorData in FloorDatas[count]) {
                    string typeText = floorData.Key.AssemblyQualifiedName;
                    foreach (var version in floorData.Value) {
                        versions.Add(AssyFloorDataText(
                            typeText, 
                            version.Key, 
                            serializer.Serialize_Indented(version.Value)));
                    }
                }
                sdReady.FloorDatas.Add(versions);
            }
            return $"^{Version},{serializer.Serialize_Indented(sdReady)}";
        }
        public void Deserialize(ISerializerAndDeserializer deserializer, string text) {
            var result = GetVersionInfoAndSerializeText(text);

            if (result.Item1 != Version) throw new Exception("ChainEnvironmentのDeserializeに失敗");
            var sdReady = deserializer.Deserialize<ChainEnvironmentSdReady>(result.Item2);
            for(int count = 0; count<sdReady.FloorDatas.Count; count++) {
                var floorData = new Dictionary<Type, Dictionary<string, object>>();

                var floorDataTexts = sdReady.FloorDatas[count];
                foreach(var floorDataText in floorDataTexts) {
                    var floorDataSource = ParseFloorDataText(floorDataText);
                    Type type = Type.GetType(floorDataSource.Item1);
                    string versionName = floorDataSource.Item2;
                    object value = deserializer.Deserialize(type ,floorDataSource.Item3);

                    if (!floorData.ContainsKey(type)) {
                        Dictionary<string, object> versions = new Dictionary<string, object>();
                        versions.Add(versionName, value);
                        floorData.Add(type, versions);
                    } else {
                        floorData[type].Add(versionName, value);
                    }
                }

                //フロアデータを追加
                FloorDatas.Add(floorData);
            }
            currentFloorNo = FloorDatas.Count - 1;
            currentFloor = FloorDatas[currentFloorNo];
        }
        (string, string) GetVersionInfoAndSerializeText(string text) {
            if (text.Substring(0, 1) != "^") return (null, text);
            int index = text.IndexOf(",");
            if (index == -1) return (null, text);

            return (text.Substring(1, index - 1), text.Substring(index + 1));
        }
        (string, string, string) ParseFloorDataText(string text) {
            int index = text.IndexOf("/~,");
            if (index == -1) return (null, null, null);
            string typeText = text.Substring(0, index);
            string temp = text.Substring(index + 1);
            index = temp.IndexOf("/~,");

            string versionName = text.Substring(0, index);
            string dataText = text.Substring(index + 1);
            return (typeText, versionName, dataText);
        }
        string AssyFloorDataText(string typeText, string versionName, string dataText) {
            return $"{typeText}/~,{versionName}/~,{dataText}";
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
            Arguments = null;
            FloorDatas.Add(new Dictionary<Type, Dictionary<string, object>>());
            currentFloorNo++;
            currentFloor = FloorDatas[currentFloorNo];
        }
        public void PullArguments() {
            //何もしない
        }
        public void Up() {
            FloorDatas.RemoveAt(FloorDatas.Count - 1);
            currentFloorNo--;
            currentFloor = FloorDatas[currentFloorNo];
        }

        List<(Type, string)> ReturnValues = null;
        List<(Type, string, object)> Arguments = null;
        public void Down(List<(Type, string)> returnValues, List<(Type, string, object)> arguments) {
            ReturnValues = returnValues;
            Down();
            Arguments = arguments;
        }
        public void PullArguments(List<(Type, string)> variables) {
            if (Arguments == null) return;
            for(int count = 0; count < variables.Count; count++) {
                TryCreateOrSetValue_Locally(Arguments[count].Item1, variables[count].Item2, Arguments[count].Item3);
            }
        }
        public void Up(List<(Type, string)> returnValues) {
            List<object> values = new List<object>();
            for(int count = 0; count < returnValues.Count; count++) {
                values.Add(this.GetValue(returnValues[count].Item1, returnValues[count].Item2));
            }
            currentFloorNo--;
            currentFloor = FloorDatas[currentFloorNo];
            for (int count = 0; count < ReturnValues.Count; count++) {
                TrySetValue(ReturnValues[count].Item1, ReturnValues[count].Item2, values[count]);
            }

            FloorDatas.RemoveAt(FloorDatas.Count - 1);
        }
    }
}
