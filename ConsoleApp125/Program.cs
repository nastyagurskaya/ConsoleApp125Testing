using System;

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
            bool started = _proc.Start(command);
            if (started) return "Command excuted";
            else return "Executing failed";
            //var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command)
            //{
            //    RedirectStandardOutput = true,
            //    RedirectStandardError = true,
            //    UseShellExecute = false,
            //    CreateNoWindow = true
            //};

            //var proc = new System.Diagnostics.Process {StartInfo = procStartInfo};
            //proc.Start();
            //var result = proc.StandardOutput.ReadToEnd();
            //var resultError = proc.StandardError.ReadToEnd();
            //var error = proc.ExitCode;

            //if (error != 0)
            //{
            //    throw new Exception(result + resultError);
            //}

            //return result + resultError;
        }
    }
    public interface IProcessService
    {
        bool Start(string command);
    }
    public class ProcessService : IProcessService
    {
        public bool Start(string command)
        {
            var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var proc = new System.Diagnostics.Process { StartInfo = procStartInfo };
            proc.Start();
            var result = proc.StandardOutput.ReadToEnd();
            var resultError = proc.StandardError.ReadToEnd();

            var error = proc.ExitCode;

            if (error != 0)
            {
                return false;
            }

            return true;
        }
    }
}
