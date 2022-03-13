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
        typ Deserialize<typ>(string jsonText);
        object Deserialize(Type typ, string jsonText);
    }
}
