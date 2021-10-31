﻿    using System;
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
        const int INFINT = int.MaxValue;
        const double INFDOUBLE = double.MaxValue;
        public class Node
        {
            public Dictionary<string, object> inf;
            public Node()
            {
                this.inf = new Dictionary<string, object>();
            }
            public Node(Dictionary<string, object> inf)
            {
                this.inf = inf;
            }
            public Node(Node node)
            {
                this.inf = new Dictionary<string, object>(node.inf);
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
        private Dictionary<string, bool> nov = new Dictionary<string, bool>();

        public void InitialNov() 
        {
            foreach (var item in this.nodes)
            {
                nov.Add(item.Key, true);
            }
        }
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
                    Dictionary<string, object> infTemp = new Dictionary<string, object>();
                    for (int i = 1; i < vs.Length; i++)
                    {
                        string[] vsTemp = vs[i].Split('/');
                        infTemp.Add(vsTemp[0], vsTemp[1]);
                    }
                    Node tempNode = new Node(infTemp);
                    nodes.Add(vs[0], tempNode);
                }
            }
            InitialNov();
        }
        public Graph()
        {
            name = "";
            nodes = new Dictionary<string, Node>();
        }
        public Graph Copy()
        {
            Graph graphAns = new Graph();
            graphAns.name = String.Copy(this.name);
            foreach (var item in this.nodes)
            {
                string nameNodes = "";
                nameNodes += item.Key;
                Node valueNodes = new Node(item.Value);
                graphAns.nodes.Add(nameNodes, valueNodes);
            }
            graphAns.InitialNov();
            return graphAns;
        }


        public void NovSet() //метод помечает все вершины графа как непросмотреные
        {
            foreach (var item in nodes)
            {
                nov[item.Key] = true;
            }
        }

        public void Dfs(string v)
        {
            object o = new object();
            nov[v] = false; //помечаем ее как просмотренную
                            // в матрице смежности просматриваем строку с номером v
            foreach (var u in nodes.Keys)
            {
                //если вершины v и u смежные, к тому же вершина u не просмотрена,
                if ((this.FindRebro(v, u, out o)) && nov[u])
                {
                    Dfs(u); // то рекурсивно просматриваем вершину
                }
            }
        }


        public bool AddNode(string line)
        {
            char[] masSplit = { '/', '-', '.', ',' };
            bool flag = true;
            string[] vs = line.Split(' ');
            if (this.FindNode(vs[0]))
            {
                return false;
            }
            Dictionary<string, object> infTemp = new Dictionary<string, object>();
            for (int i = 1; i < vs.Length; i++)
            {
                string[] vsTemp = vs[i].Split(masSplit);
                if (vs[i] == "")
                {
                    break;
                }
                infTemp.Add(vsTemp[0], vsTemp[1]);
            }
            if (flag)
            {
                this.nodes.Add(vs[0], new Node(infTemp));
            }

            return flag;
        }
        
        public string[,] MatrixSmej()
        {
            string[] name = nodes.Keys.ToArray();
            string[,] arr = new string[nodes.Count + 1, nodes.Count + 1];
            for (int i = 0; i < nodes.Count; i++)
            {
                arr[0, i + 1] = name[i];
                arr[i + 1, 0] = name[i];
            }
            if (isOrient)
            {
                for (int i = 1; i < nodes.Count + 1; i++)
                {
                    for (int j = 1; j < nodes.Count + 1; j++)
                    {
                        object weight = new object();
                        if (this.FindRebro(arr[i, 0], arr[0, j], out weight))
                        {
                            arr[i, j] = weight.ToString();
                        }
                        else
                        {
                            arr[i, j] = "0";
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i < nodes.Count + 1; i++)
                {
                    for (int j = 1; j < nodes.Count + 1; j++)
                    {
                        object weight = new object();
                        if (this.FindRebro(arr[i, 0], arr[0, j], out weight))
                        {
                            arr[i, j] = weight.ToString();
                        }
                        else if (this.FindRebro(arr[0, j], arr[i, 0], out weight))
                        {
                            arr[i, j] = weight.ToString();
                        }
                        else
                        {
                            arr[i, j] = "0";
                        }
                    }
                }
            }
           

            return arr;
        }
        public bool AddRebro(string line)
        {
            bool flag = false;
            string[] vs = line.Split(' ');
            //будем добавлять по названию вершины в графе: "имя вершины название_вершины/вес" 
            if (isOrient)
            {
                foreach (var item in this.nodes)
                {
                    if (item.Key == vs[0])
                    {
                        string[] vsTemp = vs[1].Split('/');
                        object o = new object();
                        if (this.FindRebro(vs[0], vsTemp[0], out o))
                        {
                            return false;
                        }
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
            }
            else
            {
                foreach (var item in this.nodes)
                {
                    string[] vsTemp;
                    if (item.Key == vs[0])
                    {
                        vsTemp = vs[1].Split('/');
                        object o = new object();
                        if (this.FindRebro(vs[0], vsTemp[0], out o))
                        {
                            return false;
                        }
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
                    vsTemp = vs[1].Split('/');
                    if (item.Key == vsTemp[0])
                    {
                        object o = new object();
                        if (this.FindRebro(vsTemp[0], vs[0], out o))
                        {
                            return false;
                        }
                        try
                        {
                            item.Value.inf.Add(vs[0], vsTemp[1]);
                            flag = true;
                        }
                        catch
                        {
                            flag = false;
                        }
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

        public bool FindNode(string nameNode)
        {
            Node node = new Node();
            return nodes.TryGetValue(nameNode, out node);
        }

        public bool FindRebro(string nameNode, string nameRebr, out object weight)
        {
            Node node = new Node();
            if (nodes.TryGetValue(nameNode, out node))
            { 
                return node.inf.TryGetValue(nameRebr, out weight);
            }
            else
            {
                weight = null;
                return false;
            }
        }

        public Node FirstOrDefailt() 
        {
            Node node = new Node();
            if (nodes.Count == 0)
            {
                return new Node();
            }
            else
            {
                return nodes.Values.FirstOrDefault();
            }
        }

        public int[,] FloydForArcs()
        {
            string[,] temp = MatrixSmej();
            int[,] array = new int[temp.GetLength(0) - 1, temp.GetLength(1) - 1];
            for (int l = 0; l < temp.GetLength(0) - 1; l++)
            {
                for (int q = 0; q < temp.GetLength(1) - 1; q++)
                {
                    array[l, q] = int.Parse(temp[l + 1, q + 1]);
                }
            }
            int i, j, k;
            int[,] a = new int[nodes.Count, nodes.Count];
            for (i = 0; i < nodes.Count; i++)
            {
                for (j = 0; j < nodes.Count; j++)
                {
                    if (i == j)
                    {
                        a[i, j] = 0;
                    }
                    else
                    {
                        if (array[i, j] == 0)
                        {
                            a[i, j] = int.MaxValue;
                        }
                        else
                        {
                            a[i, j] = 1;
                        }
                    }
                }
            }
            //осуществляем поиск кратчайших путей
            for (k = 0; k < nodes.Count; k++)
            {
                for (i = 0; i < nodes.Count; i++)
                {
                    for (j = 0; j < nodes.Count; j++)
                    {
                        int distance = a[i, k] + a[k, j];
                        if (a[i, j] > distance)
                        {
                            a[i, j] = distance;
                        }
                    }
                }
            }
            return a;//в качестве результата возвращаем массив кратчайших путей между
        } //всеми парами вершин

        public void SaveAs()
        {
            string FileName = @"C:\Users\denzi\source\repos\GraphTask3lvl\GraphTask3lvl\output.txt";
            using (StreamWriter streamWriter = new StreamWriter(FileName))
            {
                if (isOrient)
                {
                    if (isSuspend)
                    {
                        streamWriter.WriteLine(this.name + " " + "1" + " " + "1");
                    }
                    else
                    {
                        streamWriter.WriteLine(this.name + " " + "1" + " " + "0");
                    }
                }
                else
                {
                    if (isSuspend)
                    {
                        streamWriter.WriteLine(this.name + " " + "0" + " " + "1");
                    }
                    else
                    {
                        streamWriter.WriteLine(this.name + " " + "0" + " " + "0");
                    }
                }
                foreach (var item in this.nodes)
                {
                    streamWriter.WriteLine(item.Key + " " + item.Value.ToString());
                }
            }
        }

        //13 task 1a 1
        public string[] FindZahodIshod(string nameNode)
        {
            HashSet<string> arr = new HashSet<string>();
            foreach (var item in nodes)
            {
                object o = new object();
                if (this.FindRebro(item.Key, nameNode, out o) && this.FindRebro(nameNode, item.Key, out o))
                {
                    arr.Add(item.Key);
                }       
            }
            return arr.ToArray();
        }
        //14 task 1a 2
        public string[] FindSmej(string nameNode)
        {
            HashSet<string> arr = new HashSet<string>();
            foreach (var item in nodes)
            {
                object o = new object();
                if (this.FindRebro(item.Key, nameNode, out o) || this.FindRebro(nameNode, item.Key, out o))
                {
                    arr.Add(item.Key);
                }
            }
            return arr.ToArray();
        }
        //4 task 1b
        public Graph Reverse()
        {
            Graph ans = new Graph();
            ans.name = String.Copy(this.name) + " " + "reverse";
            foreach (var item in this.nodes)
            {
                ans.nodes.Add(String.Copy(item.Key), new Node());
            }
            foreach (var item in nodes)
            {
                foreach (var itemInf in item.Value.inf)
                {
                    ans.nodes[(string)itemInf.Key].inf.Add(item.Key, itemInf.Value);
                }
            }
            return ans;
        }

        //7 task II 
        public bool isHaveRoot()
        {
            foreach (var item in nodes.Keys)
            {
                Dfs(item);
                bool flag = true;
                foreach (var item2 in nov)
                {
                    if (item2.Value == true)
                    {
                        flag = false;
                    }
                }
                if (flag == true)
                {
                    return true;
                }
                NovSet();
            }
            return false;
        }

        //31 task II 
        public string[] ShortLength(string u)
        {
            string[] ans = new string[nodes.Count];
            int k = 0;
            foreach (var item in nodes)
            {
                if (item.Key == u)
                {
                    break;
                }
                k++;
            }
            int[,] temp = FloydForArcs();
            for (int i = 0; i < nodes.Count; i++)
            {
                ans[i] = temp[i, k].ToString();
            }
            return ans;
        }

        //Прим task III
        public Graph AlgorithmPrima() 
        {
            Graph graphAns = new Graph
            {
                isOrient = false
            };
            Graph currGraph = this.Copy();
            currGraph.NovSet();
            graphAns.isSuspend = currGraph.isSuspend;
            graphAns.name = String.Copy(currGraph.name + " Frame");
            string line = currGraph.nodes.Keys.Last();

            graphAns.nodes.Add(String.Copy(line), new Node());
            int i = 0;
            while (graphAns.nodes.Count < currGraph.nodes.Count  //пока количество вершин меньше чем в исходном 
                && i < (Math.Pow(currGraph.nodes.Count, 2) + 1)) // пока не произошло n*n итераций, так как это максимально 
            {                                               // количество итераций
                string minName = "";
                int min = INFINT;
                foreach (var item in currGraph.nodes[line].inf)
                {
                    int curr = int.Parse(item.Value.ToString());
                    if (curr < min && currGraph.nov[item.Key])
                    {
                        minName = String.Copy(item.Key);
                        min = curr;
                    }
                }
                if (min != INFINT) 
                {  
                    currGraph.nov[minName] = false;
                    graphAns.AddRebro(String.Copy(line) + " " + minName + "/" + min.ToString());
                    graphAns.AddNode(String.Copy(minName) + " " + line + "/" + min.ToString());
                    
                    currGraph.RemoveRebro(String.Copy(line) + " " + minName); 
                    line = minName;
                }
                else
                {
                    foreach (var item in nov)
                    {
                        bool flag = false;
                        if (!item.Value)
                        {
                            foreach (var elem in nov)
                            {
                                if (elem.Value)
                                {
                                    object o = new object();
                                    flag = currGraph.FindRebro(item.Key, elem.Key, out o);
                                    if (flag)
                                    {
                                        int curr = int.Parse(o.ToString());
                                        if (min < curr)
                                        {
                                            minName = elem.Key;
                                            min = curr;
                                        }
                                    }
                                }
                            }
                            if (min != INFINT && flag)
                            {
                                currGraph.nov[minName] = false;
                                graphAns.AddNode(String.Copy(item.Key) + " ");
                                graphAns.AddRebro(String.Copy(minName) + " " + item.Key + "/" + min.ToString());
                                currGraph.RemoveRebro(String.Copy(line) + " " + minName);
                                line = minName;
                            }
                        }
                    }
                }
                i++;
            }
            return graphAns;
        }
        public Dictionary<string, Node> GetGraph()
        {
            return nodes;
        }
    }
}
