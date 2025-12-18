using System;
using System.IO;
using System.Linq;

namespace WriteO;

class Program
{
    static void Main(string[] args)
    {
	Console.WriteLine("Test");
        User user = new User(UserFetcher.GetName(), UserFetcher.GetLang());
	Console.WriteLine(user);
	int mode = 0;
	while(true)
	{
	    Console.Clear();
	    Console.WriteLine(mode);
	    Console.WriteLine("What action do you want to perform?\n1.....Messages\n2.....FileSystem\n3.....WIP");
	    if(int.TryParse(Console.ReadLine().Remove(1), out mode))
	    {
		if(mode == 1) {}
	    }
	}
    }
}

public class Mode
{
}

public static class UserFetcher
{
    public static int key = 10;
    public static string GetName()
    {
	string? name = "";
	do
	{
            try
	    {
	        using(StreamReader nameGetter = new StreamReader("name.txt"))
	        {
	            name = String.DecodeText(nameGetter.ReadLine(), key);
	        }
	    }
	    catch(Exception)
	    {
	        StreamWriter nameWriter = new StreamWriter("name.txt");
	        do
	        {
		    Console.Clear();
		    Console.WriteLine("Welcome to WriteO - The successor to WriteC");
	            Console.WriteLine("Please enter your name:");
	            name = Console.ReadLine();
	        } while(string.IsNullOrEmpty(name));
	        nameWriter.WriteLine(String.EncodeText(name, key));
	        nameWriter.Close();
	    }
	} while(string.IsNullOrEmpty(name));
	return name;
    }
    public static string GetLang()
    {
        string lang = "NotEmpty";
	do
	{
	    try
	    {
	        using(System.IO.StreamReader langGetter = new StreamReader("lang.txt"))
		{
			lang = langGetter.ReadLine();
		}
	    }
	    catch(Exception)
	    {
		lang = "DE";
		StreamWriter langWriter = new StreamWriter("lang.txt");
		do
		{
		    do
		    {
		        Console.Clear();
			if(string.IsNullOrEmpty(lang) || !LangList(lang))
			    Console.WriteLine("++ ERROR - Please enter a valid Language ++");
                        Console.WriteLine("Please enter your language:");
		        lang = Console.ReadLine().ToUpper();
		    } while(string.IsNullOrEmpty(lang));
		} while(!LangList(lang)); 
		langWriter.WriteLine(lang);
		langWriter.Close();
	    }
        } while(string.IsNullOrEmpty(lang));
        return (string)lang;
    }
    public static bool LangList(string input)
    {
        string[] allowedLangs = {
	    "DE", "EN"
	};
	bool retval = false;
        foreach(string allowedLang in allowedLangs)
	{
	    if(input==allowedLang) retval = true;
	}
	return retval;

    }
}

class User
{
    private string name;

    public string Name
    {
	get; init;
    }
    private string lang;

    public string Lang
    {
        get; private set;
    }
    public User(string name, string lang)
    {
        Name = name;
	Lang = lang;
    }
    public override string ToString()
    {
        return Name;
    }
    public void changeLang(string newLang)
    {
        Lang = newLang;
    }
}

public class String
{
    internal static string EncodeText(string input, int key)
    {
        string retval = "";
	foreach(char c in retval)
	{
	    retval += (char)(c + key);
	}
	return retval;
    }
    internal static string DecodeText(string input, int key)
    {
	return EncodeText(input, 0 - key);
    }
}
