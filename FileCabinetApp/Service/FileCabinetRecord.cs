namespace FileCabinetApp.Service;

using System.Globalization;

public class FileCabinetRecord
{
    public static CultureInfo DateFormat { get; } = new CultureInfo("en-US");

    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public override string ToString()
    {
        return string.Format("#{0}, {1}, {2}, {3}", this.Id, this.FirstName, this.LastName, this.DateOfBirth.ToString("yyyy-MMM-dd", FileCabinetRecord.DateFormat));
    }
}