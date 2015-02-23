﻿using System;
using FluentAssertions;
using MDXEngine;
using MDXEngine.Textures;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTests
{
    [TestClass]
    public class CommandsSequenceUnitTests
    {
        private HLSLProgram _hlslProgram;
        private CommandsSequence _commands;


        public CommandsSequenceUnitTests()
        {
            _hlslProgram = Utilities.GetHLSLProgramWithTwoTextureResources();
            _commands = new CommandsSequence(_hlslProgram);
        }

        [TestMethod]
        public void CommandsSequence_Load_A_Resource_Already_Loaded_Always_Succeed()
        {
            var command = new CommandsSequence(_hlslProgram);
            var texture = new Texture(Utilities.GetDxContext(), "./Images/fox.jpg");

            command.TryAddCommand("texture1", texture).Should().BeTrue();
            command.TryAddCommand("texture1", texture).Should().BeTrue();
            command.AddCommand("texture1", texture);
        }

        [TestMethod]
        public void CommandsSequence_Load_A_Resource_In_A_Already_Filled_Slot_Should_Fail()
        {
            var command = new CommandsSequence(_hlslProgram);
            var texture1 = new Texture(Utilities.GetDxContext(), "./Images/fox.jpg");
            var texture2 = new Texture(Utilities.GetDxContext(), "./Images/fox.jpg");
           
            command.TryAddCommand("texture1", texture1).Should().BeTrue();
            command.TryAddCommand("texture1", texture2).Should().BeFalse();
            command.Invoking(x=>x.AddCommand("texture1", texture2)).ShouldThrow<Exception>();
        }


        [TestMethod]
        public void CommandsSequence_Merging_Two_Sequences_That_Load_Resources_In_Different_Slots_Should_Succeed()
        {
            var command1 = new CommandsSequence(_hlslProgram);
            var command2 = new CommandsSequence(_hlslProgram);
        
            var texture1 = new Texture(Utilities.GetDxContext(), "./Images/fox.jpg");
            var texture2 = new Texture(Utilities.GetDxContext(), "./Images/fox.jpg");

            command1.TryAddCommand("texture1", texture1).Should().BeTrue();
            command2.TryAddCommand("texture2", texture2).Should().BeTrue();
            command1.CanMerge(command2).Should().BeTrue();
            command2.CanMerge(command1).Should().BeTrue();
            command1.TryMerge(command2).Should().BeTrue();
            command1.Count.Should().Be(2);
           
        }


    }
}
