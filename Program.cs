using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace XMLToData
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Application app = new Application(/*context*/);
            app.ReadXml();
        }
    }
}
