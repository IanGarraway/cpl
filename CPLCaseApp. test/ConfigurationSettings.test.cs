using Microsoft.VisualStudio.TestTools.UnitTesting;
using CPL_Case_mini_capstone.Configurations;
using System;
using System.IO;

namespace CPLCaseApp._test
{
    [TestClass]
    public sealed class ConfigurationSettingsTest
    {
        [TestMethod]
        public void TestConfigurationLoad()
        {
            //Arrange

            //Act
            Configuration settings = new Configuration("mockappsettings.json");

            //Assert
            Assert.IsNotNull(settings);
            Assert.IsTrue(settings.LoadSuccessful);
            Assert.AreEqual("fakeResource", settings.Resource);
            Assert.AreEqual("fakeSecretID", settings.Secret);
            Assert.AreEqual("fakeAppID", settings.ClientID);
        }

        [TestMethod]
        public void TestConfigurationFailureOnNoFile()
        {
            //Arrange
            string expectedMessage = "Error: Configuration file not found";

            using (var consoleOutput = new StringWriter())
            {
                var originalOut = Console.Out;
                Console.SetOut(consoleOutput);

                try
                {
                    //Act
                    Configuration settings = new Configuration("noFile");

                    //Assert
                    Assert.IsNotNull(settings);
                    Assert.IsFalse(settings.LoadSuccessful);
                    Assert.AreEqual(expectedMessage, consoleOutput.ToString().Trim());
                }finally { Console.SetOut(originalOut); }
            }
        }

        [TestMethod]
        public void TestConfigurationFailureOnNoResource()
        {
            //Arrange
            
            string expectedMessage = "Error:Value cannot be null. (Parameter 'Resource') is null.";

            using (var consoleOutput = new StringWriter())
            {
                var originalOut = Console.Out;
                Console.SetOut(consoleOutput);

                try
                {
                    //Act
                    Configuration settings = new Configuration("mockappsettingsnoresource.json");

                    //Assert
                    Assert.IsNotNull(settings);
                    Assert.IsFalse(settings.LoadSuccessful);
                    Assert.AreEqual(expectedMessage, consoleOutput.ToString().Trim());
                }
                finally
                {
                    Console.SetOut(originalOut);
                }
            }
        }

        [TestMethod]
        public void TestConfigurationFailureOnNoSecretId()
        {
            //Arrange
            string expectedMessage = "Error:Value cannot be null. (Parameter 'Secret') is null.";

            using (var consoleOutput = new StringWriter())
            {

                var originalOut = Console.Out;
                Console.SetOut(consoleOutput);

                try
                {
                    //Act
                    Configuration settings = new Configuration("mockappsettingsnosecretid.json");

                    //Assert
                    Assert.IsNotNull(settings);
                    Assert.AreEqual(expectedMessage, consoleOutput.ToString().Trim());
                    Assert.IsFalse(settings.LoadSuccessful);
                }
                finally { Console.SetOut(originalOut); }
            }
        }
    }
}
