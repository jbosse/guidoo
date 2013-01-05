using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NUnit.Framework;

namespace Website.Test
{
    [SetUpFixture]
    public class SetupFixture
    {
        private Process _ravenDbProcess;
        private bool _started;

        [SetUp]
        public void SetUp()
        {
            var exists = Process.GetProcessesByName("Raven.Server").Length >= 1;
            if (!exists)
            {
                StartRavenDb();
            }
        }

        [TearDown]
        public void TearDown()
        {
        }

        private void StartRavenDb()
        {
            var ravenServer = ConfigurationManager.AppSettings["RavenServerPath"];
            Console.Out.WriteLine("ravenServer = {0}", ravenServer);
            _ravenDbProcess = new Process
            {
                StartInfo =
                {
                    FileName = ravenServer
                }
            };
            _ravenDbProcess.Start();
            Thread.Sleep(10000);
        }
    }
}