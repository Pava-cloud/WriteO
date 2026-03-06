namespace WriteO;

public static class SettingMode
{
    private static int selectedIndex = 0;

    private static readonly string[] options =
    {
        "Name",
        "Server Location",
        "Key",
        "Exit"
    };

    public static void Show()
    {
        ConsoleKey key;
        do
        {
            DrawMenu();
            
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
                    if (selectedIndex != 3) HandleSelection();
                    else return;
                    break;

                case ConsoleKey.Escape:
                    return;
            }

        } while (true);
    }

    private static void DrawMenu()
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
                Console.Write("Enter new Name: ");
                User.Name = Console.ReadLine() ?? User.Name;
                break;

            case 1:
                Console.Write("Enter new Server Location: ");
                Files.Log = Console.ReadLine() ?? Files.Log;
                break;

            case 2:
                Console.Write("Enter new Key (int): ");
                if (int.TryParse(Console.ReadLine(), out int key))
                    User.Key = key;
                break;

            case 3:
                return;
        }
            
        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey(true);
    }   
}
