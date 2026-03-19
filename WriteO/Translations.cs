using System.Collections.Generic;

namespace WriteO;

public static class Translations
{
    private static Dictionary<string, Dictionary<string, string>> translations = new Dictionary<string, Dictionary<string, string>>()
    {
        {
            "EN", new Dictionary<string, string>()
            {
                { "Welcome", "Welcome to WriteO - The successor to WriteC\nInitializing..." },
                { "BuildSuccess", "[green]✓ Build completed successfully[/]" },
                { "ActionPrompt", "Which Action do you want to perform?" },
                { "Messages", "Messages" },
                { "FileServer", "File Server" },
                { "Settings", "Settings" },
                { "Exit", "Exit" },
                { "NewUserPath", "It seems as if you were a new user. Please select a path for the server." },
                { "NamePrompt", "Please enter your name:" },
                { "MessagesHeader", "-- Messages --" },
                { "FileServerHeader", "-- File Server --" },
                { "Upload", "1.....Upload" },
                { "Download", "2.....Download" },
                { "ExitOption", "3.....Exit" },
                { "ChooseUpload", "Choose a file to upload or \":exit\" to exit." },
                { "WhichDownload", "Which file to download? (\":exit\" to exit)" },
                { "WhereDownload", "Where to download to? (\":exit\" to exit)" },
                { "Success", "Success!" },
                { "InvalidEncoding", "[bold red]Not a valid encoding[/]" },
                { "SettingName", "Name" },
                { "SettingServer", "Server Location" },
                { "SettingKey", "Key" },
                { "SettingLang", "Language" },
                { "SettingExit", "Exit" },
                { "EnterNewName", "Enter new Name: " },
                { "EnterNewServer", "Enter new Server Location: " },
                { "EnterNewKey", "Enter new Key (int): " },
                { "EnterNewLang", "Enter new Language (EN, DE): " },
                { "PressAnyKey", "\nPress any key to return..." }
            }
        },
        {
            "DE", new Dictionary<string, string>()
            {
                { "Welcome", "Willkommen bei WriteO - Dem Nachfolger von WriteC\nInitialisierung..." },
                { "BuildSuccess", "[green]✓ Build erfolgreich abgeschlossen[/]" },
                { "ActionPrompt", "Welche Aktion möchten Sie ausführen?" },
                { "Messages", "Nachrichten" },
                { "FileServer", "Dateiserver" },
                { "Settings", "Einstellungen" },
                { "Exit", "Beenden" },
                { "NewUserPath", "Es scheint, als wären Sie ein neuer Benutzer. Bitte wählen Sie einen Pfad für den Server." },
                { "NamePrompt", "Bitte geben Sie Ihren Namen ein:" },
                { "MessagesHeader", "-- Nachrichten --" },
                { "FileServerHeader", "-- Dateiserver --" },
                { "Upload", "1.....Hochladen" },
                { "Download", "2.....Herunterladen" },
                { "ExitOption", "3.....Beenden" },
                { "ChooseUpload", "Wählen Sie eine Datei zum Hochladen oder \":exit\" zum Beenden." },
                { "WhichDownload", "Welche Datei soll heruntergeladen werden? (\":exit\" zum Beenden)" },
                { "WhereDownload", "Wohin soll die Datei heruntergeladen werden? (\":exit\" zum Beenden)" },
                { "Success", "Erfolg!" },
                { "InvalidEncoding", "[bold red]Keine gültige Kodierung[/]" },
                { "SettingName", "Name" },
                { "SettingServer", "Serverstandort" },
                { "SettingKey", "Schlüssel" },
                { "SettingLang", "Sprache" },
                { "SettingExit", "Beenden" },
                { "EnterNewName", "Neuen Namen eingeben: " },
                { "EnterNewServer", "Neuen Serverstandort eingeben: " },
                { "EnterNewKey", "Neuen Schlüssel eingeben (int): " },
                { "EnterNewLang", "Neue Sprache eingeben (EN, DE): " },
                { "PressAnyKey", "\nBeliebige Taste drücken, um zum Hauptmenü zurückzukehren..." }
            }
        }
    };

    public static string Get(string key)
    {
        string lang = User.Lang ?? "EN";
        if (translations.ContainsKey(lang) && translations[lang].ContainsKey(key))
        {
            return translations[lang][key];
        }
        return translations["EN"].ContainsKey(key) ? translations["EN"][key] : key;
    }
}
