using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MDXEngine;
using Should.Fluent;
using System.Collections.Generic;
namespace UnitTests
{
    [TestClass]
    public class UnitTestNTree
    {


        public NTreeNode<int> SetTree1()
        {
            var tree1 = new NTreeNode<int>(1);
            tree1.AppendChild(new NTreeNode<int>(2));
            tree1.AppendChild(new NTreeNode<int>(3));
            tree1.AppendChild(new NTreeNode<int>(4));
            tree1.GetChild(1).AppendChild(new NTreeNode<int>(5));
            return tree1;
        }

        

        [TestMethod]
        public void NTree_ANewNodeShouldBeAChildlessRootNode()
        {
            var node = new NTreeNode<int>(1);

            node.IsChildless().Should().Be.True();
            node.IsRoot().Should().Be.True();
        }

        [TestMethod]
        public void NTree_ANodeAppendedToARootShouldHaveTheRootAsParentAndShouldBeTheLastChild()
        {
            var node0 = new NTreeNode<int>(0);
            var node1 = new NTreeNode<int>(1);
            var node2 = new NTreeNode<int>(2);

            node0.AppendChild(node1);
            node0.AppendChild(node2);

            node1.GetParent().Should().Be.Equals(node0);
            node2.GetParent().Should().Be.Equals(node0);
        }
         [TestMethod]
        public void NTree_TestingGetChildMethod()
        {
            var tree1 = new NTreeNode<int>(1);
            tree1.AppendChild(new NTreeNode<int>(2));
            tree1.AppendChild(new NTreeNode<int>(3));
            tree1.AppendChild(new NTreeNode<int>(4));
            tree1.GetChild(0).GetData().Should().Be.Equals(2);
            tree1.GetChild(1).GetData().Should().Be.Equals(3);
            tree1.GetChild(2).GetData().Should().Be.Equals(4);

        }

        [TestMethod]
        public void NTree_IteratorAtRootShouldNeverGotoParent()
         {
             var tree1=SetTree1();
             var it = tree1.GetIterator();
             it.GotoParent().Should().Be.False();
             it.GetData().Should().Be.Equals(tree1.GetData());
         }

        [TestMethod]
        public void NTree_IteratorGoToChildAndReturnToParent()
        {
            var tree1 = SetTree1();
            var it = tree1.GetIterator();
            it.GotoChild().Should().Be.True();
            it.GetData().Should().Be.Equals(2);
            it.GotoParent().Should().Be.True();
            it.GetData().Should().Be.Equals(1);
        }
        [TestMethod]
        public void NTree_IteratorGoToSiblingsNext()
        {
            var tree1 = SetTree1();
            var it = tree1.GetIterator();
            it.GotoChild().Should().Be.True();
            it.GetData().Should().Be.Equals(2);
            it.GotoNextSibling().Should().Be.True();
            it.GetData().Should().Be.Equals(3);
            it.GotoNextSibling().Should().Be.True();
            it.GetData().Should().Be.Equals(4);
            it.GotoNextSibling().Should().Be.False();
            it.GetData().Should().Be.Equals(4);
        }
        [TestMethod]
        public void NTree_IteratorGoToSiblingPrev()
        {
            var tree1 = SetTree1();
            var it = tree1.GetChilds()[tree1.GetChilds().Count-1].GetIterator();
            it.GetData().Should().Be.Equals(4);
            it.GotoPrevSibling().Should().Be.True();
            it.GetData().Should().Be.Equals(3);
            it.GotoPrevSibling().Should().Be.True();
            it.GetData().Should().Be.Equals(2);
            it.GotoPrevSibling().Should().Be.False();
            it.GetData().Should().Be.Equals(2);
        }

        [TestMethod]
        public void NTree_ForAllParents()
        {
            var lst = new List<int>();
            var tree1 = SetTree1();
            var node = tree1.GetChilds()[1].GetChild(0);
            node.ForAllParents(nd => lst.Add(nd.GetData()));

            //check
            lst.Count.Should().Be.Equals(2);
            lst[0].Should().Be.Equals(2);
            lst[1].Should().Be.Equals(1);
        }

        [TestMethod]
        public void NTree_ForAllInOrder()
        {
            var lst = new List<int>();
            var tree1 = SetTree1();
            tree1.ForAllInOrder( node =>  lst.Add(node.GetData()));

            lst.Count.Should().Be.Equals(5);
            lst[0].Should().Be.Equals(2);
            lst[1].Should().Be.Equals(5);
            lst[2].Should().Be.Equals(3);
            lst[3].Should().Be.Equals(4);
            lst[4].Should().Be.Equals(1);



        }



    }

   

}
