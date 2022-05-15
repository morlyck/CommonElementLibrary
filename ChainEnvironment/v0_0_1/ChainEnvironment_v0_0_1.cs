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
        Dictionary<Type, List<object>> currentFloors = null;

        List<Dictionary<Type, List<object>>> FloorDatas = new List<Dictionary<Type, List<object>>>();

        List<object> GetFloorData(Type type) {
            if (currentFloors.ContainsKey(type)) return currentFloors[type];
            var floorData = new List<object>();
            currentFloors.Add(type, floorData);
            return floorData;
        }

        object GetVariableValue(Type type,string variableName) {

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

        #region(DataType)
        public DataType GetValue<DataType>(string variableName) {
            throw new NotImplementedException();
        }
        public DataType SetValue<DataType>(string variableName, object value) {
            throw new NotImplementedException();
        }
        public bool Exists<DataType>(string variableName) {
            throw new NotImplementedException();
        }
        public DataType CreateOrSetValue_Local<DataType>(string variableName, object value) {
            throw new NotImplementedException();
        }
        public bool Remove<DataType>(string variableName) {
            throw new NotImplementedException();
        }
        public void RemoveAll<DataType>(string variableName) {
            throw new NotImplementedException();
        }
        #endregion

        #region(Type)
        public object GetValue(Type type, string variableName) {
            throw new NotImplementedException();
        }
        public object SetValue(Type type, string variableName, object value) {
            throw new NotImplementedException();
        }
        public bool Exists(Type type, string variableName) {
            throw new NotImplementedException();
        }
        public object CreateOrSetValue_Local(Type type, string variableName, object value) {
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
