using Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Players
{
    class MySerializer:MainSerializer
    {
        public void Serialize(List<object> obj, Stream stream)
        {
            StreamWriter streamWrite = new StreamWriter(stream);
            string serialText = "";

            for (int i = 0; i < obj.Count; i++)
            {
                string strObject = "";
                FormObjectsLine(obj[i], "", ref strObject);
                serialText += strObject;
            }

            streamWrite.WriteLine(serialText);
            
            streamWrite.Close();
        }

        public List<T> Deserialize<T>(string str)
        {
            bool isCorrectType = false;
            List<T> l = new List<T>();
            List<string> st = new List<string>();
            string s3;
            string str1;

            using (StreamReader streamReader = File.OpenText(str))
            {
                while ((s3 = streamReader.ReadLine()) != null)
                {
                    st.Add(s3);
                }
            }
            
            str1 = File.ReadAllText(str);
            
            StringReader sr = new StringReader(str1);
            string strr;

            T obj = (T)Activator.CreateInstance(typeof(T));
            Type ObjType = obj.GetType();

            while ((strr = sr.ReadLine()) != null)
            {
                string lineName = "";
                string lineVal = "";

                if (strr.Contains(">") && !strr.Contains("\'"))
                {
                    lineName = strr.Replace("\t", "");
                    lineName = lineName.Replace(" ", "");
                    lineName = lineName.Replace(">", "");
                }

                if (strr.Contains(":"))
                {
                    string[] s = strr.Split(':');

                    lineName = s[0].Replace("\t", "");
                    lineName = lineName.Replace(" ", "");

                    if (s[1].Contains("\'"))
                    {
                        lineVal = strr.Split('\'')[1];
                    }
                    else
                    {
                        lineVal = s[1].Replace("\t", "");
                        lineVal = lineVal.Replace(" ", "");
                    }
                }

                if (strr.Contains("<") && !strr.Contains("\'"))
                {
                    lineName = strr.Replace("\t", "");
                    lineName = lineName.Replace(" ", "");
                    lineName = lineName.Replace("<", "");

                    if (lineName == ObjType.Name)
                    {
                        l.Add(obj);
                        obj = (T)Activator.CreateInstance(typeof(T));
                        continue;
                    }
                }

                if (lineName == ObjType.Name)
                {
                    isCorrectType = true;
                }
                else
                {
                    PropertyInfo[] pi = ObjType.GetProperties();
                    foreach (PropertyInfo p in pi)
                    {
                        if ((p.PropertyType.IsPrimitive || p.PropertyType.Name == "String" ||
                            p.PropertyType.Name == "Int32" || p.PropertyType.Name == "Int64" ||
                            p.PropertyType.Name == "Long")
                            )
                        {
                            if (p.Name == lineName)
                            {
                                if (p.PropertyType.Name == "String")
                                {
                                    p.SetValue(obj, lineVal, null);
                                    break;
                                }
                                if (p.PropertyType.Name == "Int32")
                                {
                                    p.SetValue(obj, Int32.Parse(lineVal), null);
                                    break;
                                }
                                if (p.PropertyType.Name == "Int64")
                                {
                                    p.SetValue(obj, Int64.Parse(lineVal), null);
                                    break;
                                }
                                if (p.PropertyType.Name == "Long")
                                {
                                    p.SetValue(obj, long.Parse(lineVal), null);
                                    break;
                                }
                            }
                        }
                        else if (lineName == p.PropertyType.Name)
                        {
                            doFind<T>(ref obj, ref sr, strr, p);
                        }
                    }
                }
            }

            sr.Close();

            if (isCorrectType)
            {
                return l;
            }
            else
            {
                return default(List<T>);
            }
        }

        void FormObjectsLine(object obj, string set,ref string str)
        {
            Type t = obj.GetType();
            PropertyInfo[] pi = t.GetProperties();

            str += set + Begin(t.Name);

            foreach (PropertyInfo some in pi)
            {
                if (some.PropertyType.IsPrimitive || some.PropertyType == typeof(string))
                {
                    if (some.PropertyType == typeof(string))
                    {
                        str += set + FormStringLine(some.Name, some.GetValue(obj, null).ToString());
                    }
                    else
                    {
                        str += set + FormLine(some.Name, some.GetValue(obj, null).ToString());
                    }
                }
                else
                {
                    object obj2 = some.GetValue(obj, null);
                    FormObjectsLine(obj2, (set + "\t"), ref str);
                }
            }

            str += set + End(t.Name);
        }

        void doFind<T>(ref T ob, ref StringReader reader, string currentLine, PropertyInfo propInfo)
        {
            object obj = Activator.CreateInstance(propInfo.PropertyType);
            Type innerObjType = propInfo.PropertyType;
            string strr = currentLine;

            do
            {
                string lineName = "";
                string lineVal = "";

                if (strr.Contains(">") && !strr.Contains("\'"))
                {
                    lineName = strr.Replace("\t", "");
                    lineName = lineName.Replace(" ", "");
                    lineName = lineName.Replace(">", "");
                }

                if (strr.Contains(":"))
                {
                    string[] s = strr.Split(':');

                    lineName = s[0].Replace("\t", "");
                    lineName = lineName.Replace(" ", "");

                    if (s[1].Contains("\'"))
                    {
                        lineVal = strr.Split('\'')[1];
                    }
                    else
                    {
                        lineVal = s[1].Replace("\t", "");
                        lineVal = lineVal.Replace(" ", "");
                    }
                }

                if (strr.Contains("<") && !strr.Contains("\'"))
                {
                    lineName = strr.Replace("\t", "");
                    lineName = lineName.Replace(" ", "");
                    lineName = lineName.Replace("<", "");
                    if (lineName == innerObjType.Name)
                    {
                        propInfo.SetValue(ob, obj, null);
                        return;
                    }
                }

                if (lineName == innerObjType.Name)
                {

                }
                else
                {
                    PropertyInfo[] pi = innerObjType.GetProperties();
                    foreach (PropertyInfo p in pi)
                    {
                        if ((p.PropertyType.IsPrimitive || p.PropertyType.Name == "String" ||
                            p.PropertyType.Name == "Int32" || p.PropertyType.Name == "Int64" ||
                            p.PropertyType.Name == "Long" || p.PropertyType.Name == "Single")
                            )
                        {
                            if (p.Name == lineName)
                            {
                                if (p.PropertyType.Name == "String")
                                {
                                    p.SetValue(obj, lineVal, null);
                                    break;
                                }
                                if (p.PropertyType.Name == "Single")
                                {
                                    p.SetValue(obj, float.Parse(lineVal), null);
                                    break;
                                }
                                if (p.PropertyType.Name == "Int32")
                                {
                                    p.SetValue(obj, Int32.Parse(lineVal), null);
                                    break;
                                }
                                if (p.PropertyType.Name == "Int64")
                                {
                                    p.SetValue(obj, Int64.Parse(lineVal), null);
                                    break;
                                }
                                if (p.PropertyType.Name == "Long")
                                {
                                    p.SetValue(obj, long.Parse(lineVal), null);
                                    break;
                                }
                            }
                        }
                        else if (lineName == p.PropertyType.Name)
                        {
                            doFind<object>(ref obj, ref reader, strr, p);

                        }
                    }
                }
            } while ((strr = reader.ReadLine()) != null);
        }

        string FormLine(string name, string value)
        {
            string s = "\t" + name + " : " + value + "\n";
            return s;
        }


        string FormStringLine(string name, string value)
        {
            string s = "\t" + name + " : '" + value + "'\n";
            return s;
        }
        string Begin(string name)
        {
            return name + " >\n";
        }

        string End(string name)
        {
            return name + "  <\n";
        }

        public string GetExtention()
        {
            return "special";
        }

        public void SerializeWithPlugin(List<object> objectForSerializing, Stream streamForWork, PluginClass pluginName)
        {
            StreamWriter streamWriter = new StreamWriter(streamForWork);
            string serialText = "";

            for (int i = 0; i < objectForSerializing.Count; i++)
            {
                string stringWithInformationOfObject = "";
                FormObjectsLine(objectForSerializing[i], "", ref stringWithInformationOfObject);
                serialText += stringWithInformationOfObject;
            }

            if (pluginName == null)
            {
                streamWriter.WriteLine(serialText);
            }
            else
            {
                streamWriter.WriteLine(pluginName.Encript(System.Text.Encoding.UTF8.GetBytes(serialText)));
            }

            streamWriter.Close();
        }

        public List<T> DeserializeWithPlugin<T>(string stringForDeserializing, PluginClass pluginName)
        {
            bool isCorrectType = false;
            List<T> ListWithResults = new List<T>();
            List<string> stringForDecript = new List<string>();
            string helpString;
            string decriptText;

            using (StreamReader streamReader = File.OpenText(stringForDeserializing))
            {
                while ((helpString = streamReader.ReadLine()) != null)
                {
                    stringForDecript.Add(helpString);
                }
            }

            decriptText = pluginName.Decript(String.Join("",stringForDecript));

            StringReader stringReader = new StringReader(decriptText);
            
            string strr;

            T ResultObject = (T)Activator.CreateInstance(typeof(T));
            Type ObjType = ResultObject.GetType();

            while ((strr = stringReader.ReadLine()) != null)
            {
                string lineName = "";
                string lineVal = "";

                if (strr.Contains(">") && !strr.Contains("\'"))
                {
                    lineName = strr.Replace("\t", "");
                    lineName = lineName.Replace(" ", "");
                    lineName = lineName.Replace(">", "");
                }

                if (strr.Contains(":"))
                {
                    string[] s = strr.Split(':');

                    lineName = s[0].Replace("\t", "");
                    lineName = lineName.Replace(" ", "");

                    if (s[1].Contains("\'"))
                    {
                        lineVal = strr.Split('\'')[1];
                    }
                    else
                    {
                        lineVal = s[1].Replace("\t", "");
                        lineVal = lineVal.Replace(" ", "");
                    }
                }

                if (strr.Contains("<") && !strr.Contains("\'"))
                {
                    lineName = strr.Replace("\t", "");
                    lineName = lineName.Replace(" ", "");
                    lineName = lineName.Replace("<", "");

                    if (lineName == ObjType.Name)
                    {
                        ListWithResults.Add(ResultObject);
                        ResultObject = (T)Activator.CreateInstance(typeof(T));
                        continue;
                    }
                }

                if (lineName == ObjType.Name)
                {
                    isCorrectType = true;
                }
                else
                {
                    PropertyInfo[] pi = ObjType.GetProperties();
                    foreach (PropertyInfo p in pi)
                    {
                        if ((p.PropertyType.IsPrimitive || p.PropertyType.Name == "String" ||
                            p.PropertyType.Name == "Int32" || p.PropertyType.Name == "Int64" ||
                            p.PropertyType.Name == "Long")
                            )
                        {
                            if (p.Name == lineName)
                            {
                                if (p.PropertyType.Name == "String")
                                {
                                    p.SetValue(ResultObject, lineVal, null);
                                    break;
                                }
                                if (p.PropertyType.Name == "Int32")
                                {
                                    p.SetValue(ResultObject, Int32.Parse(lineVal), null);
                                    break;
                                }
                                if (p.PropertyType.Name == "Int64")
                                {
                                    p.SetValue(ResultObject, Int64.Parse(lineVal), null);
                                    break;
                                }
                                if (p.PropertyType.Name == "Long")
                                {
                                    p.SetValue(ResultObject, long.Parse(lineVal), null);
                                    break;
                                }
                            }
                        }
                        else if (lineName == p.PropertyType.Name)
                        {
                            doFind<T>(ref ResultObject, ref stringReader, strr, p);
                        }
                    }
                }
            }

            stringReader.Close();

            if (isCorrectType)
            {
                return ListWithResults;
            }
            else
            {
                return default(List<T>);
            }
        }
    }
}
