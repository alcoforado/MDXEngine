using System;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void ObjectsShouldBePersistedInFile()
        {
            if (File.Exists("./test.json"))
                File.Delete("./test.json");
            DogTest test = new DogTest() { id = 5, str = "Rex" };
            DogTest test2 = new DogTest() { id = 7, str = "Snoopy" };

            using (var store = new JsonFileStore())
            {
                store.Open("./test.json");
                store.Save("Dog1", test);
                store.Save("Dog2", test2);
            }
            System.Diagnostics.Debug.Write(File.ReadAllText("./test.json"));
            //var str = File.ReadAllText("./test.json");
            File.ReadAllText("./test.json").ShouldBeEquivalentTo("{\r\n  \"Dog1\": {\r\n    \"$type\": \"TestApp.Tests.JsonStoreTest+DogTest, TestApp.Tests\",\r\n    \"id\": 5,\r\n    \"str\": \"Rex\"\r\n  },\r\n  \"Dog2\": {\r\n    \"$type\": \"TestApp.Tests.JsonStoreTest+DogTest, TestApp.Tests\",\r\n    \"id\": 7,\r\n    \"str\": \"Snoopy\"\r\n  }\r\n}");
        }

        [TestMethod]
        public void ObjectsShouldBePersisted()
        {
            if (File.Exists("./test.json"))
                File.Delete("./test.json");
            DogTest test = new DogTest() { id = 5, str = "Rex" };
            DogTest test2 = new DogTest() { id = 7, str = "Snoopy" };

            using (var store = new JsonFileStore())
            {
                store.Open("./test.json");
                store.Save("Dog1", test);
                store.Save("Dog2", test2);
                var result = store.Load<DogTest>("Dog2");
                result.ShouldBeEquivalentTo(test2);
            }

        }


    }
}
