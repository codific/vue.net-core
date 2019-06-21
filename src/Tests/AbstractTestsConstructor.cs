using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public abstract class AbstractTestsConstructor
    {
        private readonly TestServerFixture testServerFixture = new TestServerFixture();

        public TService GetService<TService>() where TService : class
        {
            return this.testServerFixture.TestServer?.Host?.Services?.GetService(typeof(TService)) as TService;
        }
    }
}
