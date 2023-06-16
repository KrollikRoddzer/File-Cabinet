namespace FileCabinetApp.Service;

using System.Globalization;

public class FileCabinetRecord
{
    public static CultureInfo DateFormat { get; } = new CultureInfo("en-US");

    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public char Sex { get; set; }

    public int PostIndex { get; set; }

    public string Country { get; set; } = string.Empty;

    public override string ToString()
    {
        return string.Format("#{0}, {1}, {2}, {3}, sex: {4}, post index number: {5}, {6}", this.Id, this.FirstName, this.LastName, this.DateOfBirth.ToString("yyyy-MMM-dd", FileCabinetRecord.DateFormat), this.Sex, this.PostIndex, this.Country);
    }
}