using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VcsWebdriver.Drivers;
using VcsWebdriver.Pages;

namespace VcsWebdriver.Tests
{
    class SelectDemoPageDropdownTest
    {

        private static SelectDemoDropdownPage _page;

        [OneTimeSetUp]

        public static void SetUpChrome()
        {
            var driver = CustomDrivers.GetChromeDriver();
            _page = new SelectDemoDropdownPage(driver);
        }

        [TestCase(DayOfWeek.Tuesday)]
      
        public static void DayOfWeekDropDown(DayOfWeek testuojamaDiena)
        {
            _page.PasirinktaDiena(testuojamaDiena).PatikrintiPasirinktaDiena(testuojamaDiena);
        }

        [TestCase("New York", TestName = "Pasirenkame viena reiksme ir patikriname")]
        [TestCase("New Jersey", TestName = "Pasirenkame kita reiksme ir patikriname")]
        public static void PasirinktaVienaReiksme(string elementoValueKuriPasirinksime)
        {
            _page.PasirinkReiksmeIsDidelioLauko(elementoValueKuriPasirinksime)
                .PaspauskGetAllSelected()
                .TikrinkPasirinktaReiksme(elementoValueKuriPasirinksime);
        }

        [TestCase("New Jersey", "California", TestName = "Pasirenkame 2 reiksmes ir patikriname")]
        [TestCase("Washington", "Ohio", "Texas", TestName = "Pasirenkame 3 reiksmes ir patikriname")]
        [TestCase("Washington", "Ohio", "Texas", "Florida", TestName = "Pasirenkame 4 reiksmes ir patikriname")]
        public static void PasirinktiKeliasReiksmes(params string[] elementuValuesKuriuosPasirinksime)
        {
            _page.PasirinkReiksmesIsDidelioLauko(elementuValuesKuriuosPasirinksime)
                .PaspauskGetAllSelected()
                .TikrinkPasirinktasReiksmes(elementuValuesKuriuosPasirinksime);
        }

        [OneTimeTearDown]
        public static void CloseBroser()
        {
            _page.CloseBrowser();
        }
    }
}
