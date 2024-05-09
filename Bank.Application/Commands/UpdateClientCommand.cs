namespace Bank.Application.Commands;

public class UpdateClientCommand : IRequest<bool>
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Patronymic { get; init; } //отчество
    public string PassportSeries { get; init; }
    public string PassportNumber { get; init; }
    public int RegistrationDateYear { get; init; }
    public int RegistrationDateMonth { get; init; }
    public int RegistrationDateDay { get; init; }
    public string Region { get; init; }
    public string City { get; init; }
    public string Street { get; init; }
    public int HouseNumber { get; init; } //Номер дома
    public string? BuildingNumber { get; init; } //Корпус
    public string PhoneNumber { get; init; }

    public UpdateClientCommand(Guid id, string firstName, string lastName, string patronymic, 
        string passportSeries, string passportNumber, int registrationDateYear, int registrationDateMonth, int registrationDateDay,
        string region, string city, string street, int houseNumber, string? buildingNumber, 
        string phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        PassportSeries = passportSeries;
        PassportNumber = passportNumber;
        RegistrationDateYear = registrationDateYear;
        RegistrationDateMonth = registrationDateMonth;
        RegistrationDateDay = registrationDateDay;
        Region = region;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        BuildingNumber = buildingNumber;
        PhoneNumber = phoneNumber;
    }
}