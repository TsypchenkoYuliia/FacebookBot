using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook_Bot_.Driver
{
    public class Browser
    {
        public ChromeDriver driver;
        ChromeOptions options;
        public Browser() 
        {
            options = new ChromeOptions();
            options.AddArguments("--disable-notifications");
            options.AddArguments("--disable-popup-blocking");
            options.AddArguments("--disable-application-cache");
            options.AddArguments("--disable-extensions");

            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;

            driver = new ChromeDriver(driverService, options);
        }
        
    }
}
