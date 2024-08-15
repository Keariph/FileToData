namespace XMLToData.Models
{
    /// <summary>
    /// Represents a order info with details such as date and total cost.
    /// </summary>
    public class ProductOrder
    {
        /// <summary>
        /// Initializes a new instance <see cref="ProductOrder"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <param name="order"></param>
        /// <param name="amount"></param>
        public ProductOrder(long id, Product product, Order order, int amount)
        {
            Id = id;
            Product = product;
            Order = order;
            Amount = amount;
        }


        /// <summary>
        /// Initializes a new empty instance <see cref="ProductOrder"/> class.
        /// </summary>
        public ProductOrder()
        {
        }

        public long Id { get; set; }

        /// <summary>
        /// Gets or sets foreign key of the <see cref="XMLToData.Models.Product"/> for the order.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets foreign key of the <see cref="XMLToData.Models.Order"/> for the order.
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Gets or sets the amount of the products.
        /// </summary>
        public int Amount { get; set; }
    }
}
