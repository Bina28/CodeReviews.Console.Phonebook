﻿using PhoneBook.Bina28;
using Spectre.Console;
using System.ComponentModel;

var isAppRunning = true;

while (isAppRunning)
{
	Console.Clear();

	var enumToDescription = Enum.GetValues(typeof(MenuOptions))
		.Cast<MenuOptions>()
		.ToDictionary(option => GetDescription(option), option => option);

	while (isAppRunning)
	{
		Console.Clear();

		// Generate a list of descriptions based on enum
		var menuChoices = enumToDescription.Keys.ToList();

		// Now use the description list in the SelectionPrompt
		var options = AnsiConsole.Prompt(
			new SelectionPrompt<string>()
				.Title("Select an option")
				.AddChoices(menuChoices)
		);

		// Get the corresponding enum value from the dictionary
		if (!enumToDescription.TryGetValue(options, out var selectedOption))
		{
			Console.WriteLine("Invalid selection.");
			continue;
		}

		switch (selectedOption)
		{
			case MenuOptions.AddData:
				PhoneBookService.AddInput();
				break;
			case MenuOptions.RemoveData:
				PhoneBookService.DeleteInput();
				break;
			case MenuOptions.ShowAllData:
				PhoneBookService.GetContacts();
				break;
			case MenuOptions.ShowData:
				PhoneBookService.GetContactInput();
				break;
			case MenuOptions.UpdateData:
				PhoneBookService.UpdateInput();
				break;
			case MenuOptions.Exit:
				AnsiConsole.MarkupLine("[green]Exiting the application. Goodbye![/]");
				isAppRunning = false;
				break;
		}
	}
	// The method for getting the description of an enum
	static string GetDescription(Enum value)
	{
		Type type = value.GetType();
		string name = Enum.GetName(type, value);
		if (name != null)
		{
			var field = type.GetField(name);
			if (field != null)
			{
				var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
				if (attr != null)
				{
					return attr.Description;
				}
			}
		}
		return value.ToString(); // Return the enum name if no description is found
	}

}

enum MenuOptions
{
	[Description("Add")]
	AddData,

	[Description("Remove")]
	RemoveData,

	[Description("Show all contacts")]
	ShowAllData,

	[Description("Show contact by Id")]
	ShowData,

	[Description("Update")]
	UpdateData,

	[Description("Exit")]
	Exit
}






