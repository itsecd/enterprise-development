using Hotel.Domain.Entities;
using Hotel.Infrastructure;

public class ClientRepository(HotelDbContext context) :
    EfRepository<Client, int>(context), IClientRepository
{ }