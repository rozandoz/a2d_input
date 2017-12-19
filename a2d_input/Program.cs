using System;
using a2d_input.Commands;
using CommandLine;

namespace a2d_input
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var parserResult = Parser.Default.ParseArguments<GetDevicesCommand, TestSignalCommand>(args);

            var devicesCommand = new Func<GetDevicesCommand, int>(GetDevicesCommand.Run);
            var testCommand = new Func<TestSignalCommand, int>(TestSignalCommand.Run);

            parserResult.MapResult(devicesCommand, testCommand, errs => 1);

            Console.ReadKey();
        }
    }
}