using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VcsWebdriver.Pages
{
    class SelectDemoDropdownPage: PageBase
    {

        private static SelectElement DienosPasirinkimoSarasas => new SelectElement(Driver.FindElement(By.Id("select-demo")));
        private static IWebElement PasirinktosDienosPavadinimas => Driver.FindElement(By.ClassName("selected-value"));

        private static SelectElement MiestuPasirinkimoSarasas => new SelectElement(Driver.FindElement(By.Id("multi-select")));
        private static IWebElement GetAllSelectedMygtukas => Driver.FindElement(By.Id("printAll"));
        private static IWebElement RezultatoZinute => Driver.FindElement(By.ClassName("getall-selected"));


        public SelectDemoDropdownPage(IWebDriver _driver) : base(_driver)
        {
            Driver.Url = "https://www.seleniumeasy.com/test/basic-select-dropdown-demo.html";
        }

        public SelectDemoDropdownPage PasirinktaDiena(DayOfWeek dayOfWeek)
        {
            DienosPasirinkimoSarasas.SelectByValue(dayOfWeek.ToString());
            return this;
        }

        public SelectDemoDropdownPage PatikrintiPasirinktaDiena(DayOfWeek tiketinasPasirinkimas)
        {
            Assert.AreEqual($"Day selected :- {tiketinasPasirinkimas}", PasirinktosDienosPavadinimas.Text);
            return this;
        }

        public SelectDemoDropdownPage PasirinkReiksmeIsDidelioLauko(string reiksme)
        {
            MiestuPasirinkimoSarasas.SelectByValue(reiksme);
            return this;
        }

        public SelectDemoDropdownPage PasirinkReiksmesIsDidelioLauko(string[] elementuValuesKuriuosPasirinksime)
        {
            var actions = new Actions(Driver).KeyDown(Keys.LeftControl);

            foreach (var option in MiestuPasirinkimoSarasas.Options)
                if (elementuValuesKuriuosPasirinksime.Contains(option.GetAttribute("value")))
                    actions.Click(option);

            actions.KeyUp(Keys.LeftControl).Build().Perform();
            return this;
        }

        public SelectDemoDropdownPage PaspauskGetAllSelected()
        {
            GetAllSelectedMygtukas.Click();
            return this;
        }

        public SelectDemoDropdownPage TikrinkPasirinktasReiksmes(string[] elementuValuesKuriuosPasirinksime)
        {
            string message = RezultatoZinute.Text;
            Assert.IsTrue(message.StartsWith("Options selected are : "), "Expected message to start with 'Options selected are : '");
            foreach (var reiksme in elementuValuesKuriuosPasirinksime)
                Assert.IsTrue(message.Contains(reiksme), $"Expected '{message}' to contain '{reiksme}'");

            return this;
        }

        public SelectDemoDropdownPage TikrinkPasirinktaReiksme(string reiksme)
        {
            string message = RezultatoZinute.Text;
            Assert.AreEqual($"Options selected are : {reiksme}", message, "Uzrasas nesutampa");
            return this;
        }
    }
}
