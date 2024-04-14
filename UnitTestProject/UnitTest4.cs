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
            Assert.IsTrue(page.Reg("Иванов Иван Иванович", "Ivan2423", "23421", "Мужской", "Администратор", "9262322222", "knnkl"));
        }

        [TestMethod]
        public void RegistrationTestNegative()
        {
            var page = new RegPage();
            Assert.IsFalse(page.Reg("", "Ivan2423", "23421", "Мужской", "Администратор", "9262322222", "knnkl"));
            Assert.IsFalse(page.Reg("Иванов Иван Иванович", "", "23421", "Мужской", "Администратор", "9262322222", "knnkl"));
            Assert.IsFalse(page.Reg("Иванов Иван Иванович", "Ivan2423", "", "Мужской", "Администратор", "9262322222", "knnkl"));
            Assert.IsFalse(page.Reg("Иванов Иван Иванович", "Ivan2423", "23421", "", "Администратор", "9262322222", "knnkl"));
            Assert.IsFalse(page.Reg("Иванов Иван Иванович", "Ivan2423", "23421", "Мужской", "", "9262322222", "knnkl"));
            Assert.IsFalse(page.Reg("Иванов Иван Иванович", "Ivan2423", "23421", "Мужской", "Администратор", "", "knnkl"));
            Assert.IsFalse(page.Reg("Иванов Иван Иванович", "Ivan2423", "23421", "Мужской", "Администратор", "9262322222", ""));
            Assert.IsFalse(page.Reg("Чашин Елизар Михеевич", "Elizor@gmai,com", "yntiRS", "Мужской", "Администратор", "10706282916", "padsp"));
        }
    }
}
