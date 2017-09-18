using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace Block_Chain_Impl_Ver01
{

    public class SerializationUnit
    {
        /// <summary>  
        /// The object is serialized as a byte array  
        /// </summary>  
        public static byte[] SerializeObject(object obj)
        {
            if (obj == null)
                return null;
            //Memory example
            MemoryStream ms = new MemoryStream();
            //To create an instance serialization
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);//Serialization of objects, written in MS flow  
            ms.Position = 0;
            //byte[] bytes = new byte[ms.Length];//This is wrong
            byte[] bytes = ms.GetBuffer();
            ms.Read(bytes, 0, bytes.Length);
            ms.Close();
            return bytes;
        }

        /// <summary>  
        /// The array of bytes to deserialize an object  
        /// </summary>  
        public static object DeserializeObject(byte[] bytes)
        {
            object obj = null;
            if (bytes == null)
                return obj;
            //Create a memory stream using the byte[] passed
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            obj = formatter.Deserialize(ms);//The memory stream reverse sequence into object  
            ms.Close();
            return obj;
        }
    }
}