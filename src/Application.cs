using System.Xml.Linq;

namespace FileToData
{
    /// <summary>
    /// The Application class handles reading data from an XML file, creating user, order, and product objects, 
    /// and saving them to the database.
    /// </summary>
    public class Application
    {
        private List<User> _userList = new List<User>();
        private List<Order> _orderList = new List<Order>();
        private List<Product> _productsList = new List<Product>();
        private List<ProductOrder> _productsOrder = new List<ProductOrder>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// </summary>
        public Application() { }

        /// <summary>
        /// Reads data from an XML file, creates user, order, and product objects, 
        /// and stores them in corresponding lists.
        /// </summary>
        public void ReadXml(string path)
        {
            User user = new User();
            Order order = new Order();
            XDocument xml = XDocument.Load(path);
            XElement orders = xml.Element("orders");

            if (orders is not null)
            {
                foreach (XElement orderXml in orders.Elements("order"))
                {
                    long id = Int64.Parse(orderXml.Element("no").Value);
                    DateTime date = DateTime.Parse(orderXml.Element("reg_date").Value);
                    double sum = Double.Parse(orderXml.Element("sum").Value);
                    List<XElement>? productsXml = orderXml.Elements("product").ToList();
                    XElement? userXml = orderXml.Element("user");

                    user = CreateUserFromXml(userXml);
                    order = new Order(id, date, user, sum);
                    _orderList.Add(order);
                    CreateProductFromXml(productsXml, order);
                }
            }

            for (int i = 0; i < _productsList.Count; i++)
            {
                Console.WriteLine(_productsList[i].Name + " " + _productsList[i].Price);
            }

            SendToDatabase();
        }

        /// <summary>
        /// Creates a <see cref="User"/> object from an XML element and adds it to the user list 
        /// if it does not already exist.
        /// </summary>
        /// <param name="userXml">The XML element containing user data.</param>
        /// <returns>The created <see cref="User"/> object.</returns>
        public User CreateUserFromXml(XElement userXml)
        {
            User user;
            string name = userXml.Element("fio").Value;
            string email = userXml.Element("email").Value;

            if (!_userList.Exists(user => user.Name == name))
            {
                user = new User(_userList.Count + 1, name, email);
                _userList.Add(user);
                return user;
            }

            return _userList.Find(user => user.Name == name);
        }

        /// <summary>
        /// Creates <see cref="Product"/> objects from a list of XML elements and associates them 
        /// with an order. Adds products and product orders to their respective lists.
        /// </summary>
        /// <param name="products">The list of XML elements containing product data.</param>
        /// <param name="order">The order associated with the products.</param>
        public void CreateProductFromXml(List<XElement> products, Order order)
        {
            Product product = new Product();

            if (products is not null)
            {
                foreach (XElement productXml in products)
                {
                    int quantity = Int32.Parse(productXml.Element("quantity").Value);
                    string name = productXml.Element("name").Value;
                    double price = Double.Parse(productXml.Element("price").Value);
                    product = new Product(_productsList.Count + 1, name, price);

                    if (!_productsList.Exists(product => product.Name == name))
                    {
                        _productsList.Add(product);
                    }
                    else
                    {
                        product = _productsList.Find(product => product.Name == name);
                    }

                    _productsOrder.Add(new ProductOrder(_productsOrder.Count + 1, product, order, quantity));
                }
            }
        }

        /// <summary>
        /// Saves the user, product, order, and product order data to the database.
        /// </summary>        
        public void SendToDatabase()
        {
            using (MyDbContext context = new MyDbContext())
            {
                context.Users.AddRange(_userList);
                context.Products.AddRange(_productsList);
                context.Orders.AddRange(_orderList);
                context.ProductOrders.AddRange( _productsOrder );
                context.SaveChanges();
            }
        }
    }
}



