using FileToData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    internal class SenderTests
    {
        private List<User> _userList;
        private List<Order> _orderList;
        private List<Product> _productsList;
        private List<ProductOrder> _productsOrder;
        private ISenderData _sender;

        [SetUp]
        public void Setup()
        {
            _userList = new List<User>() { new User(1, "Ivan", "ivan@email.com"), new User(2, "Pavel", "pavel@email.com") };
            _orderList = new List<Order>() { new Order(1, DateTime.Parse("10.10.2010"), _userList[0], 57), new Order(2, DateTime.Parse("01.01.2001"), _userList[1], 114) };
            _productsList = new List<Product>() { new Product(1, "Pivo", 57), new Product(1, "Pivo", 57) };
            _productsOrder = new List<ProductOrder>{ new ProductOrder(1, _productsList[0], _orderList[0], 1), new ProductOrder(2, _productsList[0], _orderList[1], 2) };
            _sender = new SenderToDb(_userList, _orderList, _productsList, _productsOrder);
        }

        [Test]
        public void MatchProduct()
        {
            Assert.Throws<InvalidOperationException>(() => _sender.SendAllData());
        }
    }
}
