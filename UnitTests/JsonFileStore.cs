using System;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDX;
using TestApp.Services;

namespace TestApp.Tests
{
    [TestClass]
    public class JsonStoreTest
    {
        private class DogTest
        {
            public int id { get; set; }
            public string str { get; set; }


        }

        [TestInitialize]
        public void Initialize()
        {
           

        }




        [TestMethod]
        public void OpenAndSaveEmptyFileShouldSaveNewFile()
        {
            if (File.Exists("./notest.json"))
                File.Delete("./notest.json");
            using (var store = new JsonFileStore())
            {
                store.Open("./notest.json");
            }
            File.Exists("./notest.json").Should().BeTrue();
        }

        [TestMethod]
        public void ObjectShouldBePersisted()
        {
            if (File.Exists("./test.json"))
                File.Delete("./test.json");
            DogTest test = new DogTest() {id = 5,str = "Rex"};
            using (var store = new JsonFileStore())
            {
                store.Open("./test.json");
                store.Save("Dog1",test);
            }
            System.Diagnostics.Debug.Write(File.ReadAllText("./test.json"));
        }


    }
}
