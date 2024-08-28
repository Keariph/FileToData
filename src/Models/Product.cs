namespace FileToData
{
    /// <summary>
    /// Represents a product info with details such as name and price.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Initializes a new instance <see cref="Product"/> class.
        /// <para>
        /// Used to serialize and deserialize an object.
        /// </para>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        public Product(long id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        /// <summary>
        /// Initializes a new empty instance <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier for the priduct.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name for the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price for the product.
        /// </summary>
        public double Price { get; set; }
    }
}
