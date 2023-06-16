namespace FileCabinetApp.Service;

using System.Globalization;

public class FileCabinetRecord
{
    public static CultureInfo DateFormat { get; } = new CultureInfo("en-US");

    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }
}