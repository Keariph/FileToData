using System.Xml.Linq;

namespace FileToData
{
    /// <summary>
    ///The XmlReader class handles reading data from an XML file.
    /// </summary>
    public class XmlReader : IDataReader
    {
        /// <summary>
        /// Initializes a new instance <see cref="XmlReader"/> class.
        /// </summary>
        public XmlReader() { }

        /// <summary>
        /// Reads data from the provided file path and returns the data as an <see cref="XDocument"/>.
        /// </summary>
        /// <param name="path">The file path from which to read the data.</param>
        /// <returns>An object containing the data read from the specified path.</returns>        
        public object ReadData(string path)
        {
            XDocument xml = XDocument.Load(path);
            return xml;
        }
    }
}
