using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

namespace Orderprogram
{
    [Serializable]
    public class OrderDetails
    {
        private string name;
        private int number;
        private double price;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Number
        {
            get { return number; }
            set
            {
                if (value > 0) number = value;
                else Console.WriteLine("数量不能小于0");
            }
        }
        public double Price
        {
            get { return price; }
            set
            {
                if (value > 0) price = value;
                else Console.WriteLine("价格不能小于0");
            }
        }
        public double getPrice()
        {
            return this.number * this.price;
        }

        public OrderDetails()
        {
            this.name = string.Empty;
            this.number = 0;
            this.price = 0;
        }
        public OrderDetails(string name, int number, double price)
        {
            this.name = name;
            this.number = number;
            this.price = price;
        }

        public override string ToString()
        {
            return name + " " + number + " " + price;
        }

        public override bool Equals(object obj)
        {
            OrderDetails a = obj as OrderDetails;
            return this.name == a.name;
        }

       

    }

    [Serializable]
    public class Order : IComparable
    {
        public int ID { get; set; }
        public string Customer { get; set; }
        public double Money { get; set; }
        public string Data { get; set; }

        public List<OrderDetails> orderdetails = new List<OrderDetails>();

        public int CompareTo(object obj)
        {
            Order a = obj as Order;
            return this.ID.CompareTo(a.ID);
        }
        public override bool Equals(object obj)
        {
            Order a = obj as Order;
            return this.ID == a.ID;
        }

        public override int GetHashCode()
        {
            return Convert.ToInt32(ID);
        }

        public override string ToString()
        {
            return ID + " " + Customer + " " + Money + " " + Data;
        }

        public Order(int id, string customer, string data)
        {
            this.ID = id;
            this.Customer = customer;
            this.Data = data;
        }

        public Order()
        {
            this.ID = 0;
            this.Customer=String.Empty;
            this.Money = 0;
            this.Data=String.Empty;
        }
        public void getmoney()
        {
            double i = 0;
            foreach (OrderDetails a in this.orderdetails)
            {
                i = i + a.getPrice();
            }
            this.Money = i;
        }

        public void AddOrderdetails(string name, int number, double price)//添加订单
        {
            OrderDetails a = new OrderDetails(name, number, price);
            int i = 0;
            foreach (OrderDetails b in this.orderdetails)
            {
                if (a == b)
                    i = 1;
            }
            if (i == 0)
                this.orderdetails.Add(a);
        }

        public void DeleteOrderdetails()
        {
            try
            {
                Console.WriteLine("请输入想要删除的订单明细号：");
                int a = Convert.ToInt32(Console.ReadLine());
                this.orderdetails.RemoveAt(a);
                Console.WriteLine("删除成功！");
            }
            catch
            {
                Console.WriteLine("输入订单明细号错误");
            }
        }

        public void ShowOrderdetails()
        {
            Console.WriteLine("名称 数量 单价");
            foreach (OrderDetails a in this.orderdetails)
            {
                Console.WriteLine(a);
            }
        }
    }

    public interface IOrderService
    {
        void addOrder();
        void deleteOrder();
        void searchOrder(int i);
        void changeOrder();
        void sortOrder();
        void export();
        void import();
    }

    [Serializable]
    public class OrderService : IOrderService
    {
        public List<Order> order = new List<Order>();

        public OrderService()
        {

        }

        public void addOrder()
        {
            try
            {
                Console.WriteLine("请输入订单编号：");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("请输入客户名称：");
                string customer = Console.ReadLine();
                Console.WriteLine("请输入时间：");
                string data = Console.ReadLine();
                Order a = new Order(id, customer, data);
                Console.WriteLine("开始添加订单项：");
                bool judge = true;
                bool same = false;
                foreach (Order m in this.order)
                {
                    if (m.Equals(a)) same = true;
                }
                if (same) Console.WriteLine("订单号重复");
                else
                {
                    while (judge && !same)
                    {
                        Console.WriteLine("请输入商品名称：");
                        string name = Console.ReadLine();
                        Console.WriteLine("请输入购买数量：");
                        int number = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("请输入商品单价：");
                        double price = Convert.ToDouble(Console.ReadLine());
                        a.AddOrderdetails(name, number, price);
                        Console.WriteLine("是否继续添加订单,是请按1，否则按0：");
                        int x = Convert.ToInt32(Console.ReadLine());
                        if (x == 0) judge = false;
                        else if (x == 1) continue;
                        else if (x != 1 && x != 0)
                        {
                            Exception e = new Exception();
                            throw e;
                        }
                    }
                    order.Add(a);
                    a.getmoney();
                    Console.WriteLine("添加订单成功！");
                }
            }
            catch
            {
                Console.WriteLine("输入错误");
            }
        }
        public void deleteOrder()
        {
            try
            {
                Console.WriteLine("输入订单号删除订单或者相应的明细：");
                int id = Convert.ToInt32(Console.ReadLine());
                int index = 0;
                foreach (Order a in order)
                {
                    if (a.ID == id) index = this.order.IndexOf(a);
                }
                Console.WriteLine("输入1删除订单，输入2进一步删除订单明细：");
                int choose = Convert.ToInt32(Console.ReadLine());
                if (choose == 1)
                {
                    this.order.RemoveAt(index);
                    Console.WriteLine("订单删除成功");
                }
                else if (choose == 2)
                {
                    this.order[index].ShowOrderdetails();
                    this.order[index].DeleteOrderdetails();
                }
                else
                {
                    Console.WriteLine("订单明细删除成功");
                }
            }
            catch
            {
                Console.WriteLine("输入错误");
            }
        }
        public void searchOrder(int i)
        {
            try
            {
                if (i == 1)//按照订单号查询
                {
                    Console.WriteLine("请输入要查询的订单号：");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var query1 = from s in order
                                 where s.ID == id
                                 orderby s.Money
                                 select s;
                    List<Order> a1 = query1.ToList();
                    foreach (Order a in a1)
                    {
                        Console.WriteLine(a);
                    }
                }
                if (i == 2)//按照客户名称查询
                {
                    Console.WriteLine("请输入要查询的客户名：");
                    string customer = Console.ReadLine();
                    var query2 = from s in order
                                 where s.Customer == customer
                                 orderby s.Money
                                 select s;
                    List<Order> a2 = query2.ToList();
                    foreach (Order a in a2)
                    {
                        Console.WriteLine(a);
                    }
                }
                if (i == 3)//按照订单总金额查询
                {
                    Console.WriteLine("请输入要查询的订单总金额：");
                    double money = Convert.ToDouble(Console.ReadLine());
                    var query3 = from s in order
                                 where s.Money == money
                                 select s;
                    List<Order> a3 = query3.ToList();
                    foreach (Order a in a3)
                    {
                        Console.WriteLine(a);
                    }
                }
                if (i == 4)//按照商品名称查询
                {
                    Console.WriteLine("请输入要查询的订单号：");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("请输入要查询的商品名称:");
                    string name = Console.ReadLine();
                    var query4 = from s in order
                                 where s.ID == id
                                 select s;
                    int index1 = 0;
                    foreach (Order a in query4)
                    {
                        if (a.orderdetails[index1].Name == name)
                        {
                            Console.WriteLine(a.orderdetails[index1]);
                        }
                        index1++;
                    }
                }
                else
                {
                    Console.WriteLine("输入错误");
                }
            }
            catch
            {
                Console.WriteLine("输入错误");
            }
        }
        public void changeOrder()
        {
            try
            {
                Console.WriteLine("请输入想要修改的订单号：");
                int id = Convert.ToInt32(Console.ReadLine());
                foreach (Order a in order)
                {
                    if (a.ID == id)
                    {
                        Console.WriteLine("请输入修改后的总金额：");
                        double money2 = Convert.ToDouble(Console.ReadLine());
                        a.Money = money2;
                    }
                }
            }
            catch
            {
                Console.WriteLine("输入错误");
            }
        }
        public void sortOrder()
        {
            try
            {
                Console.WriteLine("对订单按订单号排序：");
                order.Sort((p1, p2) => p1.ID - p2.ID);
                order.ForEach(p => Console.WriteLine(p));
                Console.WriteLine("订单排序已完成");
            }
            catch
            {
                Console.WriteLine("订单排序失败");
            }
        }

        public void export()
        {
            XmlSerializer a = new XmlSerializer(typeof(List<Order>));
            using (FileStream b = new FileStream("order.xml", FileMode.Create))
            {
                a.Serialize(b, this.order);
            }
            Console.WriteLine("序列化完成");
        }

        public void import()
        {
            XmlSerializer a = new XmlSerializer(typeof(List<Order>));
            using (FileStream b = new FileStream("order.xml", FileMode.Open))
            {
                List<Order> c = (List<Order>)a.Deserialize(b);
                Console.WriteLine("反序列化结果：");
                foreach (Order o in c)
                {
                    Console.WriteLine("订单号 客户 日期 总金额");
                    Console.WriteLine("{0} {1} {2} {3}", o.ID, o.Customer, o.Data, o.Money);
                    o.ShowOrderdetails();
                }
            }
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            OrderService orderService = new OrderService();
            bool prc = true;
            while (prc)
            {
                Console.WriteLine("输入1增加订单，输入2删除订单，输入3查询订单，输入4修改订单，输入5订单排序，输入6订单序列化，输入7订单反序列化，输入8退出");
                int choose1 = Convert.ToInt32((Console.ReadLine()));
                if (choose1 == 1) orderService.addOrder();
                else if (choose1 == 2) orderService.deleteOrder();
                else if (choose1 == 3)
                {
                    Console.WriteLine("请输入查询方式1-4：");
                    int c = Convert.ToInt32((Console.ReadLine()));
                    orderService.searchOrder(c);
                }
                else if (choose1 == 4) orderService.changeOrder();
                else if (choose1 == 5) orderService.sortOrder();
                else if (choose1 == 6) orderService.export();
                else if (choose1 == 7) orderService.import();
                else if (choose1 == 8) prc = false;
            }
        }
    }


}
