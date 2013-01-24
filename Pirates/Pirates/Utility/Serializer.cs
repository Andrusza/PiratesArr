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
                BinaryFormatter Formatter = new BinaryFormatter();
                //Formatter.Serialize(stream, objectToSerialize);
                stream.Close();
            }
        }

        public T DeSerializeObject<T>(string filename)
        {
            T objectToSerialize;
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                objectToSerialize = (T)Formatter.Deserialize(stream);
                stream.Close();
            }
            return objectToSerialize;
        }
    }
}