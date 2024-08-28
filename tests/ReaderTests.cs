using System.Data;
using System.Xml.Linq;
using FileToData;

namespace TestProject
{
    public class ReaderTests
    {
        FileToData.IDataReader reader;

        [SetUp]
        public void Setup()
        {
            reader = new XmlReader();
        }

        [Test]
        public void NonexistPath()
        {
            string nonexistPath = "nonexistPath.xml";
            Assert.Throws<FileNotFoundException>(() => reader.ReadData(nonexistPath));
        }

        [Test]
        public void InvalidFileFormat()
        {
            string invalidFileFormat = Environment.CurrentDirectory + "\\invalidFileFormat.html";
            Assert.Throws<System.Xml.XmlException>(() => reader.ReadData(invalidFileFormat));
        }

        [Test]
        public void InvalidXml()
        {
            string invalidXml = Environment.CurrentDirectory + "\\invalidXml.xml";
            Assert.Throws<System.Xml.XmlException>(() => reader.ReadData(invalidXml));
        }

        [Test]
        public void CorrectFile()
        {
            string str = "<user><id>1</id><fio>Ivanov Ivan Ivanovich</fio><email>ivan@email.com</email></user>";
            XDocument expectDoc = XDocument.Parse(str);
            string correctFile = Environment.CurrentDirectory + "\\correctFile.xml";
            Assert.AreEqual(expectDoc.ToString(), reader.ReadData(correctFile).ToString());
        }

    }
}