namespace WriteO;

public static class SettingMode
{
    private static int selectedIndex = 0;

    private static string[] GetOptions() => new[]
    {
        Translations.Get("SettingName"),
        Translations.Get("SettingServer"),
        Translations.Get("SettingKey"),
        Translations.Get("SettingLang"),
        Translations.Get("SettingExit")
    };

    public static void Show()
    {
        ConsoleKey key;
        do
        {
            var options = GetOptions();
            DrawMenu(options);
            
            var keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex + 1) % options.Length;
                    break;

                case ConsoleKey.Enter:
                    if (selectedIndex != options.Length - 1) HandleSelection();
                    else return;
                    break;

                case ConsoleKey.Escape:
                    return;
            }

        } while (true);
    }

    private static void DrawMenu(string[] options)
    {
        Console.Clear();
        Console.CursorVisible = false;

        int windowWidth = Console.WindowWidth;
        int windowHeight = Console.WindowHeight;

        for (int i = 0; i < options.Length; i++)
        {
            string text = options[i];
            int x = (windowWidth - text.Length) / 2;
            int y = (windowHeight / 2 - options.Length / 2) + i;

            Console.SetCursorPosition(x, y);

            if (i == selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine(text);
                Console.ResetColor();
            }
            else Console.WriteLine(text);
        }
    }

    private static void HandleSelection()
    {
        Console.Clear();
        Console.CursorVisible = true;

        switch (selectedIndex)
        {
            case 0:
                Console.Write(Translations.Get("EnterNewName"));
                User.Name = Console.ReadLine() ?? User.Name;
                break;

            case 1:
                Console.Write(Translations.Get("EnterNewServer"));
                Files.Log = Console.ReadLine() ?? Files.Log;
                break;

            case 2:
                Console.Write(Translations.Get("EnterNewKey"));
                if (int.TryParse(Console.ReadLine(), out int key))
                    User.Key = key;
                break;

            case 3:
                Console.Write(Translations.Get("EnterNewLang"));
                string lang = Console.ReadLine()?.ToUpper() ?? "EN";
                if (DataFetcher.LangIsAllowed(lang))
                {
                    User.changeLang(lang);
                    using (StreamWriter langWriter = new StreamWriter("lang.txt"))
                    {
                        langWriter.WriteLine(lang);
                    }
                }
                break;
        }
            
        Console.WriteLine(Translations.Get("PressAnyKey"));
        Console.ReadKey(true);
    }
   
}
