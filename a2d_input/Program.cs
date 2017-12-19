using System;
using a2d_input.Commands;
using CommandLine;

namespace a2d_input
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var parserResult = Parser.Default.ParseArguments<GetDevicesOptions, TestSignalOptions>(args);

            var devicesCommand = new Func<GetDevicesOptions, int>(RunHandler);
            var testCommand = new Func<TestSignalOptions, int>(RunHandler);

            parserResult.MapResult(devicesCommand, testCommand, errs => 1);

            Console.ReadKey();
        }

        private static int RunHandler(CommandOptions options)
        {
            IHandler handler = null;

            var testOptions = options as TestSignalOptions;
            if (testOptions != null) handler = new TestSignalHandler(testOptions);

            var devicesOptions = options as GetDevicesOptions;
            if (devicesOptions != null) handler = new GetDevicesHandler(devicesOptions);

            if (handler == null) throw new NotSupportedException();

            return handler.Run();
        }
    }
}