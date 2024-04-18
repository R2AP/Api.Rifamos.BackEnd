using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace  Api.Rifamos.BackEnd
{
    public static class Logger
    {
        public static void InicializarLog()
        {
            XmlDocument log4netConfig = new();
            log4netConfig.Load(File.OpenRead(Path.Combine(AppContext.BaseDirectory, "log4net.config")));
            var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(Hierarchy));
            XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

            //var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            //XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

        }
    }
}
