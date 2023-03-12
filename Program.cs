// See https://aka.ms/new-console-template for more information
using System;

class Program
{
    public void Primnum(int n)
    {
        for (int i = 2; i < n; i++)
        {
            if (n % i == 0)
                return;
        }
        Console.WriteLine(n);
    }
    static void Main(string[] args)
    {
        int m = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("The Primenumber of " + m + " is:");
        for (int j = 2; j < m; j++)
        {
            if (m % j == 0)
            {
                Program program = new Program();
                program.Primnum(j);
            }

        }

    }

}
