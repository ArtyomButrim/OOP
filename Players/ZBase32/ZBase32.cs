using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin;

namespace ZBase32
{
    public class ZBase32 : PluginClass 
    {
        private const string alphabet = "ybndrfg8ejkmcpqxot1uwisza345h769";

        private readonly byte[] DecodingTable = new byte[128];

        public ZBase32()
        {
            for (var i = 0; i < DecodingTable.Length; ++i)
            {
                DecodingTable[i] = byte.MaxValue;
            }

            for (var i = 0; i < alphabet.Length; ++i)
            {
                DecodingTable[alphabet[i]] = (byte)i;
            }
        }

        public string Encript(byte[] stringForEncripting)
        {
            try
            {
                if (stringForEncripting == null)
                {
                    throw new Exception("Не было передано данных для кодирования в ZBase32!");
                }

                var resultString = new StringBuilder((int)Math.Ceiling(stringForEncripting.Length * 8.0 / 5.0));

                for (var i = 0; i < stringForEncripting.Length; i += 5)
                {
                    var countOfBytesAtResultString = Math.Min(5, stringForEncripting.Length - i);

                    ulong buffer = 0;
                    for (var j = 0; j < countOfBytesAtResultString; ++j)
                    {
                        buffer = (buffer << 8) | stringForEncripting[i + j];
                    }

                    var countOfBitsAtResultString = countOfBytesAtResultString * 8;

                    while (countOfBitsAtResultString > 0)
                    {
                        var index = countOfBitsAtResultString >= 5 ? (int)(buffer >> (countOfBitsAtResultString - 5)) & 0x1f : (int)(buffer & (ulong)(0x1f >> (5 - countOfBitsAtResultString))) << (5 - countOfBitsAtResultString);
                        resultString.Append(alphabet[index]);
                        countOfBitsAtResultString = countOfBitsAtResultString - 5;
                    }
                }

                return resultString.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при кодировании ZBase32 \n" + ex.Message);
            }
        }

        public string Decript(string stringForDecripting)
        {
            try
            {
                if (stringForDecripting == string.Empty)
                {
                    return "";
                }

                var resultString = new List<byte>((int)Math.Ceiling(stringForDecripting.Length * 5.0 / 8.0));

                var index = new int[8];

                for (var i = 0; i < stringForDecripting.Length;)
                {
                    i = CreateIndexByOctetAndMovePosition(ref stringForDecripting, i, ref index);

                    var shortByteCount = 0;
                    ulong buffer = 0;

                    for (var j = 0; j < 8 && index[j] != -1; ++j)
                    {
                        buffer = (buffer << 5) | (ulong)(DecodingTable[index[j]] & 0x1f);
                        shortByteCount++;
                    }

                    var bitCount = shortByteCount * 5;

                    while (bitCount >= 8)
                    {
                        resultString.Add((byte)((buffer >> (bitCount - 8)) & 0xff));
                        bitCount -= 8;
                    }
                }

                string result = System.Text.Encoding.ASCII.GetString(resultString.ToArray());
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception("Ошибка при декодировании ZBase32 \n" + ex.Message);
            }
        }

        private int CreateIndexByOctetAndMovePosition(ref string data, int currentPosition, ref int[] index)
        {
            var j = 0;
            while (j < 8)
            {
                if (currentPosition >= data.Length)
                {
                    index[j++] = -1;
                    continue;
                }

                if (IgnoredSymbol(data[currentPosition]))
                {
                    currentPosition++;
                    continue;
                }

                index[j] = data[currentPosition];
                j++;
                currentPosition++;
            }

            return currentPosition;
        }

        private bool IgnoredSymbol(char checkedSymbol)
        {
            return checkedSymbol >= DecodingTable.Length || DecodingTable[checkedSymbol] == byte.MaxValue;
        }

        public string getExtention()
        {
            return "z32";
        }
    }
}
