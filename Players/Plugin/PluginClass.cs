using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    public interface PluginClass
    {
        string Encript(byte[] stringForEncripting);

        string Decript(string stringForDecripting);

        string getExtention();
    }
}
