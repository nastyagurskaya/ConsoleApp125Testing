using System;
using System.Diagnostics;

namespace ConsoleApp125
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandToExec = "calc";
            ProcessService service = new ProcessService();
            Console.WriteLine($@"Starting Actioner with ""command"" param: {commandToExec}");
            var actionResult = new Actioner(service).ExecuteCommand(commandToExec);
            Console.WriteLine($"Action Result: {actionResult}");
            Console.ReadKey();
        }
    }

    public class Actioner
    {
        private readonly IProcessService _proc;
        public Actioner(IProcessService proc)
        {
            _proc = proc;
        }
        public string ExecuteCommand(string command)
        {
            var procStartInfo = new ProcessStartInfo("cmd", "/c " + command)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var proc = new Process {StartInfo = procStartInfo};
            bool  started = _proc.Start(proc);
            //proc.Start();
            //var result = proc.StandardOutput.ReadToEnd();
            //var resultError = proc.StandardError.ReadToEnd();
            //var error = proc.ExitCode;

            if (!started)
            {
                throw new Exception("Failed");
            }

            return "Executed";
        }
    }
    public interface IProcessService
    {
        bool Start(Process proc);
    }
    public class ProcessService : IProcessService
    {
        public bool Start(Process proc)
        {
            proc.Start();
            var result = proc.StandardOutput.ReadToEnd();
            var resultError = proc.StandardError.ReadToEnd();
            var er = proc.ExitCode;
            if (er != 0) return false;

            return true;
        }
    }
}
