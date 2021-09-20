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
            public string name { get; set; }
            public Dictionary<object, object> inf { get; set; }
            public Node() { }
            public Node(string name)
            {
                this.name = name;
                this.inf = new Dictionary<object, object>();
            }
            public Node(string name, Dictionary<object, object> inf)
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

        public Dictionary<string ,Node> nodes;

        public Graph(string name)
        {
            Dictionary<object, object> infTemp = new Dictionary<object, object>();
            nodes.Add(name)
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
                    Node tempNode = new Node(vs[0], infTemp);
                    nodes.Add(tempNode);
                }
            }
        }

        public Graph(Graph graph)
        {
            this.name += graph.name;
            foreach (var item in graph.nodes)
            {
                string nameNodes = "";
                nameNodes += item.name;
                Dictionary<object, object> infTemp = new Dictionary<object, object>();
                foreach (var el in item.inf)
                {
                    infTemp.Add(el.Key, el.Value);
                }
                this.nodes.Add(new Node(nameNodes, infTemp));
            }
        }
        // make bool result
        public void AddNode(string line)
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

        public void AddRebro(string line)
        {
            string[] vs = line.Split(' ');
            //будем добавлять по названию вершины в графе: "имя вершины название_вершины/вес" 
            foreach (var item in this.nodes)
            {
                if (item.name.Equals(vs[0]))
                {
                    string[] vsTemp = vs[1].Split('/'); 
                    item.inf.Add(vsTemp[0], vsTemp[1]);
                }
            }
        }

        public void RemoveNode(string line)
        {
            //будем удалять по названию вершины в графе
            Node removeItem = new Node();
            bool flag = false;
            foreach (var item in this.nodes)
            {
                if (item.name.Equals(line))
                {
                    removeItem = item;
                    flag = true;
                }
            }
            this.nodes.Remove(removeItem);
            if (!flag)
            {
                throw new Exception("Данные отсутствут");
            }
        }

        public void RemoveRebro(string line)
        {
            string[] vs = line.Split(' ');
            bool flag = false;
            //будем удалять по названию вершины в графе: "имя вершины название_вершины"
            foreach (var item in this.nodes)
            {
                if (item.name.Equals(vs[0]))
                {
                    KeyValuePair<object, object> removeItem = new KeyValuePair<object, object>();
                    foreach (var el in item.inf)
                    {
                        if (((string)el.Key).Equals(vs))
                        {
                            removeItem = el;
                            flag = true;
                        }
                    }
                    item.inf.Remove(removeItem);
                }
            }
            if (!flag)
            {
                throw new Exception("Данные отсутствут");
            }
        }

        public void SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
            {
                streamWriter.WriteLine(this.name);
                foreach (var item in this.nodes)
                {
                    streamWriter.WriteLine(item.ToString());
                }       
            }
        }
    }
}
