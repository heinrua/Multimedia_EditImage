using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPT_BTL
{
    internal class HuffmanCompression
    {
        private class Node
        {
            public char Character { get; set; }
            public int Frequency { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        private Dictionary<char, string> huffmanTable;

        public HuffmanCompression()
        {
            huffmanTable = new Dictionary<char, string>();
        }

        // Nén chuỗi
        public string Compress(string input)
        {
            Dictionary<char, int> frequencyTable = BuildFrequencyTable(input);
            Node root = BuildHuffmanTree(frequencyTable);
            BuildHuffmanTable(root, "");
            StringBuilder encodedData = new StringBuilder();

            // Nén chuỗi đầu vào theo bảng mã Huffman
            foreach (char c in input)
            {
                encodedData.Append(huffmanTable[c]);
            }

            return encodedData.ToString();
        }

        // Giải nén chuỗi
        public string Decompress(string encodedData)
        {
            Dictionary<string, char> reverseTable = new Dictionary<string, char>();

            // Tạo bảng giải mã (đảo ngược bảng mã Huffman)
            foreach (var pair in huffmanTable)
            {
                reverseTable[pair.Value] = pair.Key;
            }

            StringBuilder decodedData = new StringBuilder();
            string currentCode = "";

            // Duyệt qua từng bit của chuỗi nén
            foreach (char bit in encodedData)
            {
                currentCode += bit;
                if (reverseTable.ContainsKey(currentCode))
                {
                    decodedData.Append(reverseTable[currentCode]);
                    currentCode = "";  // Xử lý phần mã vừa giải nén
                }
            }

            return decodedData.ToString();
        }

        // Xây dựng bảng tần suất từ chuỗi đầu vào
        private Dictionary<char, int> BuildFrequencyTable(string input)
        {
            Dictionary<char, int> frequencyTable = new Dictionary<char, int>();
            foreach (char c in input)
            {
                if (!frequencyTable.ContainsKey(c))
                    frequencyTable[c] = 0;

                frequencyTable[c]++;
            }

            return frequencyTable;
        }

        // Xây dựng cây Huffman từ bảng tần suất sử dụng List thay cho PriorityQueue
        private Node BuildHuffmanTree(Dictionary<char, int> frequencyTable)
        {
            List<Node> nodeList = new List<Node>();

            // Tạo danh sách các node ban đầu từ bảng tần suất
            foreach (var pair in frequencyTable)
            {
                Node node = new Node
                {
                    Character = pair.Key,
                    Frequency = pair.Value
                };
                nodeList.Add(node);
            }

            // Xây dựng cây Huffman bằng cách lấy hai node có tần suất thấp nhất
            while (nodeList.Count > 1)
            {
                // Sắp xếp danh sách theo tần suất
                nodeList.Sort((x, y) => x.Frequency.CompareTo(y.Frequency));

                // Lấy hai node có tần suất thấp nhất
                Node left = nodeList[0];
                Node right = nodeList[1];

                // Tạo một node cha mới
                Node parent = new Node
                {
                    Character = '\0',  // '0' chỉ là một ký tự không có ý nghĩa
                    Frequency = left.Frequency + right.Frequency,
                    Left = left,
                    Right = right
                };

                // Loại bỏ hai node đã lấy và thêm node cha vào danh sách
                nodeList.RemoveAt(0);
                nodeList.RemoveAt(0);
                nodeList.Add(parent);
            }

            // Trả về cây Huffman hoàn chỉnh
            return nodeList[0];
        }

        // Xây dựng bảng mã Huffman từ cây Huffman
        private void BuildHuffmanTable(Node node, string code)
        {
            if (node == null)
                return;

            if (node.Left == null && node.Right == null)
            {
                // Nếu là lá, lưu mã Huffman cho ký tự
                huffmanTable[node.Character] = code;
            }

            BuildHuffmanTable(node.Left, code + "0");  // Thêm '0' cho nhánh trái
            BuildHuffmanTable(node.Right, code + "1"); // Thêm '1' cho nhánh phải
        }

        // Trả về bảng mã Huffman
        public Dictionary<char, string> GetHuffmanTable()
        {
            return huffmanTable;
        }
    }
}
