using Bank.Application.Repositories;

namespace Bank.Application.Commands;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, bool>
{
    private readonly IClientRepository _clientRepository;
    private readonly ILogger<CreateClientCommand> _logger;

    public CreateClientCommandHandler(IClientRepository clientRepository, ILogger<CreateClientCommand> logger)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<bool> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var name = new Name(request.FirstName, request.LastName, request.Patronymic);
        var registrationAddress = new RegistrationAddress
        (new DateOnly(request.RegistrationDateYear, request.RegistrationDateMonth, request.RegistrationDateDay),
            request.Region, request.City,
            request.Street, request.HouseNumber, request.BuildingNumber);
        var passport = new Passport(name, request.PassportSeries, request.PassportNumber, registrationAddress);
        var client = new Client(passport, request.PhoneNumber);
        client.UserId = request.UserId;

        _logger.LogInformation("Create client: {@client}", client);

        _clientRepository.Add(client);

        return await _clientRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}