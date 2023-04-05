using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orderprogram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderprogram.Tests
{
    [TestClass()]
    public class OrderTests
    {
        [TestMethod()]
        public void searchOrderTest()
        {
            Order order4 = new Order();
            List<Order> order = new List<Order>();
            OrderService a = new OrderService();

            order4.ID = 1;
            order4.Customer = "zhang";
            order4.Data = "2023";
            order4.Money=10;
            order.Add(order4);

            Assert.AreEqual(1, order[0].ID);
            Assert.AreEqual("zhang", order[0].Customer);
            Assert.AreEqual("2023", order[0].Data);
            Assert.AreEqual(10, order[0].Money);
        }

        [TestMethod()]
        public void addOrderTest()
        {
            Order order1 = new Order();
            List<Order> order = new List<Order>();
            OrderService a=new OrderService();

            order1.ID = 1;
            order1.Customer = "zhang";
            order1.Data = "2023";
            order1.AddOrderdetails("apple", 5, 2);
            order.Add(order1);
            order1.getmoney();
            Assert.AreEqual(1, order[0].ID);
            Assert.AreEqual("zhang",order[0].Customer);
            Assert.AreEqual("2023", order[0].Data);
            Assert.AreEqual(10, order[0].Money);
        }

        [TestMethod()]
        public void deleteOrderTest()
        {
            Order order2=new Order();
            Order order3=new Order();
            List<Order>order=new List<Order>();
            OrderService a= new OrderService();

            order2.ID = 2;
            order2.Customer = "wang";
            order2.Data = "2022";
            order2.AddOrderdetails("apple", 5, 2);
            order.Add(order2);
            order2.getmoney();

            order3.ID = 3;
            order3.Customer = "zhao";
            order3.Data = "2021";
            order3.AddOrderdetails("apple", 5, 2);
            order.Add(order3);
            order3.getmoney();

            order.Remove(order3);
            Assert.AreEqual(2,order[0].ID);
            Assert.AreEqual("wang", order[0].Customer);
            Assert.AreEqual("2022", order[0].Data);
            Assert.AreEqual(10, order[0].Money);
        }
    }
        

}
