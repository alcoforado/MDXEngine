using System;
using FluentAssertions;
using MDXEngine;
using MDXEngine.Textures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MDXEngine.DrawTree;
using MDXEngine.Interfaces;

namespace UnitTests
{
    [TestClass]
    public class CommandsSequenceUnitTests
    {
        private HLSLProgram _hlslProgram;
        private LoadCommandsSequence _commands;
        private Mock<ILoadCommand> _load1;
        private Mock<ILoadCommand> _load2;

        public CommandsSequenceUnitTests()
        {
            _hlslProgram = Utilities.GetHLSLProgramWithTwoTextureResources();
            _commands = new LoadCommandsSequence(_hlslProgram);
            _load1 = new Mock<ILoadCommand>();
            _load2 = new Mock<ILoadCommand>();
        }

        [TestMethod]
        public void CommandsSequence_Load_A_Resource_Already_Loaded_Always_Succeed()
        {
            var command = new LoadCommandsSequence(_hlslProgram);

            _load1.Setup(x => x.CanBeOnSameSlot(It.IsAny<ILoadCommand>())).Returns(true);
            _load1.SetupGet(x=>x.SlotName).Returns("texture1");
            
            _load2.Setup(x => x.CanBeOnSameSlot(It.IsAny<ILoadCommand>())).Returns(true);
            _load2.SetupGet(x => x.SlotName).Returns("texture1");

            command.TryAddLoadCommand(_load1.Object).Should().BeTrue();
            command.TryAddLoadCommand(_load2.Object).Should().BeTrue();
            
        }

        [TestMethod]
        public void CommandsSequence_Load_A_Resource_In_A_Already_Filled_Slot_Should_Fail()
        {
            var command = new LoadCommandsSequence(_hlslProgram);

            _load1.Setup(x => x.CanBeOnSameSlot(It.IsAny<ILoadCommand>())).Returns(false);
            _load1.SetupGet(x => x.SlotName).Returns("texture1");

            _load2.Setup(x => x.CanBeOnSameSlot(It.IsAny<ILoadCommand>())).Returns(false);
            _load2.SetupGet(x => x.SlotName).Returns("texture1");
            
           
            command.TryAddLoadCommand(_load1.Object).Should().BeTrue();
            command.TryAddLoadCommand(_load2.Object).Should().BeFalse();
            command.Invoking(x=> x.Add(_load2.Object)).ShouldThrow<Exception>();
        }


        [TestMethod]
        public void CommandsSequence_Merging_Two_Sequences_That_Load_Resources_In_Different_Slots_Should_Succeed()
        {
            var command1 = new LoadCommandsSequence(_hlslProgram);
            var command2 = new LoadCommandsSequence(_hlslProgram);

            _load1.Setup(x => x.CanBeOnSameSlot(It.IsAny<ILoadCommand>())).Returns(false);
            _load1.SetupGet(x => x.SlotName).Returns("texture1");

            _load2.Setup(x => x.CanBeOnSameSlot(It.IsAny<ILoadCommand>())).Returns(false);
            _load2.SetupGet(x => x.SlotName).Returns("texture2");

            command1.TryAddLoadCommand(_load1.Object).Should().BeTrue();
            command2.TryAddLoadCommand(_load2.Object).Should().BeTrue();
            command1.CanMerge(command2).Should().BeTrue();
            command2.CanMerge(command1).Should().BeTrue();
            command1.TryMerge(command2).Should().BeTrue();
            command1.Count.Should().Be(2);
           
        }

        [TestMethod]
        public void CommandsSequence_Execute_Should_Load_All_Resources()
        {
            var command = new LoadCommandsSequence(_hlslProgram);
            var res1 = new Mock<IShaderResource>();
            var res2 = new Mock<IShaderResource>();

            _load1.Setup(x => x.CanBeOnSameSlot(It.IsAny<ILoadCommand>())).Returns(false);
            _load1.SetupGet(x => x.SlotName).Returns("texture1");

            _load2.Setup(x => x.CanBeOnSameSlot(It.IsAny<ILoadCommand>())).Returns(false);
            _load2.SetupGet(x => x.SlotName).Returns("texture2");
          
            command.TryAddLoadCommand(_load1.Object).Should().BeTrue();
            command.TryAddLoadCommand(_load2.Object).Should().BeTrue();

            command.Execute();

            _load1.Verify(x => x.Load());
            _load2.Verify(x => x.Load());

        }


    }
}
