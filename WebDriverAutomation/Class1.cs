using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Threading;
using OpenQA.Selenium.Support.UI;

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

        [Test, Description("Open and close Alert PopUp")]
        public void ShouldOpenCloseAlertPopUpContact()
        {
            driver.FindElement(By.Id("contact_link")).Click();
            Assert.True(driver.Url.Contains("contact"));
            driver.FindElement(By.Id("submit_message")).Click();

            //current opened alert winow
            IAlert popUp = driver.SwitchTo().Alert();
            Assert.True(popUp.Text.Contains("Name field is empty"));

            popUp.Accept(); //click the OK button
            Assert.True(driver.Title.Contains("Contact"));
        }

        [Test]
        public void checkContactPageTitle()
        {
            driver.FindElement(By.Id("contact_link")).Click();
            Assert.True(driver.Title.Contains("Contact"));
            takeScreenShot("contactPageTestScreenShot");
        }

        public void takeScreenShot(String filename)
        {
            //driver instance
            ITakesScreenshot screenshotHandler = driver as ITakesScreenshot;

            //save the screenshot
            Screenshot screenshot = screenshotHandler.GetScreenshot();
            screenshot.SaveAsFile(filename + ".png", ImageFormat.Png);
            //The image is saved in the project bin folder
        }


        [Test]
        public void ContactPageWaiting()
        {
            driver.FindElement(By.Id("contact_link")).Click();
            //waitOnPage(5);
            waitPageUntilElementIsVisible(By.Id("contact_link"), 5);
        }

        public void waitOnPage(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        public IWebElement waitPageUntilElementIsVisible(By locator, int maxSeconds)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(maxSeconds))
                .Until(ExpectedConditions.ElementExists((locator)));

        }
    }
}
