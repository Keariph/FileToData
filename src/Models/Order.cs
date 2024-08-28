namespace FileToData
{
    /// <summary>
    /// Represents a order info with details such as date and total cost.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Initializes a new instance <see cref="Order"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <param name="user"></param>
        /// <param name="totalCost"></param>
        public Order(long id, DateTime date, User user, double totalCost)
        {
            Id = id;
            Date = date;
            User = user;
            TotalCost = totalCost;
        }

        /// <summary>
        /// Initializes a new empty instance <see cref="Order"/> class.
        /// </summary>
        public Order()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier for the order.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the date of the order.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets foreign key of the <see cref="XMLToData.Models.User"/> for the order.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the total cost for the order.
        /// </summary>
        public double TotalCost { get; set; }
    }
}
