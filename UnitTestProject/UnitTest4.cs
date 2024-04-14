using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Controls;
using UnitTest;
using static UnitTest.RegPage;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void RegistrationTestSuccess()
        {
            var page = new RegPage();
            Assert.IsTrue(page.Reg("sdfsdf", "sdfsd", "123123", "Мужской", "Администратор", "9260436408", "knnkl"));
        }
    }
}
