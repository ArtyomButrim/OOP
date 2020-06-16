using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Plugin;

namespace Players
{
    class JSONSerializer : MainSerializer
    {
        public void Serialize(List<object> obj, Stream stream)
        {
            FileStream fs = (FileStream)stream;
            string jsonString = "";

            for (int i = 0; i < obj.Count; i++)
            {
                jsonString += JsonConvert.SerializeObject(obj[i], Formatting.Indented);
                if (i < obj.Count-1)
                {
                    jsonString += "$";
                }
            }

            fs.Write(Encoding.ASCII.GetBytes(jsonString), 0, jsonString.Length);

            fs.Close();
        }

        public List<T> Deserialize<T>(string str)
        {
            string st = File.ReadAllText(str);
            List<string> lines = new List<string>();
            string s;
            using (StreamReader streamReader = File.OpenText(str))
            {
                while ((s = streamReader.ReadLine()) != null)
                {
                    lines.Add(s);

                }

                string[] strs = st.Split('$');

                try
                {
                    List<T> tList = new List<T>();
                    foreach (string someString in strs)
                    {
                        T t = JsonConvert.DeserializeObject<T>(someString);
                        tList.Add(t);
                    }

                    return tList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!\n" + ex.Message);
                    return default(List<T>);
                }
            }
        }


        public void SerializeWithPlugin(List<object> objectForSerializing, Stream streamForWork, PluginClass pluginName)
        {
            FileStream fileStream = (FileStream)streamForWork;
            string jsonString = "";

            for (int i = 0; i < objectForSerializing.Count; i++)
            {
                jsonString += JsonConvert.SerializeObject(objectForSerializing[i], Formatting.Indented);
                if (i < objectForSerializing.Count - 1)
                {
                    jsonString += "$";
                }
            }

            fileStream.Write(Encoding.ASCII.GetBytes(pluginName.Encript(System.Text.Encoding.UTF8.GetBytes(jsonString))), 0, pluginName.Encript(System.Text.Encoding.UTF8.GetBytes(jsonString)).Length);

            fileStream.Close();
        }

        public List<T> DeserializeWithPlugin<T>(string stringForDeserializing, PluginClass pluginName)
        {
            string stringWithEncriptText= File.ReadAllText(stringForDeserializing);
            List<string> lines = new List<string>();
            string str;
            using (StreamReader streamReader = File.OpenText(stringForDeserializing))
            {
                while ((str = streamReader.ReadLine()) != null)
                {
                    lines.Add(str);

                }

                stringWithEncriptText = pluginName.Decript(String.Join("",lines));

                string[] strs = stringWithEncriptText.Split('$');

                try
                {
                    List<T> ListForResults = new List<T>();
                    foreach (string someString in strs)
                    {
                        T result = JsonConvert.DeserializeObject<T>(someString);
                        ListForResults.Add(result);
                    }

                    return ListForResults;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!\n" + ex.Message);
                    return default(List<T>);
                }
            }
        }

        public string GetExtention()
        {
            return "json";
        }
    }
}
