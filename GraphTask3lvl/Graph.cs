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
            public Dictionary<object, object> inf { get; set; }
            public Node()
            {
                this.inf = new Dictionary<object, object>();
            }
            public Node(Dictionary<object, object> inf)
            {
                this.inf = inf;
            }
            public Node(Node node)
            {
                this.inf = new Dictionary<object, object>(node.inf);
            }

            public override string ToString()
            {
                StringBuilder res = new StringBuilder();
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
        public Dictionary<string ,Node> nodes;

        public Graph(string name)
        {
            this.name = name;
            nodes = new Dictionary<string, Node>();
        }
        public Graph()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            using (StreamReader streamReader = new StreamReader(openFileDialog.FileName))
            {
                string line;
                line = streamReader.ReadLine() ?? null;
                if (line == null)
                {
                    this.name = line;
                }
                else
                { 
                    throw new Exception("Данные отсутствут");
                    this.name = "";
                }
                    
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] vs = line.Split(' ');
                    Dictionary<object, object> infTemp = new Dictionary<object, object>();
                    for (int i = 1; i < vs.Length; i++)
                    {
                        string[] vsTemp = vs[i].Split('/');
                        infTemp.Add(vsTemp[0], vsTemp[1]);
                    }
                    Node tempNode = new Node(infTemp);
                    nodes.Add(vs[0], tempNode);
                }
            }
        }

        public Graph(Graph graph)
        {
            this.name += graph.name;
            foreach (var item in graph.nodes)
            {
                string nameNodes = "";
                
                nameNodes += item.Key;
                Node valueNodes = new Node(item.Value);
                this.nodes.Add(nameNodes, valueNodes);
            }
        }
        public bool AddNode(string line)
        {
            char[] masSplit = { '/', '-', '.', ',' };
            bool flag = false;
            string[] vs = line.Split(' ');
            Dictionary<object, object> infTemp = new Dictionary<object, object>();
            for (int i = 1; i < vs.Length; i++)
            {
                string[] vsTemp = vs[i].Split(masSplit);
                try
                { 
                    infTemp.Add(vsTemp[0], vsTemp[1]);
                    flag = true;
                }
                catch
                {
                    flag = false;
                }
            }
            return flag;
        }

        public void AddRebro(string line)
        {
            string[] vs = line.Split(' ');
            //будем добавлять по названию вершины в графе: "имя вершины название_вершины/вес" 
            foreach (var item in this.nodes)
            {
                if (item.Key == vs[0])
                {
                    string[] vsTemp = vs[1].Split('/');
                    item.Value.inf.Add(vsTemp[0], vsTemp[1]);
                }
            }
        }

        public bool RemoveNode(string line)
        {
            //будем удалять по названию вершины в графе
            Node removeItem = new Node();
            bool flag = false;
            foreach (var item in this.nodes)
            {
                if (item.Key.Equals(line))
                {
                    flag = true;
                    this.nodes.Remove(item.Key);
                }
            }
            return flag;
        }

        public bool RemoveRebro(string line)
        {
            string[] vs = line.Split(' ');
            bool flag = false;
            //будем удалять по названию вершины в графе: "имя вершины название_вершины"
            foreach (var item in this.nodes)
            {
                if (item.Key.Equals(vs[0]))
                {
                    KeyValuePair<object, object> removeItem = new KeyValuePair<object, object>();
                    foreach (var el in item.Value.inf)
                    {
                        if (((string)el.Key).Equals(vs))
                        {
                            removeItem = el;
                            flag = true;
                        }
                    }
                    item.Value.inf.Remove(removeItem);
                }
            }
            return flag;
        }

        public void SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
            {
                streamWriter.WriteLine(this.name);
                foreach (var item in this.nodes)
                {
                    streamWriter.WriteLine(item.Key + " " + item.Value.ToString());
                }       
            }
        }
    }
}
