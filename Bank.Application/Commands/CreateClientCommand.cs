namespace Bank.Application.Commands;

public class CreateClientCommand : IRequest<bool>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Patronymic { get; init; } //отчество
    public string PassportSeries { get; init; }
    public string PassportNumber { get; init; }
    public DateOnly RegistrationDate { get; init; }
    public string Region { get; init; }
    public string City { get; init; }
    public string Street { get; init; }
    public int HouseNumber { get; init; } //Номер дома
    public string? BuildingNumber { get; init; } //Корпус
    public string PhoneNumber { get; init; }

    public CreateClientCommand(string firstName, string lastName, string patronymic, 
        string passportSeries, string passportNumber, DateOnly registrationDate,
        string region, string city, string street, int houseNumber, string? buildingNumber, 
        string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        PassportSeries = passportSeries;
        PassportNumber = passportNumber;
        RegistrationDate = registrationDate;
        Region = region;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        BuildingNumber = buildingNumber;
        PhoneNumber = phoneNumber;
    }
}