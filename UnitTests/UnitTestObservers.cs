using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
namespace UnitTests
{
    [TestClass]
    public class ObserversTest
    {
        class Test : Observable<Test>
        {
            public Test() { }

            public void TriggerChange()
            {
                this.OnChanged(this);
            }
        }

        class Obs : MDXEngine.IObserver<Test>
        {
            public int sum;
            
            public Obs()
            {
                sum = 0;
            }

            public  void Changed(Test test)
            {
                sum++;

            }

        }

        [TestMethod]
        public void Observers_ObservableShouldTriggerAllObservers()
        {
            var ob1 = new Obs();
            var ob2 = new Obs();

            var t = new Test();
            t.AttachObserver(ob1);
            t.AttachObserver(ob2);

            t.TriggerChange();

            ob1.sum.Should().Be(1);
            ob2.sum.Should().Be(1);

        }


        [TestMethod]
        public void Observers_ObservableAddedTwiceIsTheSameAsAddedOnce()
        {
            var ob1 = new Obs();
            var ob2 = new Obs();

            var t = new Test();
            t.AttachObserver(ob1);
            t.AttachObserver(ob1);
            t.AttachObserver(ob2);
            t.AttachObserver(ob2);
            t.AttachObserver(ob2);

            t.TriggerChange();

            ob1.sum.Should().Be(1);
            ob2.sum.Should().Be(1);
        }


        [TestMethod]
        public void Observers_ObservableRemovedShouldNotBeCalledAgain()
        {
            var ob1 = new Obs();
            var ob2 = new Obs();

            var t = new Test();
            t.AttachObserver(ob1);
            t.AttachObserver(ob2);

            t.TriggerChange();

            ob1.sum.Should().Be(1);
            ob2.sum.Should().Be(1);

            t.DetachObserver(ob2);

            t.TriggerChange();
            ob1.sum.Should().Be(2);
            ob2.sum.Should().Be(1);

        
        }

        [TestMethod]
        public void Observers_ObservableRemovedTwiceShouldNotBeCalledAgain()
        {
            var ob1 = new Obs();
            var ob2 = new Obs();

            var t = new Test();
            t.AttachObserver(ob1);
            t.AttachObserver(ob2);

            t.TriggerChange();

            ob1.sum.Should().Be(1);
            ob2.sum.Should().Be(1);

            t.DetachObserver(ob2);
            t.DetachObserver(ob2);
            t.TriggerChange();
            ob1.sum.Should().Be(2);
            ob2.sum.Should().Be(1);


        }
    }
}
