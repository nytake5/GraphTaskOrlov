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
            Graph graph = new Graph(@"C:\Users\orlovda\source\repos\graph\GraphTaskOrlov\GraphTask3lvl\input.txt");

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
                Console.WriteLine("5 - сохранить как");
                int k = int.Parse(Console.ReadLine());
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
                        graph.SaveAs();
                        break;
                    default:
                        Console.WriteLine("");
                        break;
                }
            }
        }
    }
}
