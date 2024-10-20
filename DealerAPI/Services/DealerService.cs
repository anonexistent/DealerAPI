using DealerAPI.Model;
using DealerAPI.Persistence;
using DealerLibrary;
using Microsoft.EntityFrameworkCore;

namespace DealerAPI.Services;

public class DealerService
{
    private readonly DealerDbContext _dealerDb;
    private readonly DealerTypeService _dealerTypeService;

    public DealerService(DealerDbContext dealerDb, DealerTypeService dealerTypeService)
    {
        _dealerDb = dealerDb;
        _dealerTypeService = dealerTypeService;
    }

    public async Task<ServiceAnswer<ICollection<Dealer>>> GetList()
    {
        var result = await _dealerDb.Dealers.Include(x=>x.DealerType).ToListAsync();

        return new ServiceAnswer<ICollection<Dealer>>()
        {
            Ok = true,
            Answer = result
        };
    }

    public async Task<ServiceAnswer<Dealer>> Get(string dealerId)
    {
        if(!Guid.TryParse(dealerId, out var dealerGuid))
        {
            return new ServiceAnswer<Dealer>
            {
                Ok = false,
                Errors = new[]
                {
                    new ServiceFieldError()
                    {
                        Fields = new[]{"dealerId"},
                        Message = "dealerId не соответсвует формату."
                    }
                }
            };
        }
        return await Get(dealerGuid);
    }

    public async Task<ServiceAnswer<Dealer>> Get(Guid dealerId)
    {
        var dealer = await _dealerDb.Dealers.Where(x=>x.Id == dealerId).FirstOrDefaultAsync();

        if(dealer is null)
        {
            return new ServiceAnswer<Dealer>()
            {
                Ok = false,
                Errors = new[]
                {
                    new ServiceFieldError()
                    {
                        Fields = new []{"id"},
                        Message = "dealer не найден."
                    }
                }
            };
        }    

        return new ServiceAnswer<Dealer>()
        {
            Ok = true,
            Answer = dealer
        };
    }

    public async Task<ServiceAnswer<Dealer>> Create(string name, string description, string dealerTypeId)
    {
        var errors = new List<object>();

        if(string.IsNullOrEmpty(name))
        {
            errors.Add(
                new ServiceFieldError()
                {
                    Fields = new[] { "name" },
                    Message = $"Длина поля {nameof(name)} не должно быть 0."
                }
            );
        }

        if(string.IsNullOrEmpty(description))
        {
            errors.Add(
                new ServiceFieldError()
                {
                    Fields = new[] { "description" },
                    Message = $"Длина поля {nameof(description)} не должно быть 0."
                }
            );
        }

        if (!Guid.TryParse(dealerTypeId, out var dealerTypeGuid))
        {
            errors.Add(
                new ServiceFieldError()
                {
                    Fields = new[]{ "id" },
                    Message = "dealerTypeId не соответсвует формату."
                }
            );                
        }

        if (errors.Count > 0)
        {
            return new ServiceAnswer<Dealer>()
            {
                Ok = false,
                Errors = errors
            };
        }

        return await Create(name, description, dealerTypeGuid);
    }

    public async Task<ServiceAnswer<Dealer>> Create(string name, string description, Guid typeId)
    {
        var type = await _dealerTypeService.Get(typeId);
        if (!type.Ok || type.Answer is null)
        {
            return new ServiceAnswer<Dealer>()
            {
                Ok = false,
                Errors = type.Errors
            };
        }

        var newDealer = new Dealer() { Id = Guid.NewGuid(), Name = name, Description = description, DealerType = type.Answer };

        _dealerDb.Add(newDealer);
        await _dealerDb.SaveChangesAsync();

        return new ServiceAnswer<Dealer>()
        {
            Ok = true,
            Answer = newDealer
        };
    }

    public async Task<ServiceAnswer<Dealer>> Remove(string id)
    {
        if (!Guid.TryParse(id, out var dealerGuid))
        {

            return new ServiceAnswer<Dealer>()
            {
                Ok = false,
                Errors = new[] 
                {
                    new ServiceFieldError()
                    {
                        Fields = new[]{ "delaerId" },
                        Message = "dealerId не соответсвует формату."
                    }
                }
            };
        }

        return await Remove(dealerGuid);
    }

    public async Task<ServiceAnswer<Dealer>> Remove(Guid id)
    {
        var dealerBayBay = await Get(id);

        if(!dealerBayBay.Ok || dealerBayBay.Answer is null)
        {
            return new ServiceAnswer<Dealer>()
            {
                Ok = false,
                Errors = new[]
                {
                    new ServiceFieldError()
                    {
                        Fields= new[]{ "dealerId" },
                        Message = "dealer не найден."
                    }
                }
            };
        }

        _dealerDb.Dealers.Remove(dealerBayBay.Answer);
        await _dealerDb.SaveChangesAsync();

        return new ServiceAnswer<Dealer>()
        {
            Ok = true,
            Answer = dealerBayBay.Answer
        };
    }

    public async Task<ServiceAnswer<Dealer>> Change(string dealerId, string name, string description, string dealerTypeId)
    {
        var errors = new List<object>();

        if (!Guid.TryParse(dealerId, out var dealerGuid))
        {
            errors.Add(
                new ServiceFieldError()
                {
                    Fields = new[] { "id" },
                    Message = "dealerId не соответсвует формату."
                }
            );
        }

        if (string.IsNullOrEmpty(name))
        {
            errors.Add(
                new ServiceFieldError()
                {
                    Fields = new[] { "name" },
                    Message = $"Длина поля {nameof(name)} не должно быть 0."
                }
            );
        }

        if (string.IsNullOrEmpty(description))
        {
            errors.Add(
                new ServiceFieldError()
                {
                    Fields = new[] { "description" },
                    Message = $"Длина поля {nameof(description)} не должно быть 0."
                }
            );
        }

        if (!Guid.TryParse(dealerTypeId, out var dealerTypeGuid))
        {
            errors.Add(
                new ServiceFieldError()
                {
                    Fields = new[] { "id" },
                    Message = "dealerTypeId не соответсвует формату."
                }
            );
        }

        if (errors.Count > 0)
        {
            return new ServiceAnswer<Dealer>()
            {
                Ok = false,
                Errors = errors
            };
        }

        return await Change(dealerGuid, name, description, dealerTypeGuid);
    }

    public async Task<ServiceAnswer<Dealer>> Change(Guid dealerId, string name, string description, Guid dealerTypeId)
    {
        var dealer = await Get(dealerId);
        var dealerType = await _dealerTypeService.Get(dealerTypeId);

        if(!dealer.Ok || dealer.Answer is null)
        {
            return new ServiceAnswer<Dealer>()
            {
                Ok = false,
                Errors = new[]
                {
                    new ServiceFieldError()
                    {
                        Fields = new []{"id"},
                        Message = "dealer не найден."
                    }
                }
            };
        }

        if(!dealerType.Ok || dealerType.Answer is null)
        {
            return new ServiceAnswer<Dealer>()
            {
                Ok = false,
                Errors = new[]
                {
                    new ServiceFieldError()
                    {
                        Fields = new []{"dealerTypeId"},
                        Message = "dealerType не найден."
                    }
                }
            };
        }

        dealer.Answer.Name = name;
        dealer.Answer.Description = description;

        dealer.Answer.DealerType = dealerType.Answer;

        _dealerDb.Dealers.Update(dealer.Answer);
        await _dealerDb.SaveChangesAsync();

        return new ServiceAnswer<Dealer>()
        {
            Ok = true,
            Answer = dealer.Answer
        };
    }
}
