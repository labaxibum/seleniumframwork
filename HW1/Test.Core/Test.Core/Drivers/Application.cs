using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test.Core.Drivers
{
    public class Application
    {
        public static IConfigurationRoot Configue()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                 .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                 .AddJsonFile($"appsetting.json", optional: true)
                 .AddEnvironmentVariables();
            return configBuilder.Build();
        }
    }
}
