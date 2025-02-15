using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPT_BTL
{
    internal class LZW
    {

        public List<int> Compress(byte[] input)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for (int i = 0; i < 256; i++) dictionary.Add(((char)i).ToString(), i);

            string current = string.Empty;
            List<int> encoded = new List<int>();
            int nextCode = 256;

            foreach (byte b in input)
            {
                string combined = current + (char)b;
                if (dictionary.ContainsKey(combined)) current = combined;
                else
                {
                    encoded.Add(dictionary[current]);
                    dictionary.Add(combined, nextCode++);
                    current = ((char)b).ToString();
                }
            }

            if (!string.IsNullOrEmpty(current)) encoded.Add(dictionary[current]);
            return encoded;
        }

        public byte[] Decompress(List<int> compressed)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < 256; i++) dictionary.Add(i, ((char)i).ToString());

            int nextCode = 256;
            string current = dictionary[compressed[0]];
            List<byte> decompressed = new List<byte>();
            decompressed.AddRange(EncodingToBytes(current));

            for (int i = 1; i < compressed.Count; i++)
            {
                string entry;
                if (dictionary.ContainsKey(compressed[i])) entry = dictionary[compressed[i]];
                else if (compressed[i] == nextCode) entry = current + current[0];
                else throw new ArgumentException("Invalid compressed data.");

                decompressed.AddRange(EncodingToBytes(entry));
                dictionary.Add(nextCode++, current + entry[0]);
                current = entry;
            }

            return decompressed.ToArray();
        }

        private byte[] EncodingToBytes(string input)
        {
            List<byte> bytes = new List<byte>();
            foreach (char c in input) bytes.Add((byte)c);
            return bytes.ToArray();
        }
    }
}
