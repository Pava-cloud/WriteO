using Spectre.Console;
namespace WriteO;

public class String
{
    internal static string EncodeText(string s, int key)
    {
        string retval = "";
        for (int i = 0; i < s.Length; i++)
        {
            retval += (char)(s[i] + key + i);//random.Next(60));
        }
        return retval;
    }
    internal static string DecodeText(string s, int key)
    {
        string retval = "";
        for (int i = 0; i < s.Length; i++)
        {
            retval += (char)(s[i] - key - i);
        }
        return retval;
    }
    internal static void WriteWarning(string s)
    {
        Console.CursorVisible = false;
        Program.ClearAll();
        Console.Clear();
        WriteCenteredText(s, Console.WindowHeight / 2);
        Console.ReadKey(true);
        Console.CursorVisible = true;
    }
    internal static void WriteMarkupWarning(string s, string markup)
    {
        Console.CursorVisible = false;
        Program.ClearAll();
        Console.Clear();
        WriteCenteredMarkupText(s, markup, Console.WindowHeight / 2);
        Console.ReadKey(true);
        Console.CursorVisible = true;
    }
    internal static void WriteCenteredText(string s, int line)
    {
        int x = (Console.WindowWidth - s.Length) / 2;
        Console.SetCursorPosition(x, line);
        Console.WriteLine(s);
    }
    internal static void WriteCenteredMarkupText(string s, string markup, int line)
    {
        int x = (Console.WindowWidth - s.Length) / 2;
        Console.SetCursorPosition(x, line);
        if (!string.IsNullOrWhiteSpace(markup))
            AnsiConsole.MarkupLine(markup + s + "[/]");
        else AnsiConsole.MarkupLine(s);
    }
}
