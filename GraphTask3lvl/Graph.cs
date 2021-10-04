    using System;
using System.Collections;
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
            public Dictionary<object, object> inf;
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
                get
                {
                    return inf.Count;
                }
            }
        }

        public string name;
        private bool isOrient = true;
        private bool isSuspend = true;
        public Dictionary<string, Node> nodes;
        public Graph(string FileLine)
        {
            nodes = new Dictionary<string, Node>();
            using (StreamReader streamReader = new StreamReader(FileLine))
            {
                string line;
                line = streamReader.ReadLine() ?? null;
                string[] temp = line.Split();
                if (temp[0] != null)
                {
                    this.name = temp[0];
                    if (int.Parse(temp[1]) == 0)
                    {
                        this.isOrient = false;
                    }
                    if (int.Parse(temp[2]) == 0)
                    {
                        this.isSuspend = false;
                    }

                }
                else
                {
                    this.name = "";
                }
                nodes = new Dictionary<string, Node>();
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
        public Graph()
        {
            name = "";
            nodes = new Dictionary<string, Node>();
        }
        public Graph(Graph graph)
        {   
            this.name = String.Copy(graph.name);
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
            if (flag)
            {
                nodes.Add(vs[0], new Node(infTemp));
            }

            return flag;
        }

        public bool AddRebro(string line)
        {
            bool flag = false;
            string[] vs = line.Split(' ');
            //будем добавлять по названию вершины в графе: "имя вершины название_вершины/вес" 
            foreach (var item in this.nodes)
            {
                if (item.Key == vs[0])
                {
                    string[] vsTemp = vs[1].Split('/');

                    try
                    {
                        item.Value.inf.Add(vsTemp[0], vsTemp[1]);
                        flag = true;
                    }
                    catch
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        public bool RemoveNode(string line)
        {
            //будем удалять по названию вершины в графе
            Node removeItem = new Node();
            bool flag = false;
            string delItem = "";
            foreach (var item in this.nodes)
            {
                
                if (item.Key.Equals(line))
                {
                    flag = true;
                    delItem = item.Key;
                    this.RemoveRebro(item.Key + " " + line);
                }
            }
            if (flag)
            {
                nodes.Remove(delItem);
            }

            return flag;
        }

        public bool RemoveRebro(string line)
        {
            string[] vs = line.Split(' ');
            //будем удалять по названию вершины в графе: "имя вершины название_вершины"
            if (this.isOrient)
            { 
                foreach (var item in this.nodes)
                {
                    if (item.Key.Equals(vs[0]))
                    {
                        return item.Value.inf.Remove(vs[1]);
                    }
                }
                return false;
            }
            else
            {
                bool flag1 = false,
                    flag2 = false;

                foreach (var item in this.nodes)
                {
                    if (item.Key.Equals(vs[0]))
                    {
                        flag1 = item.Value.inf.Remove(vs[1]);
                    }
                    if (item.Key.Equals(vs[1]))
                    {
                        flag2 = item.Value.inf.Remove(vs[0]);
                    }
                }
                return flag1 & flag2;
            }
        }

        public void SaveAs()
        {
            string FileName = @"C:\Users\orlovda\source\repos\graph\GraphTaskOrlov\GraphTask3lvl\output.txt";
            using (StreamWriter streamWriter = new StreamWriter(FileName))
            {
                if (isOrient)
                {
                    if (isSuspend)
                    {
                        streamWriter.WriteLine(this.name + " " + "1" + " " + " 1");
                    }
                    else
                    {
                        streamWriter.WriteLine(this.name + " " + "1" + " " + " 0");
                    }
                }
                else
                {
                    if (isSuspend)
                    {
                        streamWriter.WriteLine(this.name + " " + "0" + " " + " 1");
                    }
                    else
                    {
                        streamWriter.WriteLine(this.name + " " + "0" + " " + " 0");
                    }
                }
                foreach (var item in this.nodes)
                {
                    streamWriter.WriteLine(item.Key + " " + item.Value.ToString());
                }
            }
        }

        public Dictionary<string, Node> GetGraph()
        {
            return nodes;
        }
    }
}
