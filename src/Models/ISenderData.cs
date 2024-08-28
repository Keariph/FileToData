namespace FileToData
{
    /// <summary>
    /// Handles the sending of <see cref="User"/>, <see cref="Order"/>, <see cref="Product"/> and <see cref="ProductOrder"/> data to the database or another destination.
    /// </summary>
    public interface ISenderData
    {
        /// <summary>
        /// Converts raw data into appropriate internal objects such as <see cref="User"/>, <see cref="Order"/>, <see cref="Product"/> and <see cref="ProductOrder"/> .
        /// </summary>
        /// <param name="data">The raw data to be converted.</param>
        void SendAllData();
    }
}
