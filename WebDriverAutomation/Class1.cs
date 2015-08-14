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
            driver.Navigate().GoToUrl("http://www.thetestroom.com/webapp/index.html");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            Console.WriteLine("Tear Down");
        }

        //example of switching control from Window A to Window B
        [Test, Description("Open and close Terms Window")]
        public void ShouldOpenCloseTermsWindow()
        {
            //save the current window content
            String parentWindow = driver.CurrentWindowHandle;
            driver.FindElement(By.Id("footer_term")).Click();

            //switch to the window we are going to open
            foreach(String window in driver.WindowHandles)
            {
                driver.SwitchTo().Window(window);
            }
            Assert.True(driver.Url.Contains("term"));
            driver.Close();

            //swtich control back to parent window 
            driver.SwitchTo().Window(parentWindow);
            Assert.True(driver.Url.Contains("index"));
        }

        
    }
}
