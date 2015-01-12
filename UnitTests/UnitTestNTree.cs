using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MDXEngine;
using FluentAssertions;
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

            node.IsChildless().Should().BeTrue();
            node.IsRoot().Should().BeTrue();
        }

        [TestMethod]
        public void NTree_ANodeAppendedToARootShouldHaveTheRootAsParentAndShouldBeTheLastChild()
        {
            var node0 = new NTreeNode<int>(0);
            var node1 = new NTreeNode<int>(1);
            var node2 = new NTreeNode<int>(2);

            node0.AppendChild(node1);
            node0.AppendChild(node2);

            node1.GetParent().Should().Be(node0);
            node2.GetParent().Should().Be(node0);
        }
         [TestMethod]
        public void NTree_TestingGetChildMethod()
        {
            var tree1 = new NTreeNode<int>(1);
            tree1.AppendChild(new NTreeNode<int>(2));
            tree1.AppendChild(new NTreeNode<int>(3));
            tree1.AppendChild(new NTreeNode<int>(4));
            tree1.GetChild(0).GetData().Should().Be(2);
            tree1.GetChild(1).GetData().Should().Be(3);
            tree1.GetChild(2).GetData().Should().Be(4);

        }

        [TestMethod]
        public void NTree_IteratorAtRootShouldNeverGotoParent()
         {
             var tree1=SetTree1();
             var it = tree1.GetIterator();
             it.GotoParent().Should().BeFalse();
             it.GetData().Should().Be(tree1.GetData());
         }

        [TestMethod]
        public void NTree_IteratorGoToChildAndReturnToParent()
        {
            var tree1 = SetTree1();
            var it = tree1.GetIterator();
            it.GotoChild().Should().BeTrue();
            it.GetData().Should().Be(2);
            it.GotoParent().Should().BeTrue();
            it.GetData().Should().Be(1);
        }
        [TestMethod]
        public void NTree_IteratorGoToSiblingsNext()
        {
            var tree1 = SetTree1();
            var it = tree1.GetIterator();
            it.GotoChild().Should().BeTrue();
            it.GetData().Should().Be(2);
            it.GotoNextSibling().Should().BeTrue();
            it.GetData().Should().Be(3);
            it.GotoNextSibling().Should().BeTrue();
            it.GetData().Should().Be(4);
            it.GotoNextSibling().Should().BeFalse();
            it.GetData().Should().Be(4);
        }
        [TestMethod]
        public void NTree_IteratorGoToSiblingPrev()
        {
            var tree1 = SetTree1();
            var it = tree1.GetChilds()[tree1.GetChilds().Count-1].GetIterator();
            it.GetData().Should().Be(4);
            it.GotoPrevSibling().Should().BeTrue();
            it.GetData().Should().Be(3);
            it.GotoPrevSibling().Should().BeTrue();
            it.GetData().Should().Be(2);
            it.GotoPrevSibling().Should().BeFalse();
            it.GetData().Should().Be(2);
        }

        [TestMethod]
        public void NTree_ForAllParents()
        {
            var lst = new List<int>();
            var tree1 = SetTree1();
            var node = tree1.GetChilds()[1].GetChild(0);
            node.ForAllParents(nd => lst.Add(nd.GetData()));

            //check
            lst.Count.Should().Be(2);
            lst[0].Should().Be(3);
            lst[1].Should().Be(1);
        }

        [TestMethod]
        public void NTree_ForAllInOrder()
        {
            var lst = new List<int>();
            var tree1 = SetTree1();
            tree1.ForAllInOrder( node =>  lst.Add(node.GetData()));

            lst.Count.Should().Be(5);
            lst[0].Should().Be(2);
            lst[1].Should().Be(5);
            lst[2].Should().Be(3);
            lst[3].Should().Be(4);
            lst[4].Should().Be(1);
        }
        
       


    }

   

}
