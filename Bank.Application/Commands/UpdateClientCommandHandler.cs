namespace Bank.Application.Commands;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, bool>
{
    private readonly IClientRepository _clientRepository;
    private readonly ILogger<UpdateClientCommand> _logger;

    public UpdateClientCommandHandler(IClientRepository clientRepository, ILogger<UpdateClientCommand> logger)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<bool> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var name = new Name(request.FirstName, request.LastName, request.Patronymic);
        var registrationAddress = new RegistrationAddress
        (new DateOnly(request.RegistrationDateYear, request.RegistrationDateMonth, request.RegistrationDateDay),
            request.Region, request.City,
            request.Street, request.HouseNumber, request.BuildingNumber);
        var passport = new Passport(name, request.PassportSeries, request.PassportNumber, registrationAddress);
        var client = new Client(passport, request.PhoneNumber);

        _logger.LogInformation("Update client: {@client}", client);

        _clientRepository.Update(client);

        return await _clientRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}