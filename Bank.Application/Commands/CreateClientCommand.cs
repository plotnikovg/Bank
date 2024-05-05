namespace Bank.Application.Commands;

public class CreateClientCommand : IRequest<bool>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Patronymic { get; private set; } //отчество
    public string PassportSeries { get; private set; }
    public string PassportNumber { get; private set; }
    public DateOnly RegistrationDate { get; private set; }
    public string Region { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }
    public int HouseNumber { get; private set; } //Номер дома
    public string? BuildingNumber { get; private set; } //Корпус
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