// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[]args)
    {
        Random random = new Random();
        List<int>list=new List<int>();
        for(int i=0;i<100;i++)
        {
            int n=random.Next(1000);
            list.Add(n);
        }
        Console.WriteLine("排序前");
        for(int i=0;i<list.Count;i++)
        {
            Console.WriteLine(list[i]);
        }
        Console.WriteLine("排序后");
        var query=from s in list
                  orderby s descending
                  select s;
        foreach(int i in query)
            Console.WriteLine(i);
        int sum=query.Sum();
        int count=query.Count();
        int avr=sum/count;
        Console.WriteLine("sum is:"+sum);
        Console.WriteLine("average is" + avr);
        
    }
}
