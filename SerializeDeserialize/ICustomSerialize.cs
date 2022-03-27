using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonElement
{
    public interface ICustomSerialize
    {
        string Serialize(ISerializerAndDeserializer serializer);

        void Deserialize(ISerializerAndDeserializer deserializer, string text);
    }
}
