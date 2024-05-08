namespace Bank.Domain.Aggregates.ClientAggregate;

public class Passport
{
    public Name Name { get; private set; }
    public string Series { get; private set; }
    public string Number { get; private set; }
    public RegistrationAddress RegistrationAddress { get; private set; }

    protected Passport()
    {
        
    }
    public Passport(Name name, string series, string number, RegistrationAddress registrationAddress)
    {
        if (!IsSeriesValid(series))
            throw new ArgumentException("series is not valid");
        if (!IsNumberValid(number))
            throw new ArgumentException("number is not valid");
        Name = name;
        Series = series;
        Number = number;
        RegistrationAddress = registrationAddress;
    }

    private bool IsSeriesValid(string series) => series.Length == 4 ? true : false;
    private bool IsNumberValid(string number) => number.Length == 6 ? true : false;
}