using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonElement
{
    public interface ISerializerAndDeserializer
    {
        string Serialize(object obj);
        string Serialize_Indented(object obj);
        typ Deserialize<typ>(string jsonText);
        object Deserialize(Type typ, string jsonText);
        //
        string SerializeCustomOrBasic(object obj);
        typ DeserializeCustomOrBasic<typ>(string jsonText);
        object DeserializeCustomOrBasic(Type typ, string jsonText);
    }
}
