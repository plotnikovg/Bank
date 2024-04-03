using System.Runtime.InteropServices.ComTypes;

namespace Bank.Domain.Aggregates.ClientAggregate;

public class RegistrationAddress
{
    public DateOnly RegistrationDate { get; private set; }
    public string Region { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }
    public int HouseNumber { get; private set; } //Номер дома
    public string? BuildingNumber { get; private set; } //Корпус

    public RegistrationAddress(DateOnly registrationDate, string region, string city, string street, int houseNumber)
    {
        RegistrationDate = registrationDate;
        Region = region;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
    }
    public RegistrationAddress(DateOnly registrationDate, string region, string city, string street, int houseNumber, string buildingNumber)
    {
        RegistrationDate = registrationDate;
        Region = region;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        BuildingNumber = buildingNumber;
    }

    public IEnumerable<object> GetFullRegistrationAddress()
    {
        yield return RegistrationDate;
        yield return Region;
        yield return City;
        yield return Street;
        yield return HouseNumber;
        if (BuildingNumber != null) yield return BuildingNumber;
    }
}