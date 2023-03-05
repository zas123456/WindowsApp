using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            int a = int.Parse(Console.ReadLine());
            char c = char.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            int d;
            switch (c)
            {
                case '+':
                    d = a + b;
                    Console.WriteLine(d);
                    break;
                case '-':
                    d = a - b;
                    Console.WriteLine(d);
                    break;
                case '*':
                    d = a * b;
                    Console.WriteLine(d);
                    break;
                case '/':
                    d = a / b;
                    Console.WriteLine(d);
                    break;
                case '%':
                    d = a % b;
                    Console.WriteLine(d);
                    break;
                default:
                    Console.WriteLine("the import is wrong");
                    break;
            }
;            Console.ReadKey();
        }
    }
}
