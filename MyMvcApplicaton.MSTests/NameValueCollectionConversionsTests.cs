using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcIntegrationTestFramework;
using System.Collections.Specialized;

namespace MyMvcApplicaton.MSTests
{
    [TestClass]
    public class When_converting_an_object_with_one_string_property_to_name_value_collection
    {
        private NameValueCollection convertedFromObjectWithString;

        public When_converting_an_object_with_one_string_property_to_name_value_collection()
        {
            convertedFromObjectWithString = NameValueCollectionConversions.ConvertFromObject(new { name = "hello" });

        }

        [TestMethod]
        public void Should_have_key_of_name_with_value_hello()
        {
            Assert.AreEqual("hello", convertedFromObjectWithString["name"]);
        }
    }
}
