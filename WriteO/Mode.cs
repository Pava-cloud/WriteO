using System.Text;
using static System.Console;
using static WriteO.Program;
namespace WriteO;

public static class Mode
{
    public static void Select(int mode)
    {
        if (mode == 1) Message();
        else if (mode == 2) FileSystem();
    }
    private static void Command(string cmd)
    {
        switch (cmd.Split(" ")[0])
        {
            case ":clear": 
                File.Delete(Files.Log);
                File.Create(Files.Log).Dispose();
                break;
            case ":c":
                File.Delete(Files.Log);
                File.Create(Files.Log).Dispose();
                break;
            case ":cls":
                File.Delete(Files.Log);
                File.Create (Files.Log).Dispose();
                break;
            case ":cs":
                Files.Log = cmd.Split(" ")[1];
                break;
            case ":cenc":
                CmdCenc(cmd.Remove(0, 6));
                break;
        }
    }
    public static void Message()
    {
        string line = "";
        string log, input;
        while (true)
        {
            log = DataFetcher.Log();
            string[] splitLog = log.Split('\n');
            int lineCount = (splitLog.Length >= WindowHeight - 4) ? WindowHeight - 4 : splitLog.Length;

            ClearAll();
            #region Log Output
            WriteLine($"-- Messages --\n");
            for (int i = lineCount; i > 0; i--)
            {
                WriteLine(splitLog[splitLog.Length - i]);
            }
            for (int i = WindowHeight - (lineCount + 4); i > 0; i--)
            {
                WriteLine();
            }
            #endregion Log Output
            input = ReadLine();
            #region Commands
            if (input.StartsWith(':'))
            {
                Command(input);
                if (input == ":exit" || input == ":q") break;
            }
            #endregion Commands
            else
            {
                line = $"{User.Name}: {input}\n";
                StreamWriter lineWriter = new StreamWriter(Files.Log);
                lineWriter.WriteLine(String.EncodeText(log + line, User.Key));
                lineWriter.Close();
            }
        }
    } // DONE
    public static void FileSystem()
    {
        int mode = 3;
        do
        {
            ClearAll();
            
            WriteLine("-- File Server --\n");
            WriteLine("1.....Upload\n2.....Download\n3.....Exit\n");
            if (int.TryParse(ReadLine(), out mode))
            {
                #region Upload
                if (mode == 1)
                {
                    WriteLine("Choose a file to upload or \":exit\" to exit.");
                    string path = ReadLine();
		    if (path == ":exit") break;
                    File.Move(path, Files.FS + path.Split('/')[path.Split('/').Length - 1]);
                }
                #endregion Upload
                #region Download
                if (mode == 2)
                {
                    string[] files = Directory.GetFiles(Files.FS);
                    foreach (string file in files) WriteLine(file);
                    WriteLine("Which file to download? (\":exit\" to exit)");
                    string dfile = ReadLine();
		    if (dfile == ":exit") break;
                    if (File.Exists(Files.FS + dfile))
                    {
                        WriteLine("Where to download to? (\":exit\" to exit)");
                        string path = ReadLine();
                        File.Move(Files.FS + dfile, path);
                        WriteLine("Success!");
                    }
                }
                #endregion Download
            }
        } while (mode != 3);
    } // DONE
    private static void CmdCenc(string input)
    {
        switch(input)
        {
            case "u8":
                OutputEncoding = Encoding.UTF8;
                break;
            case "ascii":
                OutputEncoding = Encoding.ASCII;
                break;
            case "u16":
                OutputEncoding = Encoding.Unicode;
                break;
            case "u32":
                OutputEncoding = Encoding.UTF32;
                break;
            default:
                ClearAll();
                for (int i = 0; i < Math.Ceiling(WindowHeight / 2.0 - 1); i++) WriteLine();
                WriteLine("Not a valid encoding");
                for (int i = 0; i < Math.Floor(WindowHeight / 2.0 - 1); i++) WriteLine();
                ReadKey();
                break;
        }
    }

}
