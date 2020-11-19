using Facebook_Bot_.Driver;
using Facebook_Bot_.Model;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Facebook_Bot_.Service
{
    public class PostingService:IDisposable
    {
        Browser browser;
        Timer timer;
        List<string> groups;
        public event Action endPosting;
        public PostingService()
        {
            browser = new Browser();
            groups = new List<string>();

            //groups.Add("https://www.facebook.com/groups/rabota.polsha2017");
            //groups.Add("https://www.facebook.com/groups/106312546563330");
            //groups.Add("https://www.facebook.com/groups/Worklife.com.ua");
            //groups.Add("https://www.facebook.com/groups/praca.eu");
            //groups.Add("https://www.facebook.com/groups/1692327051016386");
            //groups.Add("https://www.facebook.com/groups/1420287161331833");
            //groups.Add("https://www.facebook.com/groups/521636951523325");
            //groups.Add("https://www.facebook.com/groups/284153058599894");
            //groups.Add("https://www.facebook.com/groups/1460998473923448");
            ////groups.Add("https://www.facebook.com/groups/2038396809761567"); delete
            //groups.Add("https://www.facebook.com/groups/TARGIPRACY");
            //groups.Add("https://www.facebook.com/groups/523290185077773");
            //groups.Add("https://www.facebook.com/groups/1627224043972006");
            //groups.Add("https://www.facebook.com/groups/pracapolska");
            //groups.Add("https://www.facebook.com/groups/400163897113751");
            //groups.Add("https://www.facebook.com/groups/ArgosGroup");
            //groups.Add("https://www.facebook.com/groups/1547915632180209");
            //groups.Add("https://www.facebook.com/groups/1978984955669389");
            //groups.Add("https://www.facebook.com/groups/595093963989305");
            //groups.Add("https://www.facebook.com/groups/1157993230882157");
            ////groups.Add("https://www.facebook.com/groups/292310154824067"); delete
            //groups.Add("https://www.facebook.com/groups/397873750593564");
            //groups.Add("https://www.facebook.com/groups/1774309186117464");
            //groups.Add("https://www.facebook.com/groups/robotavpolshe2019");
            //groups.Add("https://www.facebook.com/groups/1041858195953346");
            //groups.Add("https://www.facebook.com/groups/robotaes");
            //groups.Add("https://www.facebook.com/groups/rabota.in.Poland");
            //groups.Add("https://www.facebook.com/groups/336751676661105");
            //groups.Add("https://www.facebook.com/groups/708387059361301");
            //groups.Add("https://www.facebook.com/groups/824308357719884");
            //groups.Add("https://www.facebook.com/groups/petrov1989");
            //groups.Add("https://www.facebook.com/groups/1Work.Europe1");


            groups.Add("https://www.facebook.com/groups/Praca.w.Polsce.Work.in.Poland");
            groups.Add("https://www.facebook.com/groups/1873128166260042");
            groups.Add("https://www.facebook.com/groups/440559166305912");
            groups.Add("https://www.facebook.com/groups/trebarobota");
            groups.Add("https://www.facebook.com/groups/826048624156533");
            groups.Add("https://www.facebook.com/groups/198473674006268");
            groups.Add("https://www.facebook.com/groups/399791260225762");
            groups.Add("https://www.facebook.com/groups/515512901976103");
            groups.Add("https://www.facebook.com/groups/robotaworld");
            groups.Add("https://www.facebook.com/groups/1686409544945206");
            groups.Add("https://www.facebook.com/groups/841156749373425");
            groups.Add("https://www.facebook.com/groups/496333417237567");
            groups.Add("https://www.facebook.com/groups/299196500420514");
            groups.Add("https://www.facebook.com/groups/workpl");

        }

        public void Login(string login, string password)
        {
            try
            {
                browser.driver.Navigate().GoToUrl("https://www.facebook.com/login/device-based/regular/login/?login_attempt=1&lwv=100");
                browser.driver.FindElement(By.Id("email")).SendKeys(login + OpenQA.Selenium.Keys.Enter);
                System.Threading.Thread.Sleep(5000);
                browser.driver.FindElement(By.Id("pass")).SendKeys(password + OpenQA.Selenium.Keys.Enter);       
            }
            catch (Exception ex)
            {
               
            }
        }

        public void Stop()
        {
            browser.driver.Quit();
        }

        public void Start(Posts post)
        {
            if (post.Random)
            {
                try
                {
                    foreach (var group in groups)
                    {
                        if (post.PhotosFullName.Count > 0)
                        {                           
                            var res = Post(group, post.PhotosFullName[new Random().Next(0, post.PhotosFullName.Count - 1)], post.Message);
                            
                            if (res != 0)
                                continue;                           
                        }
                    }

                    browser.driver.Quit();
                    endPosting.Invoke();
                } 
                catch(Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    foreach (var photo in post.PhotosFullName)
                    {
                        foreach (var group in groups)
                        {
                            var res = Post(group, photo, post.Message);
                            if (res != 0)
                                continue;
                        }
                    }

                    browser.driver.Quit();
                    endPosting.Invoke();
                }
                catch (Exception ex)
                {

                }
            }            
        }

        private int Post(string groupUrl, string photoPath, string text)
        {
            try
            {
                System.Threading.Thread.Sleep(new Random().Next(1900, 2400));
                
                browser.driver.Navigate().GoToUrl(groupUrl);
                System.Threading.Thread.Sleep(new Random().Next(2700, 3200));

                String script = "var elems=document.querySelectorAll('.mkhogb32');for(var i=0; i<elems.length; i++)elems[i].style.display='block';";
                ((IJavaScriptExecutor)browser.driver).ExecuteScript(script);

                System.Threading.Thread.Sleep(new Random().Next(2700, 3200));

                var file = browser.driver.FindElement(By.CssSelector("input[type=file]"));
                file.SendKeys(photoPath);
                System.Threading.Thread.Sleep(new Random().Next(2700, 3200));

                String script2 = "var elems=document.querySelectorAll('.mkhogb32');for(var i=0; i<elems.length; i++)elems[i].style.display='none';";
                ((IJavaScriptExecutor)browser.driver).ExecuteScript(script2);
                System.Threading.Thread.Sleep(new Random().Next(2700, 3200));

                var message = browser.driver.FindElement(By.CssSelector("div[data-contents='true']>div.bi6gxh9e>div._1mf >span"));
                message.SendKeys(text + OpenQA.Selenium.Keys.Enter);
                System.Threading.Thread.Sleep(new Random().Next(2700, 3200));

                var btn = browser.driver.FindElement(By.CssSelector("div.ihqw7lf3>div.rq0escxv>div.rq0escxv>div.oajrlxb2"));
                System.Threading.Thread.Sleep(new Random().Next(2700, 3200));

                btn.Click();
                System.Threading.Thread.Sleep(new Random().Next(5000, 10000));

                return 0;
            }
            catch (Exception ex)
            {                
                //if (ex is OpenQA.Selenium.Remote.RemoteWebDriver)
                //    return;
                if (ex.Message.Contains("alert"))
                {
                    try
                    {
                        browser.driver.SwitchTo().Alert().Accept();
                    }
                    catch(Exception ec)
                    {
                        return 3;
                    }
                    try
                    {
                        var close = browser.driver.FindElement(By.CssSelector(".fcg2cn6m>div.oajrlxb2"));

                        if (close == null)
                            browser.driver.Navigate().GoToUrl("https://www.facebook.com/");
                        else
                            close.Click();
                    }
                    catch(Exception eo)
                    {
                        return 4;
                    }

                    return 1;
                }

                return 2;
            }
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
//div[data-offset-key='ckeee-0-0']>div>span
//div[data-contents='true']>div.bi6gxh9e>div._1mf >span