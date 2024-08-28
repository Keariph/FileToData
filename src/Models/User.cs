namespace FileToData
{
    /// <summary>
    /// Represents a user info with details such as name and email.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance <see cref="User"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        public User(long id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        /// <summary>
        /// Initializes a new empty instance <see cref="User"/> class.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name for the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email for the user.
        /// </summary>
        public string Email { get; set; }
    }
}
