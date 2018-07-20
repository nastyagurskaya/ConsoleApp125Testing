using ConsoleApp125;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConsoleApp125Tests
{
    public class Actioner_Should
    {
        private readonly Actioner _commandClass;
        private readonly IProcessService _service;

        public Actioner_Should()
        {
            _service = Substitute.For<IProcessService>();
            _commandClass = Substitute.For<Actioner>(_service);
        }
        //[Fact(DisplayName = "Call Correct Execute Command")]
        //public void ServiceReceived()
        //{
        //    _commandClass.ExecuteCommand("calc");
        //    _service.Received().Start("calc");
        //}
        [Theory]
        [InlineData("calc", "Executed")]
        [InlineData("", "Executed")]
        [InlineData("notepad", "Executed")]
        public void Call_CorrectExecuteCommand(string command, string expected)
        {
            //_commandClass.ExecuteCommand(Arg.Any<string>());
            _service.Start(Arg.Any<Process>()).Returns(true);
            string result = _commandClass.ExecuteCommand(command);
            Assert.Equal(result, expected);
        }
        [Theory]
        [InlineData("fhgtbnhgjn")]
        [InlineData("note")]
        public void Call_ThrowsExecuteCommand(string command)
        {
            Assert.Throws<Exception>(() => _commandClass.ExecuteCommand(command));
        }
        //[Theory]
        //[InlineData("fhgtbnhgjn")]
        //[InlineData("note")]
        //public void Call_ThrowsExecuteCommand(string command)
        //{
        //    //_commandClass.ExecuteCommand(Arg.Any<string>());
        //    Assert.Throws<Exception>(() => _commandClass.ExecuteCommand(command));
        //}
    }
}
