using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyConvertor.Engine;

namespace CurrencyConvertorTest
{
    [TestClass]
    public class CurrencyConvertorTest
    {
        [TestMethod]
        [ExpectedException(typeof(System.Net.WebException))]
        public void InvalidCurrencyType_ThrowException()
        {
            Currency testCurrency = new Currency();
            testCurrency.SetType("XXX");
            testCurrency.SetValue(1);
            testCurrency.Convert("ILS");
        }

        [TestMethod]
        public void SelfConvert_Return1()
        {
            Currency testCurrency = new Currency();
            testCurrency.SetType("ILS");
            testCurrency.SetValue(1);
            Assert.AreEqual(1.0f, testCurrency.Convert("ILS"));
        }

        [TestMethod]
        public void GetListOfCurrencies_ListIsNotNull()
        {
            Currency testCurrency = new Currency();
            Assert.IsNotNull(testCurrency.GetConvertibleTypes());
        }
    }
}
