using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace FrontendRestauranteMarisco.Tests
{
    [TestClass]
    public class PlatilloControllerTests
    {
        private IWebDriver driver;
        private string baseUrl = "https://localhost:7190"; // Tu URL base

        [TestInitialize]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }

        [TestMethod]
        public void CrearEditarEliminar_Platillo_OK()
        {
            // 1. Login
           // driver.Navigate().GoToUrl(baseUrl + "/Auth/Login");
            driver.FindElement(By.Id("Input_Email")).SendKeys("admin@correo.com"); // Ajusta según tu login
            driver.FindElement(By.Id("Input_Password")).SendKeys("123456");
            driver.FindElement(By.Id("btnLogin")).Click();
            Thread.Sleep(2000); // Espera a que cargue la página

            // 2. Ir a Platillo/Index
            driver.Navigate().GoToUrl(baseUrl + "/Platillo");
            Thread.Sleep(2000);

            // 3. Crear nuevo platillo
            driver.FindElement(By.LinkText("Crear nuevo")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Id("Nombre")).SendKeys("Platillo Selenium");
            driver.FindElement(By.Id("Descripcion")).SendKeys("Descripción de prueba");
            driver.FindElement(By.Id("Precio")).SendKeys("15.5");
            var categoriaSelect = driver.FindElement(By.Id("CategoriaId"));
            var option = categoriaSelect.FindElement(By.XPath("//option[2]")); // Selecciona segunda opción
            option.Click();

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);

            // Verificar que fue creado
            Assert.IsTrue(driver.PageSource.Contains("platillo creado con éxito"));

            // 4. Editar platillo
            driver.FindElement(By.LinkText("Editar")).Click(); // Edita el primero de la lista
            Thread.Sleep(1000);
            var nombreInput = driver.FindElement(By.Id("Nombre"));
            nombreInput.Clear();
            nombreInput.SendKeys("Platillo Selenium Editado");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);

            Assert.IsTrue(driver.PageSource.Contains("Platillo Selenium Editado"));

            // 5. Eliminar platillo
            driver.FindElement(By.LinkText("Eliminar")).Click(); // Elimina el primero de la lista
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);

            Assert.IsTrue(driver.PageSource.Contains("platillo eliminado con éxito"));
        }
    }
}
