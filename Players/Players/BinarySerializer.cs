using Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Players
{
    class BinarySerializer:MainSerializer
    {
        public void Serialize(List<object> someObject, Stream stream)
        {
            object[] obj = someObject.ToArray();

            BinaryFormatter binformatter = new BinaryFormatter();
            FileStream fs = (FileStream)stream;

            binformatter.Serialize(fs, obj);

            fs.Close();
        }

        public List<T> Deserialize<T>(string str)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                List<T> TList = new List<T>();
                object[] obj;

                using (FileStream fs = new FileStream(str,FileMode.OpenOrCreate))
                {
                    obj = (object[])formatter.Deserialize(fs);
                }

                foreach (object ob in obj)
                {
                    TList.Add((T)ob);
                }

                return TList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!\n" + ex.Message);
                return default(List<T>);
            }
        }

        public void SerializeWithPlugin(List<object> someObject, Stream streamForSerializing, PluginClass pluginName)
        {
            object[] objectForSerializing = someObject.ToArray();

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = (FileStream)streamForSerializing;

            var memoryStream = new MemoryStream();
            using (memoryStream)
            {
                binaryFormatter.Serialize(memoryStream, objectForSerializing);
            }

            string resultString = pluginName.Encript(memoryStream.ToArray());

            fileStream.Write(Encoding.ASCII.GetBytes(resultString), 0, resultString.Length);
            fileStream.Close();
        }

        public List<T> DeserializeWithPlugin<T>(string stringForDeserializing, PluginClass pluginName)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                object[] objectForDeserializing;
                List<T> ListForResults = new List<T>();

                using (FileStream fs = new FileStream(stringForDeserializing, FileMode.OpenOrCreate))
                {
                   
                    List<string> lines = new List<string>();
                    string stringForInformationFromStream;
                    using (StreamReader streamForDecripting = new StreamReader(fs))
                    {
                        while ((stringForInformationFromStream = streamForDecripting.ReadLine()) != null)
                        {
                            lines.Add(stringForInformationFromStream);
                        }

                        string decriptingInformation = pluginName.Decript(String.Join("",lines));
                        byte[] info = Encoding.ASCII.GetBytes(decriptingInformation);
                        Stream stream = new MemoryStream(info);
                        objectForDeserializing = (object[])binaryFormatter.Deserialize(stream);

                    }
                }

                foreach (object someObject in objectForDeserializing)
                {
                    ListForResults.Add((T)someObject);
                }

                return ListForResults;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Ошибка!\n" + ex.Message);
                
                return default(List<T>);

            }
        }

        public string GetExtention()
        {
            return "binar";
        }
    }
}
