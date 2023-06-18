namespace FileCabinetApp.Service;

using System.Linq;

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

    public void EditRecord(int id)
    {
        FileCabinetRecord record;
        try
        {
            record = this.list.First(item => item.Id == id);
        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException($"Record #{id} record is not found.");
        }

        Console.Write("First name: ");
        string? firstName = Console.ReadLine();
        if (firstName is null)
        {
            throw new ArgumentNullException(nameof(firstName), $"Parameter '{nameof(firstName)}' can not be null.");
        }

        if (firstName.Length < 2 || firstName.Length > 60)
        {
            throw new ArgumentException($"Length of paramter '{nameof(firstName)}' must be between 2 and 60 symbols.");
        }

        Console.Write("Last name: ");
        string? lastName = Console.ReadLine();
        if (lastName is null)
        {
            throw new ArgumentNullException(nameof(firstName), $"Parameter '{nameof(lastName)}' can not be null.");
        }

        if (lastName.Length < 2 || lastName.Length > 60)
        {
            throw new ArgumentException($"Length of paramter '{nameof(lastName)}' must be between 2 and 60 symbols.");
        }

        Console.Write("Date of birth: ");
        string? date = Console.ReadLine();

        DateTime dateOfBirth;
        if (!DateTime.TryParse(date, out dateOfBirth))
        {
            throw new ArgumentException(string.Format("Date of birth must be between {0} and {1}.\nCorrect date format is dd/mm/yyyy", new DateTime(1950, 1, 1).ToString("yyyy-MMM-dd"), DateTime.Now.ToString(DateTime.Now.ToString("yyyy-MMM-dd"))));
        }

        if (dateOfBirth < new DateTime(1950, 1, 1) || dateOfBirth > DateTime.Now)
        {
            throw new ArgumentException(string.Format("Parameter '{0}' must be between {1} and {2}.", nameof(dateOfBirth), new DateTime(1950, 1, 1).ToString("yyyy-MMM-dd"), DateTime.Now.ToString("yyyy-MMM-dd")));
        }

        Console.Write("Sex: ");
        string? sex_ = Console.ReadLine();
        if (sex_ is null)
        {
            throw new ArgumentNullException("Parameter dateOfBirth can not be null.");
        }

        if (sex_.Length != 1)
        {
            throw new ArgumentException("Sex must be char m or f.");
        }

        char sex = sex_[0];
        if (sex != 'm' && sex != 'f')
        {
            throw new ArgumentException($"Parameter '{sex}' must be f or m.");
        }

        Console.Write("Post index: ");
        string? post = Console.ReadLine();
        int postIndex;
        if (!int.TryParse(post, out postIndex))
        {
            throw new ArgumentException($"Parameter '{postIndex}' must contain only 6 numbers.");
        }

        if (postIndex < 100_000 || postIndex >= 1_000_000)
        {
            throw new ArgumentException($"Parameter '{postIndex}' must contain only 6 numbers.");
        }

        Console.Write("Country: ");
        string? country = Console.ReadLine();
        if (country is null)
        {
            throw new ArgumentNullException(nameof(country), $"Parameter '{nameof(country)} can not be null.'");
        }

        if (country.Length < 2 || country.Length > 60)
        {
            throw new ArgumentException($"Length of paramter '{nameof(country)}' must be between 2 and 60 symbols.");
        }

        record.FirstName = firstName;
        record.LastName = lastName;
        record.DateOfBirth = dateOfBirth;
        record.PostIndex = postIndex;
        record.Country = country;
        record.Sex = sex;

        Console.WriteLine($"Record #{id} is updated.");
    }
}