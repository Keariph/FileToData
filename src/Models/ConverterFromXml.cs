using System.Xml.Linq;

namespace FileToData
{
    /// <summary>
    /// Converting XML data into specific objects such as <see cref="User"/>, <see cref="Order"/>, <see cref="Product"/> and <see cref="ProductOrder"/>.
    /// </summary>
    public class ConverterFromXml : IConvertorFromData
    {
        private List<User> _userList;
        private List<Order> _orderList;
        private List<Product> _productsList;
        private List<ProductOrder> _productsOrder;

        // <summary>
        /// Initializes a new instance of the <see cref="ConverterFromXml"/> class.
        /// </summary>
        public ConverterFromXml()
        {
            _userList = new List<User>();
            _orderList = new List<Order>();
            _productsList = new List<Product>();
            _productsOrder = new List<ProductOrder>();
        }

        /// <summary>
        /// Converts XML data into appropriate internal objects such as <see cref="User"/>, <see cref="Order"/>, <see cref="Product"/> and <see cref="ProductOrder"/> .
        /// </summary>
        /// <param name="data">The XML data to be converted.</param>
        public void ConvertData(object data, out List<User> users, out List<Order> orders, out List<Product> products, out List<ProductOrder> productOrders)
        {
            XDocument xml = XDocument.Parse(data.ToString());

            User user = new User();
            Order order = new Order();
            Product product = new Product();
            ProductOrder productOrder = new ProductOrder();

            users = _userList;
            orders = _orderList;
            products = _productsList;
            productOrders = _productsOrder;

            if (xml != null)
            {
                XElement ordersXml = xml.Element("orders");

                if (ordersXml is not null)
                {
                    foreach (XElement orderXml in ordersXml.Elements("order"))
                    {
                        List<XElement> productsXml = orderXml.Elements("product").ToList();
                        XElement userXml = orderXml.Element("user");

                        if (userXml != null)
                        {
                            user = CreateUser(userXml);

                        }

                        order = CreateOrder(orderXml, user);

                        if (order != null)
                        {
                            _orderList.Add(order);
                        }
                        if (productsXml is not null)
                        {
                            foreach (XElement productXml in productsXml)
                            {
                                int quantity;
                                product = CreateProduct(productXml);

                                if (productXml.Element("quantity") != null)
                                {
                                    Int32.TryParse(productXml.Element("quantity").Value, out quantity);

                                    if (product != null && order != null && quantity > 0)
                                    {
                                        _productsOrder.Add(new ProductOrder(_productsOrder.Count + 1, product, order, quantity));
                                    }
                                }
                            }
                        }

                        
                    }
                }
            }
        }

        /// <summary>
        /// Creates a <see cref="User"/> object from the provided XML user data.
        /// </summary>
        /// <param name="userData">The XML data used to create the user.</param>
        /// <returns>A <see cref="User"/> object created from the XML data.</returns>
        public Order CreateOrder(object orderData, User user)
        {
            long id;
            double sum;
            DateTime date;
            XElement orderXml = XElement.Parse(orderData.ToString());
            Int64.TryParse(orderXml.Element("no").Value, out id);
            DateTime.TryParse(orderXml.Element("reg_date").Value, out date);
            Double.TryParse(orderXml.Element("sum").Value, out sum);

            if (id > 0 && date != DateTime.MinValue && date < DateTime.Today && sum > 0 && user != null)
            {
                return new Order(id, date, user, sum);
            }

            return null;
        }

        /// <summary>
        /// Creates a <see cref="Product"/> object from the provided XML product data.
        /// </summary>
        /// <param name="productData">The XML data used to create the product.</param>
        /// <returns>A <see cref="Product"/> object created from the XML data.</returns>
        public Product CreateProduct(object productData)
        {
            long id;
            string name;
            double price;
            Product product;
            XElement productXml = XElement.Parse(productData.ToString());

            name = productXml.Element("name").Value;
            Double.TryParse(productXml.Element("price").Value, out price);

            if (productXml.Element("id") != null)
            {
                Int64.TryParse(productXml.Element("id").Value, out id);

                if (id <= 0)
                {
                    id = _productsList.Count + 1;
                }
            }
            else
            {
                id = _productsList.Count + 1;
            }
            if (!string.IsNullOrEmpty(name) && price > 0)
            {
                product = new Product(id, name, price);

                if (!_productsList.Exists(product => product.Name == name))
                {
                    _productsList.Add(product);
                    return product;
                }
                else
                {
                    return _productsList.Find(product => product.Name == name);
                }
            }

            return null;
        }


        /// <summary>
        /// Creates an <see cref="Order"/> object from the provided XML order data and associates it with the specified user.
        /// </summary>
        /// <param name="OrderData">The XML data used to create the order.</param>
        /// <param name="User">The <see cref="User"/> object associated with the order.</param>
        /// <returns>An <see cref="Order"/> object created from the XML data.</returns>
        public User CreateUser(object userData)
        {
            long id;
            User user;
            XElement userXml = XElement.Parse(userData.ToString());
            string name = userXml.Element("fio").Value;
            string email = userXml.Element("email").Value;

            if (userXml.Element("id")!= null)
            {
                Int64.TryParse(userXml.Element("id").Value, out id);

                if (id <= 0)
                {
                    id = _userList.Count + 1;
                }
            }
            else
            {
                id = _userList.Count + 1;
            }
            if (!string.IsNullOrEmpty(name) & !string.IsNullOrEmpty(email))
            {
                if (!_userList.Exists(user => user.Name == name))
                {
                    user = new User(id, name, email);
                    _userList.Add(user);
                    return user;
                }

                return _userList.Find(user => user.Name == name);
            }

            return null;
        }
    }
}
