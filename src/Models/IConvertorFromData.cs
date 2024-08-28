namespace FileToData
{
    /// <summary>
    /// Defines a contract for converting raw data into specific objects such as users, products, and orders.
    /// </summary>
    public interface IConvertorFromData
    {
        /// <summary>
        /// Converts the provided raw data into the appropriate internal objects.
        /// </summary>
        /// <param name="data">The raw data to be converted.</param>
        void ConvertData(object data, out List<User> users, out List<Order> orders, out List<Product> products, out List<ProductOrder> productOrders);

        /// <summary>
        /// Creates a <see cref="User"/> object from the provided raw user data.
        /// </summary>
        /// <param name="userData">The raw data used to create the user.</param>
        /// <returns>A <see cref="User"/> object created from the raw data.</returns>
        User CreateUser(object userData);

        /// <summary>
        /// Creates a <see cref="Product"/> object from the provided raw product data.
        /// </summary>
        /// <param name="productData">The raw data used to create the product.</param>
        /// <returns>A <see cref="Product"/> object created from the raw data.</returns>
        Product CreateProduct(object productData);

        /// <summary>
        /// Creates an <see cref="Order"/> object from the provided raw order data and associates it with the specified user.
        /// </summary>
        /// <param name="OrderData">The raw data used to create the order.</param>
        /// <param name="User">The <see cref="User"/> object associated with the order.</param>
        /// <returns>An <see cref="Order"/> object created from the raw data.</returns>
        Order CreateOrder(object OrderData, User User);
    }
}
