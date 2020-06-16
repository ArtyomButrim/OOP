using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin;

namespace Base64
{
    public class Base64 : PluginClass
    {
        public string Encript(byte[] stringForEncripting)
        {
            try
            {
                string resultText = Convert.ToBase64String(stringForEncripting);
                return resultText;
            }
            catch(Exception error)
            {
                throw new Exception("Ошибка при попытке закодировать информация кодированием Base64\n" + error.Message);
            }
        }

        public string Decript(string stringForDecripting)
        {
            try
            {
                string resultText = Encoding.UTF8.GetString(Convert.FromBase64String(stringForDecripting));
                return resultText;
            }
            catch(Exception error)
            {
                throw new Exception("Ошибка при попытке раскодировать информацию кодированием Base64\n" + error.Message);
            }
        }

        public string getExtention()
        {
            return "64";
        }
    }
}
