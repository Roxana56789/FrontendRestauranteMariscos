using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace FrontendRestauranteMarisco.WebApp.TestUI
{
    [TestClass]
    public class AuthControllerTests
    {
        private IWebDriver _driver;
        private readonly string _urlBase = "https://localhost:7190"; 

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        [TestMethod]
        public void Login_CredencialesCorrectas_RedireccionaHome()
        {
            // Navega al formulario de login
            _driver.Navigate().GoToUrl($"{_urlBase}/Auth/Login");

            // Espera a que cargue la página (opcional, evita errores por carga lenta)
            Thread.Sleep(1000);

            // Rellena los campos de login (ajusta los 'name' según tu vista)
            _driver.FindElement(By.Name("Email")).SendKeys("kenia@gmail.com");
            _driver.FindElement(By.Name("Password")).SendKeys("kenia123");

            // Envía el formulario
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            // Espera un poco para la redirección
            Thread.Sleep(2000);

            // Verifica que redirige a la página Home
            Assert.IsTrue(_driver.Url.Contains($"{_urlBase}/Home/Index") || _driver.Url == $"{_urlBase}/",
                $"No se redirigió correctamente al Home. URL actual: {_driver.Url}");
        }

        [TestCleanup]
        public void Cleanup()
        {
            _driver.Quit();
        }
    }
}
