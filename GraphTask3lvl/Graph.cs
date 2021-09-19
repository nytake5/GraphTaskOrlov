using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphTask3lvl
{
    class Graph
    {
        public class Node
        {
            public object name { get; set; }
            public Dictionary<object, object> inf { get; set; }
            public Node() { }
            public Node(object name)
            {
                this.name = name;
                this.inf = new Dictionary<object, object>();
            }
            public Node(object name, Dictionary<object, object> inf)
            {
                this.name = name;
                this.inf = inf;
            }

            public override string ToString()
            {
                StringBuilder res = new StringBuilder();
                res.Append(name);
                res.Append(" ");
                foreach (var item in inf)
                {
                    res.Append(item.Key);
                    res.Append("/");
                    res.Append(item.Value);
                    res.Append(" ");
                }
                return res.ToString();
            }
            public int Size
            {
                get {
                    return inf.Count;
                }
            }
        }

        public string name;
        public List<Node> nodes;

        public Graph()
        { }
        public Graph(string name)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            using (StreamReader streamReader = new StreamReader(openFileDialog.FileName))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] vs = line.Split(' ');
                    Dictionary<object, object> infTemp = new Dictionary<object, object>();
                    for (int i = 1; i < vs.Length; i++)
                    {
                        string[] vsTemp = vs[i].Split('/');
                        infTemp.Add(vsTemp[0], vsTemp[1]);
                    }
                    Node tempNode = new Node(vs[0], infTemp);
                    nodes.Add(tempNode);
                }
            }
        }

        public void Add(string line)
        {
            string[] vs = line.Split(' ');
            Dictionary<object, object> infTemp = new Dictionary<object, object>();
            for (int i = 1; i < vs.Length; i++)
            {
                string[] vsTemp = vs[i].Split('/');
                infTemp.Add(vsTemp[0], vsTemp[1]);
            }
            Node tempNode = new Node(vs[0], infTemp);
            nodes.Add(tempNode);
        }
    }
}
