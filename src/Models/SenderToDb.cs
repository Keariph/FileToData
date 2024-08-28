namespace FileToData
{
    /// <summary>
    /// Handles the sending of <see cref="User"/>, <see cref="Order"/>, <see cref="Product"/> and <see cref="ProductOrder"/> data to the database.
    /// </summary>
    public class SenderToDb : ISenderData
    {
        private List<User> _userList = new List<User>();
        private List<Order> _orderList = new List<Order>();
        private List<Product> _productsList = new List<Product>();
        private List<ProductOrder> _productsOrder = new List<ProductOrder>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SenderToDb"/> class 
        /// with the provided <see cref="User"/>, <see cref="Order"/>, <see cref="Product"/> and <see cref="ProductOrder"/> lists.
        /// </summary>
        /// <param name="userList">The list of users to be sent to the database.</param>
        /// <param name="orderList">The list of orders to be sent to the database.</param>
        /// <param name="productsList">The list of products to be sent to the database.</param>
        /// <param name="productsOrder">The list of product orders to be sent to the database.</param>
        public SenderToDb(List<User> userList, List<Order> orderList, List<Product> productsList, List<ProductOrder> productsOrder)
        {
            _userList = userList;
            _orderList = orderList;
            _productsList = productsList;
            _productsOrder = productsOrder;
        }

        /// <summary>
        /// Sends all <see cref="User"/>, <see cref="Order"/>, <see cref="Product"/> and <see cref="ProductOrder"/> data to the database.
        /// </summary>
        public void SendAllData()
        {
            using (MyDbContext context = new MyDbContext())
            {
                context.Users.AddRange(_userList);
                context.Products.AddRange(_productsList);
                context.Orders.AddRange(_orderList);
                context.ProductOrders.AddRange(_productsOrder);
                context.SaveChanges();
            }
        }
    }
}
