namespace FileCabinetApp.Service;

/// <summary>
/// Criteria used in Find meethod in FileCabinetSetvise.
/// </summary>
public enum EFindCriteria
{
    /// <summary>
    /// Searching by first name.
    /// </summary>
    FirstName,

    /// <summary>
    /// Searching by last name.
    /// </summary>
    LastName,

    /// <summary>
    /// Searching by age.
    /// </summary>
    Sex,

    /// <summary>
    /// Searching by date of birth.
    /// </summary>
    DateOfBirth,

    /// <summary>
    /// Searching by post index.
    /// </summary>
    PostIndex,

    /// <summary>
    /// Searching by id.
    /// </summary>
    Id,

    /// <summary>
    /// Searching by country.
    /// </summary>
    Country,
}