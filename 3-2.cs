// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public delegate void TickHandler(object sender, DateTime time);
public delegate void AlarmHandler(object sender, DateTime alarmtime);
public class Button
{

    
    public event TickHandler Tick;
    public event AlarmHandler Alarm;

    public void Click()
    {
        DateTime time=DateTime.Now;
        Random random = new Random((int)(DateTime.Now.Ticks));
        int hour = random.Next(0, 12);
        int minute = random.Next(0, 60);
        int second = random.Next(0,60);
        string tempstr=string.Format("{0} {1}:{2}:{3}",DateTime.Now.ToString("yyyy-MM-dd"),hour,minute,second);
        DateTime alarmtime = Convert.ToDateTime(tempstr);

        Tick(this, time);
        if(time.ToString() == alarmtime.ToString())
        {
            Alarm(this, alarmtime);
        }
        
    }
    
}
public class Form
{
    public Button button1=new Button();

    public Form()
    {
        button1.Tick += Tick1;
        button1.Alarm += Alarm1;
    }
    public void Tick1(object sender, DateTime time)
    {
        Console.WriteLine("Now time is" + time);
    }

    public void Alarm1(object sender, DateTime alarmtime)
    {
        Console.WriteLine("The alarm clock you set has arrived");
    }

}

class Program
{
    static void Main(string[]args)
    {
        Form form = new Form();
        while(true)
        {
            form.button1.Click();
            System.Threading.Thread.Sleep(1000);
        }
    }
}
