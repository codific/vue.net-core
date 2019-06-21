using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class TestServerFixture : IDisposable
    {
        public TestServer TestServer { get; set; }

        public TestServerFixture()
        {
            var hostBuilder = new WebHostBuilder()
                    .UseEnvironment("Testing")
                    .UseStartup<Startup>();
            TestServer = new TestServer(hostBuilder);
        }

        public void Dispose()
        {
            TestServer.Dispose();
        }
    }
}
