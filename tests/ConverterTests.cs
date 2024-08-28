using FileToData;
using System.Text.Json;
using System.Xml.Linq;

namespace TestProject
{
    public class ConverterTests
    {
        private List<User> _userList;
        private List<Order> _orderList;
        private List<Product> _productsList;
        private List<ProductOrder> _productsOrder;
        private ConverterFromXml _converter;

        [SetUp]
        public void Setup()
        {
            List<User> _userList = new List<User>();
            List<Order> _orderList = new List<Order>();
            List<Product> _productsList = new List<Product>();
            List<ProductOrder> _productsOrder = new List<ProductOrder>();
            _converter = new ConverterFromXml();
        }

        [Test]
        public void CreateCorrectUser()
        {
            string strCorrectUser = "<user><id>1</id><fio>Ivanov Ivan Ivanovich</fio><email>ivan@email.com</email></user>";
            User result = _converter.CreateUser(strCorrectUser);
            string resultStr = JsonSerializer.Serialize(result);

            User expectUser = new User(1, "Ivanov Ivan Ivanovich", "ivan@email.com");
            string expectUserStr = JsonSerializer.Serialize(expectUser);

            Assert.AreEqual(expectUserStr, resultStr);
        }

        [Test]
        public void CreateCorrectOrder() 
        {
            string strCorrectUser = "<user><id>1</id><fio>Ivanov Ivan Ivanovich</fio><email>ivan@email.com</email></user>";
            User user = _converter.CreateUser(strCorrectUser);

            string strCorrectOrder = "<order><no>1</no><reg_date>10.10.2010</reg_date><sum>1500</sum></order>";
            Order result = _converter.CreateOrder(strCorrectOrder, user);
            string resultStr = JsonSerializer.Serialize(result);

            Order expectOrder = new Order(1, DateTime.Parse("10.10.2010"), user, 1500);
            string expectOrderStr = JsonSerializer.Serialize(expectOrder);

            Assert.AreEqual(expectOrderStr, resultStr);
        }

        [Test]  
        public void CreateCorrectProduct()
        {
            string strCorrectProduct = "<product><id>1</id><name>Pivo</name><price>57</price></product>";
            Product result = _converter.CreateProduct(strCorrectProduct);
            string resultStr = JsonSerializer.Serialize(result);

            Product expectProduct = new Product(1, "Pivo", 57);
            string expectProductStr = JsonSerializer.Serialize(expectProduct);

            Assert.AreEqual(expectProductStr, resultStr);
        }

        [Test]
        public void CreatePartialUser()
        {
            string strUserWithouId = "<user><fio>Ivanov Ivan Ivanovich</fio><email>ivan@email.com</email></user>";
            User userWithoutId = _converter.CreateUser(strUserWithouId);
            string resultStr = JsonSerializer.Serialize(userWithoutId);

            User expectUser = new User(1, "Ivanov Ivan Ivanovich", "ivan@email.com");
            string expectUserStr = JsonSerializer.Serialize(expectUser);
            Assert.AreEqual(expectUserStr, resultStr);

            string strUserWithoutFio = "<user><id>1</id><email>ivan@email.com</email></user>";
            Assert.Throws<NullReferenceException>(() => _converter.CreateUser(strUserWithoutFio));

            string strUserWithoutEmail = "<user><id>1</id><fio>Ivanov Ivan Ivanovich</fio></user>"; ;
            Assert.Throws<NullReferenceException>(() => _converter.CreateUser(strUserWithoutEmail));
        }

        [Test]
        public void CreatePartialOrder()
        {
            string strCorrectUser = "<user><id>1</id><fio>Ivanov Ivan Ivanovich</fio><email>ivan@email.com</email></user>";
            User user = _converter.CreateUser(strCorrectUser);

            string strOrderWithoutId = "<order><reg_date>10.10.2010</reg_date><sum>1500</sum></order>";
            Assert.Throws<NullReferenceException>(() => _converter.CreateOrder(strOrderWithoutId, user));

            string strOrderWithoutDate = "<order><no>1</no><sum>1500</sum></order>";
            Assert.Throws<NullReferenceException>(() => _converter.CreateOrder(strOrderWithoutDate, user));

            string strOrderWithoutSum = "<order><no>1</no><reg_date>10.10.2010</reg_date></order>";
            Assert.Throws<NullReferenceException>(() => _converter.CreateOrder(strOrderWithoutSum, user));
        }

        [Test]
        public void CreatePartialProduct()
        {
            string strProductWithoutId = "<product><name>Pivo</name><price>57</price></product>";
            Product productWithoutId = _converter.CreateProduct(strProductWithoutId);
            string productWithoutIdJson = JsonSerializer.Serialize(productWithoutId);

            Product expectProduct = new Product(1, "Pivo", 57);
            string expectProductStr = JsonSerializer.Serialize(expectProduct);
            Assert.AreEqual(expectProductStr, productWithoutIdJson);

            string strProductWithoutName = "<product><id>1</id><price>57</price></product>";
            Assert.Throws<NullReferenceException>(() => _converter.CreateProduct(strProductWithoutName));

            string strProductWithoutPrice = "<product><id>1</id><name>Pivo</name></product>";
            Assert.Throws<NullReferenceException>(() => _converter.CreateProduct(strProductWithoutPrice));
        }

        [Test]
        public void CreateInvalidUser()
        {
            string strInvalidUser = "<user><id>1<fia>Ivanov Ivan Ivanovich</fio><emal>ivan@email.com</email></user>";
            Assert.Throws<System.Xml.XmlException>(() => _converter.CreateUser(strInvalidUser));
        }

        [Test]
        public void CreateInvalidOrder()
        {
            string strCorrectUser = "<user><id>1</id><fio>Ivanov Ivan Ivanovich</fio><email>ivan@email.com</email></user>";
            User user = _converter.CreateUser(strCorrectUser);

            string strInvalidOrder = "<order><no>1</id><reg_date>10.10.2010<sum>1500</sum></order>";
            Assert.Throws<System.Xml.XmlException>(() => _converter.CreateOrder(strInvalidOrder, user));
        }

        [Test]
        public void CreateInvalidProduct()
        {
            string strInvalidProduct = "<product><nam>Pivo</name><price>57</product>";            
            Assert.Throws<System.Xml.XmlException>(() => _converter.CreateProduct(strInvalidProduct));
        }

        [Test]
        public void IncorrectUserInfo()
        {
            string strUserNegativId = "<user><id>-10</id><fio>Ivanov Ivan Ivanovich</fio><email>ivan@email.com</email></user>";
            User UserNegativId = _converter.CreateUser(strUserNegativId);
            string resultStr = JsonSerializer.Serialize(UserNegativId);

            User expectUser = new User(1, "Ivanov Ivan Ivanovich", "ivan@email.com");
            string expectUserStr = JsonSerializer.Serialize(expectUser);
            Assert.AreEqual(expectUserStr, resultStr);

            string strUserEmptyFio = "<user><id>1</id><fio></fio><email>ivan@email.com</email></user>";
            User UserEmptyFio = _converter.CreateUser(strUserEmptyFio);
            Assert.Null(UserEmptyFio);

            string strUserEmptyEmail = "<user><id>1</id><fio>Ivanov Ivan Ivanovich</fio><email></email></user>";;
            User UserEmptyEmail = _converter.CreateUser(strUserEmptyEmail);
            Assert.Null(UserEmptyEmail);
        }

        [Test]
        public void IncorrectOrderInfo()
        {
            string strCorrectUser = "<user><id>1</id><fio>Ivanov Ivan Ivanovich</fio><email>ivan@email.com</email></user>";
            User user = _converter.CreateUser(strCorrectUser);

            string strOrderNegativeId = "<order><no>-5</no><reg_date>10.10.2010</reg_date><sum>1500</sum></order>";
            Order orderNegativId = _converter.CreateOrder(strOrderNegativeId, user);
            Assert.Null(orderNegativId);

            string strOrderMinDate = "<order><no>1</no><reg_date></reg_date><sum>1500</sum></order>";
            Order orderMinDate = _converter.CreateOrder(strOrderMinDate, user);
            Assert.Null(orderMinDate);

            string strOrderMaxDate = "<order><no>1</no><reg_date>10.10.2026</reg_date><sum>1500</sum></order>";
            Order orderMaxDate = _converter.CreateOrder(strOrderMinDate, user);
            Assert.Null(orderMaxDate);

            string strOrderNegativSum = "<order><no>1</no><reg_date>10.10.2010</reg_date><sum>-1500</sum></order>";
            Order orderNegativSum = _converter.CreateOrder(strOrderNegativSum, user);
            Assert.Null(orderNegativSum);

            string strOrderNullUser = "<order><no>1</no><reg_date>10.10.2010</reg_date><sum>1500</sum></order>";
            Order orderNullUser = _converter.CreateOrder(strOrderNullUser, null);
            Assert.Null(orderNullUser);
        }

        [Test]
        public void IncorrectProductInfo()
        {
                string strProductNegativId = "<product><id>-1</id><name>Pivo</name><price>57</price></product>";
                Product productNegativId = _converter.CreateProduct(strProductNegativId);
                string productNegativIdJson = JsonSerializer.Serialize(productNegativId);

                Product expectProduct = new Product(1, "Pivo", 57);
                string expectProductStr = JsonSerializer.Serialize(expectProduct);
                Assert.AreEqual(expectProductStr, productNegativIdJson);

                string strProductEmptyName = "<product><id>1</id><name></name><price>57</price></product>";
                Product productEmptyName = _converter.CreateProduct(strProductEmptyName);
                Assert.Null(productEmptyName);

                string strProductNegativePrice = "<product><id>1</id><name>Pivo</name><price>-57</price></product>";
                Product productNegativePrice = _converter.CreateProduct(strProductNegativePrice);
                Assert.Null(productNegativePrice);            
        }

        [Test]
        public void InvalidXmlFile()
        {
            string invalidXml = "<order><no><order>";
            Assert.Throws<System.Xml.XmlException>(() => _converter.ConvertData(invalidXml, out _userList, out _orderList, out _productsList, out _productsOrder));

        }

        [Test]
        public void CorrectXmlFile() 
        {
            string correctXml = "<orders><order><no>1</no><reg_date>10.10.2010</reg_date><sum>57</sum><user><fio>Ivanov Ivan Ivanovich</fio><email>ivan@email.com</email></user><product><name>Pivo</name><price>57</price><quantity>1</quantity></product></order></orders>";
            User expectUser = new User(1, "Ivanov Ivan Ivanovich", "ivan@email.com");
            Product expectProduct = new Product(1, "Pivo", 57);
            Order expectOrder = new Order(1, DateTime.Parse("10.10.2010"), expectUser, 57);
            ProductOrder expectProductOrder= new ProductOrder(1, expectProduct, expectOrder, 1);
            string strExpectUser = JsonSerializer.Serialize(expectUser);
            string strExpectProduct = JsonSerializer.Serialize(expectProduct);
            string strExpectOrder = JsonSerializer.Serialize(expectOrder);
            string strExpectProductOrder = JsonSerializer.Serialize(expectProductOrder);

            _converter.ConvertData(correctXml, out _userList, out _orderList, out _productsList, out _productsOrder);

            string resultUser = JsonSerializer.Serialize(_userList[0]);
            string resultProduct = JsonSerializer.Serialize(_productsList[0]);
            string resultOrder = JsonSerializer.Serialize(_orderList[0]);
            string resultProductOrder = JsonSerializer.Serialize(_productsOrder[0]);

            Assert.AreEqual(strExpectUser, resultUser);
            Assert.AreEqual(strExpectProduct, resultProduct);
            Assert.AreEqual(strExpectOrder, resultOrder);
            Assert.AreEqual(strExpectProductOrder, resultProductOrder);
        }
    }
}
