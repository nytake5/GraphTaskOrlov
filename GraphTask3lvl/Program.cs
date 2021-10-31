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
            Graph graph = new Graph(@"C:\Users\denzi\source\repos\GraphTask3lvl\GraphTask3lvl\input.txt");

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
                        string[] vs = graph.ShortLength(tmp);
                        for (int i = 0; i < vs.Length; i++)
                        {
                            Console.Write(vs[i] + " ");
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
                    default:
                        Console.WriteLine("Попробуйте снова");
                        break;
                }
            }
        }
    }
}
