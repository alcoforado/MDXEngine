using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should.Fluent;
namespace UnitTests
{
    [Binding]
    public class MakingCalculations
    {
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef
        int a, b,c;
        [Given("I enter 3 plus 4")]
        public void GivenIEnter3Plus4()
        {
            //TODO: implement arrange (precondition) logic
            // For storing and retrieving scenario-specific data see http://go.specflow.org/doc-sharingdata 
            // To use the multiline text or the table argument of the scenario,
            // additional string/Table parameters can be defined on the step definition
            // method. 

            a = 3;
            b = 4;
        }

        [When("I press add")]
        public void WhenIPressAdd()
        {
            //TODO: implement act (action) logic
            c = a + b;
        }

        [Then("the result should be 7 on the screen")]
        public void TheResultShouldBe7OnTheScreen()
        {
            //TODO: implement assert (verification) logic
            
            c.Should().Be.Equals(7);
        }
    }
}
