namespace FileToData
{
    /// <summary>
    /// Defines a contract for reading data from a specified path.
    /// </summary>
    public interface IDataReader
    {
        /// <summary>
        /// Reads data from the provided file path and returns the data as an object.
        /// </summary>
        /// <param name="path">The file path from which to read the data.</param>
        /// <returns>An object containing the data read from the specified path.</returns>
        public object ReadData(string path);
    }
}
