using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlannerProject.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MealPlannerProjectTest.ExceptionsTesting
{
    [TestClass]
    public class ConfigurationErrorsExceptionTest
    {
        [TestMethod]
        public void ConfigurationErrorsExceptionTest_test()
        {
            try
            {
                throw new ConfigurationErrorsException();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ConfigurationErrorsException));
            }
            try
            {
                throw new ConfigurationErrorsException("Custom Message");
            }
            catch (ConfigurationErrorsException e)
            {
                Assert.AreEqual("Custom Message", e.Message);
                try
                {
                    throw new ConfigurationErrorsException("Custom Message", e);
                }
                catch (ConfigurationErrorsException ex)
                {
                    Assert.AreEqual("Custom Message", ex.Message);
                }
            }

        }
    }
}
