using System;
using System.Linq;
using a2d_input.Commands;
using CommandLine;

namespace a2d_input
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var parserResult = Parser.Default.ParseArguments<GetDevicesCommand, RunHookerCommand>(args);
            var erros = parserResult.Errors.ToList();

            if (erros.Any())
            {
                foreach (var error in erros)
                    Console.WriteLine(error);
            }
            else
            {
                if (parserResult.Value is GetDevicesCommand deviceCommand)
                    GetDevicesCommand.Run(deviceCommand);
            }


            Console.ReadKey();
        }
    }
}