namespace FileCabinetApp.Service;

public class FileCabinetService
{
    private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

    public int CreateRecord(string? firstName, string? lastName, DateTime dateOfBirth, char sex, int postIndex, string? country)
    {
        if (firstName is null)
        {
            throw new ArgumentNullException(nameof(firstName), $"Parameter '{nameof(firstName)}' can not be null.");
        }

        if (firstName.Length < 2 || firstName.Length > 60)
        {
            throw new ArgumentException($"Length of paramter '{nameof(firstName)}' must be between 2 and 60 symbols.");
        }

        if (lastName is null)
        {
            throw new ArgumentNullException(nameof(firstName), $"Parameter '{nameof(lastName)}' can not be null.");
        }

        if (lastName.Length < 2 || lastName.Length > 60)
        {
            throw new ArgumentException($"Length of paramter '{nameof(lastName)}' must be between 2 and 60 symbols.");
        }

        if (dateOfBirth < new DateTime(1950, 1, 1) || dateOfBirth > DateTime.Now)
        {
            throw new ArgumentException(string.Format("Parameter '{0}' must be between {1} and {2}.", nameof(dateOfBirth), new DateTime(1950, 1, 1).ToString("yyyy-mmm-dd"), DateTime.Now.ToString("yyyy-mmm-dd")));
        }

        if (sex != 'm' && sex != 'f')
        {
            throw new ArgumentException($"Parameter '{sex}' must be f or m.");
        }

        if (postIndex < 100_000 || postIndex >= 1_000_000)
        {
            throw new ArgumentException($"Parameter '{postIndex}' must contain only 6 numbers.");
        }

        if (country is null)
        {
            throw new ArgumentNullException(nameof(country), $"Parameter '{nameof(country)} can not be null.'");
        }

        if (country.Length < 2 || country.Length > 60)
        {
            throw new ArgumentException($"Length of paramter '{nameof(country)}' must be between 2 and 60 symbols.");
        }

        var record = new FileCabinetRecord
        {
            Id = this.list.Count + 1,
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
            Sex = sex,
            PostIndex = postIndex,
            Country = country,
        };

        this.list.Add(record);

        return record.Id;
    }

    public FileCabinetRecord[] GetRecords()
    {
        return this.list.ToArray();
    }

    public int GetStat()
    {
        return this.list.Count;
    }
}