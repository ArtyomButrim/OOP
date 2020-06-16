using Plugin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players
{
    interface MainSerializer
    {
        void Serialize(List<object> o, Stream stream);
        List<T> Deserialize<T>(string str);

        void SerializeWithPlugin(List<object> objectForSerializing, Stream streamForWork, PluginClass pluginName);
        List<T> DeserializeWithPlugin<T>(string stringForDeserializing, PluginClass pluginName);

        string GetExtention();
    }
}
