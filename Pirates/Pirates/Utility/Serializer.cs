using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Pirates.Utility
{
    public class Serializer
    {
        public void SerializeObject<T>(string filename, T objectToSerialize)
        {
            using (Stream stream = File.Open(filename, FileMode.Create))
            {
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, objectToSerialize);
                stream.Close();
            }
        }

        public T DeSerializeObject<T>(string filename)
        {
            T objectToSerialize;
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                BinaryFormatter bFormatter = new BinaryFormatter();
                objectToSerialize = (T)bFormatter.Deserialize(stream);
                stream.Close();
            }
            return objectToSerialize;
        }
    }
}