using DealerAPI.Contracts.Input;
using DealerAPI.Contracts.Output;
using DealerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DealerAPI.Controllers;

[ApiController]
[Route("/dealer/dealerType/")]
public class DealerTypeController : ControllerBase
{
    private readonly DealerTypeService _dealerTypeService;

    public DealerTypeController(DealerTypeService dealerService)
    {
        _dealerTypeService = dealerService;
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get([FromQuery] DealerTypeIdQuery q)
    {
        var types = await _dealerTypeService.Get(q.DealerTypeId);
        if (!types.Ok || types.Answer is null) return BadRequest(types.Errors);

        return Ok(new OutputDealerType(types.Answer));
    }

    [HttpGet("getList")]
    public async Task<IActionResult> GetList()
    {
        var types = await _dealerTypeService.GetList();
        if (!types.Ok || types.Answer is null) return BadRequest(types.Errors);

        return Ok(new OutputDealerTypeList(types.Answer));
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateDealerTypeBody b)
    {
        var dealers = await _dealerTypeService.Create(b.Name);
        if (!dealers.Ok || dealers.Answer is null) return BadRequest(dealers.Errors);

        return Ok(new OutputDealerType(dealers.Answer));
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> Remove([FromQuery] DealerTypeIdQuery q)
    {
        var type = await _dealerTypeService.Remove(q.DealerTypeId);
        if(!type.Ok || type.Answer is null) return BadRequest(type.Errors);

        return Ok(204);
    }

}
