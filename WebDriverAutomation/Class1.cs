using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverAutomation
{
    [TestFixture]
    public class Class1
    {
        IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new FirefoxDriver();
            Console.WriteLine("Set up");
        }

        //example of Ignore a test
        [Test, Ignore]
        public void CompareTwoNumbers()
        {
            int a = 10, b = 10;
            Assert.AreEqual(a, b);
        }
        [Test]
        public void ShouldCheckAppUrl()
        {
            driver.Navigate().GoToUrl("http://www.thetestroom.com/webapp/");
            Assert.True(driver.Url.Contains("webapp"));          
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            Console.WriteLine("Tear Down");
        }
    }
}
