using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTask3lvl
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(@"C:\Users\denzi\source\repos\GraphTask3lvl\GraphTask3lvl\input1.txt");

            string line = Console.ReadLine();
            while (!Console.ReadKey().Equals(ConsoleKey.Escape))
            {
                Console.WriteLine(graph.name);
                foreach (var item in graph.GetGraph())
                {
                    Console.WriteLine(item.Key + " " + item.Value.ToString());
                }
                Console.WriteLine("1 - добавить вершину");
                Console.WriteLine("2 - добавить ребро");
                Console.WriteLine("3 - удалить вершину");
                Console.WriteLine("4 - удалить ребро");
                Console.WriteLine("5 - показать матрицу смежности");
                Console.WriteLine("6 - сохранить как");
                Console.WriteLine("7 - Вывести те вершины орграфа, которые " +
                    "являются одновременно заходящими и выходящими для заданной вершины.");
                Console.WriteLine("8 - Вывести все вершины орграфа, смежные с данной.");
                Console.WriteLine("9 - Построить орграф, являющийся обращением данного орграфа");
                Console.WriteLine("10 - Определить, имеет ли данный ацикличный орграф корень");
                Console.WriteLine("11 - Вывести длины кратчайших (по числу дуг) путей от всех вершин до u");
                Console.WriteLine("12 - Вывести каркас минимального веса, полученный алгоритмом Прима");
                Console.WriteLine("13 - Найти вершину, сумма длин кратчайших путей от которой до остальных вершин минимальна.");
                Console.WriteLine("14 - Найти радиус графа — минимальный из эксцентриситетов его вершин.");
                Console.WriteLine("15 - Вывести кратчайшие пути из вершин u1 и u2 до v.");
                int k;
                int.TryParse(Console.ReadLine(), out k);
                switch (k)
                {
                    case 1:
                        Console.WriteLine("Введите в строке название вершины и список исходящих рёбер в" +
                            "в формате v/v");
                        line = Console.ReadLine();
                        if (graph.AddNode(line))
                        {
                            Console.WriteLine("Успешно!");
                        }
                        else
                        {
                            Console.WriteLine("Попробуйте ещё раз!");
                        }
                        Console.WriteLine("");
                        break;
                    case 2:
                        Console.WriteLine("Введите в строке название вершины и исходящее ребро в" +
                            "в формате v/v");
                        line = Console.ReadLine();
                        if (graph.AddRebro(line))
                        {
                            Console.WriteLine("Успешно!");
                        }
                        else
                        {
                            Console.WriteLine("Попробуйте ещё раз!");
                        }
                        Console.WriteLine("");
                        break;
                    case 3:
                        Console.WriteLine("Введите название вершины");
                        line = Console.ReadLine();
                        if (graph.RemoveNode(line))
                        {
                            Console.WriteLine("Успешно!");
                        }
                        else
                        {
                            Console.WriteLine("Попробуйте ещё раз!");
                        }
                        Console.WriteLine("");
                        break;
                    case 4:
                        Console.WriteLine("Введите название вершины и ребро");
                        line = Console.ReadLine();
                        if (graph.RemoveRebro(line))
                        {
                            Console.WriteLine("Успешно!");
                        }
                        else
                        {
                            Console.WriteLine("Попробуйте ещё раз!");
                        }
                        Console.WriteLine("");
                        break;
                    case 5:
                        string[,] arr = graph.MatrixSmej();
                        for (int i = 0; i < arr.GetLength(0); i++)
                        {
                            for (int j = 0; j < arr.GetLength(1); j++)
                            {
                                Console.Write(arr[i,j] + " ");
                            }
                            Console.WriteLine();
                        }
                        break;
                    case 6:
                        graph.SaveAs();
                        break;
                    case 7:
                        Console.WriteLine("Введите название вершины:");
                        string name = Console.ReadLine();
                        string[] arrAns = graph.FindZahodIshod(name);
                        foreach (var item in arrAns)
                        {
                            Console.Write(item + " ");
                        }
                        break;
                    case 8:
                        Console.WriteLine("Введите название вершины:");
                        name = Console.ReadLine();
                        arrAns = graph.FindSmej(name);
                        foreach (var item in arrAns)
                        {
                            Console.Write(item + " ");
                        }
                        break;
                    case 9:
                        Graph ans = graph.Reverse();
                        foreach (var item in ans.GetGraph())
                        {
                            Console.WriteLine(item.Key + " " + item.Value.ToString());
                        }
                        break;
                    case 10:
                        if (graph.isHaveRoot())
                        {
                            Console.WriteLine("Имеет!");
                        }
                        else
                        {
                            Console.WriteLine("Не имеет!");
                        }
                        break;
                    case 11:
                        Console.WriteLine("Введите название вершины:");
                        string tmp = Console.ReadLine();
                        while (!graph.FindNode(tmp))
                        {
                            Console.WriteLine("Попробуйте ещё раз");
                            tmp = Console.ReadLine();
                        }
                        Dictionary<string, int> vs = graph.ShortLength(tmp);
                        foreach (var item in vs)
                        {
                            Console.WriteLine(item.Key + ": " + item.Value.ToString());
                        }
                        break;
                    case 12:
                        Console.WriteLine("Каркас минимального веса:");
                        Graph graphAns = graph.AlgorithmPrima();
                        foreach (var item in graphAns.GetGraph())
                        {
                            Console.WriteLine(item.Key + " " + item.Value.ToString());
                        }
                        break;
                    case 13:
                        Console.WriteLine("Искомая вершина:");
                        Console.WriteLine(graph.ShortWayAtFloyd());
                        break;
                    case 14:
                        string s1, s2;
                        long rad = graph.FindRadius(out s1, out s2);
                        Console.WriteLine(s1 + " " + s2 + " " + rad.ToString());
                        break;
                    case 15:
                        Console.WriteLine("Введите три вершины через пробел");
                        line = Console.ReadLine();
                        string[] lineSplit = line.Split();
                        Dictionary<string, List<string>> ans15 = graph.ShortCut(lineSplit[0], lineSplit[1], lineSplit[2]);
                        for (int i = 0; i < 2; i++)
                        {
                            Console.WriteLine(lineSplit[i]);
                            foreach (var item in ans15[lineSplit[i]])
                            {
                                Console.Write(" " + item);
                            }
                            Console.WriteLine();
                        }
                        break;
                    default:
                        Console.WriteLine("Попробуйте снова");
                        break;
                }
            }
        }
    }
}
