using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace tester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wait 10 seconds...");

            Dictionary<string, (int,int)> userFails = new();
            int i = 0;
            DateTime lastTime = DateTime.Now;
            var testExe = $@"{Path.GetDirectoryName(System.Environment.ProcessPath)}\..\..\..\..\test\bin\Debug\net8.0\test.exe";
            while (true)
            {
                i++;

                string randomArgs = GetRandomArgs();

                var p = new Process();

                p.StartInfo.FileName = testExe;
                p.StartInfo.Arguments = randomArgs;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();

                var output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Dispose();

                var res = JsonSerializer.Deserialize<Res>(output);

                foreach (var subRes in res.SubRes)
                {
                    userFails.TryAdd(subRes.Author, (0,0));
                    if (!subRes.Success)
                    {
                        var ex = userFails[subRes.Author];
                        ex.Item1++;
                        if (subRes.Exception)
                            ex.Item2++;
                        userFails[subRes.Author] = ex;
                    }
                }

                if ((DateTime.Now - lastTime).TotalSeconds > 10)
                {
                    Console.Clear();
                    Console.WriteLine("Runs: " + i);
                    foreach (var userFail in userFails)
                    {
                        var line = $"User: {userFail.Key} fails: {userFail.Value.Item1}";
                        if (userFail.Value.Item2 > 0)
                            line += $" exceptions: {userFail.Value.Item2}";
                        Console.WriteLine(line);
                    }

                    lastTime = DateTime.Now;
                }
            }
        }

        public class Res
        {
            public string CmdLine { get;  set; }
            public string[] Args { get;  set; }
            public List<SubRes> SubRes { get; set; } = new();
        }

        public class SubRes
        {
            public string Author { get;  set; }
            public bool Success { get;  set; }
            public bool Exception { get; set; }
            public IEnumerable<string> BadArgs { get;  set; }
        }

        private static string GetRandomArgs()
        {
            var r = Random.Shared.Next(0, 127);
            StringBuilder sb = new();
            while (r-- > 0)
            {
                var c = Random.Shared.Next(32, 127);
                sb.Append((char)c);
            }
            return sb.ToString();
        }
    }
}
