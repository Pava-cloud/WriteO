using Spectre.Console;
namespace WriteO;

public class StringExtras
{
    internal static string EncodeText(object s, int key)
    {
        string str = s.ToString()!;
        string retval = "";
        for (int i = 0; i < str.Length; i++)
        {
            retval += (char)(str[i] + key + i);
        }
        return retval;
    }
    internal static string DecodeText(object s, int key)
    {
        string str = s.ToString()!;
        string retval = "";
        for (int i = 0; i < str.Length; i++)
        {
            retval += (char)(str[i] - key - i);
        }
        return retval;
    }
    internal static void WriteWarning(object s)
    {
        Console.CursorVisible = false;
        Program.ClearAll();
        Console.Clear();
        WriteCenteredMarkupText(s, "[red]", Console.WindowHeight / 2);
        Console.ReadKey(true);
        Console.CursorVisible = true;
    }
    internal static void WriteMarkupWarning(object s, string markup)
    {
        Console.CursorVisible = false;
        Program.ClearAll();
        Console.Clear();
        WriteCenteredMarkupText(s, markup, Console.WindowHeight / 2);
        Console.ReadKey(true);
        Console.CursorVisible = true;
    }
    internal static void WriteCenteredText(object s, int line)
    {
        string str = s.ToString()!;
        int x = (Console.WindowWidth - str.Length) / 2;
        Console.SetCursorPosition(x, line);
        Console.WriteLine(str);
    }
    internal static void WriteCenteredMarkupText(object s, string markup, int line)
    {
        string str = s.ToString()!;
        int x = (Console.WindowWidth - str.Length) / 2;
        Console.SetCursorPosition(x, line);
        if (!string.IsNullOrWhiteSpace(markup))
            AnsiConsole.MarkupLine(markup + str + "[/]");
        else AnsiConsole.MarkupLine(str);
    }
}
