using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FileToData
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<User> _userList = new List<User>();
            List<Order> _orderList = new List<Order>();
            List<Product> _productsList = new List<Product>();
            List<ProductOrder> _productsOrder = new List<ProductOrder>();
            IDataReader xmlReader = new XmlReader();
            IConvertorFromData converter = new ConverterFromXml();
            ISenderData sender = new SenderToDb(_userList, _orderList, _productsList, _productsOrder);

            var configuration = new ConfigurationBuilder()
                    .AddUserSecrets(Assembly.GetExecutingAssembly())
                    .Build();
            string path = configuration.GetConnectionString("XmlPath");

            var data = xmlReader.ReadData(path);
            converter.ConvertData(data, out _userList, out _orderList, out _productsList, out _productsOrder);
            sender.SendAllData();
        }
    }
}
