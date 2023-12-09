using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace SelenuimTest;

public class Tests
{
    
    IWebDriver driver;

    [SetUp]
    public void StartBrowser()
    {
        driver = new ChromeDriver();
        driver.Url = "https://content-watch.ru/text/";
    }
    

    [Test]
    public void TestTitle()
    {
        if (driver.Title == "Онлайн проверка на плагиат - проверить текст на уникальность без регистрации")
        {
            Assert.Pass();
        }
        Assert.Fail();
    }

    [Test]
    public void TestFunctions()
    {
        var textarea = driver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[2]/div/div/form/textarea"));
        var button = driver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[2]/div/div/form/div[4]/div/a"));
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

        if (textarea is not null && button is not null)
        {
            try
            {
                textarea.SendKeys(Text.AnExcerpt);
                button.Click();
                var result = wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("/html/body/div[1]/div[3]/div[2]/div/div/form/div[6]/center/table")
                    ));
                Assert.Pass();

            }
            catch (WebDriverTimeoutException)
            {
                var modal = wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("/html/body/div[5]/div/div/div[2]")));
                if (modal is not null)
                {
                    Assert.Fail(modal.Text);
                }
                Assert.Fail();
            }
        }
        Assert.Fail();
    }

    [Test]
    public void TestNavigation()
    {
        var link = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[4]/ul/li[6]/a"));
        if (link is not null)
        {
            link.Click();
            if (driver.Url == "https://content-watch.ru/api/")
            {
                Assert.Pass();
            }
        }
        Assert.Fail();
    }

    [TearDown]
    public void CloseBrowser()
    {
        driver.Close();
    }
}