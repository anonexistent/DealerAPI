using DealerAPI.Model;
using DealerAPI.Persistence;
using DealerLibrary;
using Microsoft.EntityFrameworkCore;

namespace DealerAPI.Services;

public class DealerTypeService
{
    private readonly DealerDbContext _dealerDb;

    public DealerTypeService(DealerDbContext dealerDb)
    {
        _dealerDb = dealerDb;
    }

    public async Task<ServiceAnswer<List<DealerType>>> GetList()
    {
        return new ServiceAnswer<List<DealerType>>()
        {
            Ok = true,
            Answer = _dealerDb.DealerTypes.ToList()
        };
    }

    public async Task<ServiceAnswer<DealerType>> Get(string dealerTypeId)
    {
        if (!Guid.TryParse(dealerTypeId, out var dealerTypeGuid))
        {
            return new ServiceAnswer<DealerType>
            {
                Ok = false,
                Errors = new[]
                {
                    new ServiceFieldError()
                    {
                        Fields = new[]{ "id" },
                        Message = "dealerTypeId не соответсвует формату."
                    }
                }
            };
        }

        return await Get(dealerTypeGuid);
    }

    public async Task<ServiceAnswer<DealerType>> Get(Guid dealerTypeId)
    {
        var result = await _dealerDb.DealerTypes.Where(x => x.Id == dealerTypeId).FirstOrDefaultAsync();

        return new ServiceAnswer<DealerType>()
        {
            Ok = true,
            Answer = result
        };
    }

    public async Task<ServiceAnswer<DealerType>> Create(string name)
    {
        if(string.IsNullOrEmpty(name))
        {
            return new ServiceAnswer<DealerType>()
            {
                Ok = false,
                Errors = new[]
                {
                    new ServiceFieldError()
                    {
                        Fields = new[] { "name" },
                        Message = $"Длина поля {nameof(name)} не должно быть 0."
                    }
                }
            };
        }

        return await Create(Guid.NewGuid(), name);
    }

    public async Task<ServiceAnswer<DealerType>> Create(Guid id, string name)
    {
        var newDealer = new DealerType() { Id = id, Name = name };
        _dealerDb.DealerTypes.Add(newDealer);
        await _dealerDb.SaveChangesAsync();

        return new ServiceAnswer<DealerType>()
        {
            Ok = true,
            Answer = newDealer
        };
    }

    public async Task<ServiceAnswer<DealerType>> Remove(string dealerTypeId)
    {
        if(!Guid.TryParse(dealerTypeId, out var dealerTypeGuid))
        {
            return new ServiceAnswer<DealerType>
            {
                Ok = false,
                Errors = new[]
                {
                    new ServiceFieldError()
                    {
                        Fields = new[]{ "id" },
                        Message = "dealerTypeId не соответсвует формату."
                    }
                }
            };
        }

        return await Remove(dealerTypeGuid);
    }

    public async Task<ServiceAnswer<DealerType>> Remove(Guid dealerTypeId)
    {
        var d = await Get(dealerTypeId);

        if (!d.Ok || d.Answer is null)
        {
            return new ServiceAnswer<DealerType>
            {
                Ok = false,
                Errors = new[]
                {
                    new ServiceFieldError()
                    {
                        Fields = new[]{ "id" },
                        Message = "dealerType не найден."
                    }
                }
            };
        }

        _dealerDb.Remove(d.Answer);
        await _dealerDb.SaveChangesAsync();

        return new ServiceAnswer<DealerType>()
        {
            Ok = true,
            Answer = d.Answer
        };
    }
}
