using System;
using System.IO;
using System.Linq;

namespace WriteO;

class Program
{
    static void Main(string[] args)
    {
	Console.WriteLine("Test");
        User user = new User(UserFetcher.GetName(), UserFetcher.GetLang);
	Console.WriteLine(user);
    }
}

public static class UserFetcher
{
    public static string GetName()
    {
	string? name = "";
	do
	{
            try
	    {
	        using(StreamReader nameGetter = new StreamReader("name.txt"))
	        {
	            name = nameGetter.ReadLine();
	        }
	    }
	    catch(Exception)
	    {
	        Console.WriteLine("Welcome to WriteO - The successor to WriteC");
	        Console.WriteLine("Please enter your name:");
	        StreamWriter nameWriter = new StreamWriter("name.txt");
	        do
	        {
	            name = Console.ReadLine();
	        } while(string.IsNullOrEmpty(name));
	        nameWriter.WriteLine(name);
	        nameWriter.Close();
	    }
	} while(string.IsNullOrEmpty(name));
	return name;
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
	Lang = lang
    }
    public override string ToString()
    {
        return Name;
    }
    public changeLang(string newLang)
    {
        Lang = newLang
    }
}
