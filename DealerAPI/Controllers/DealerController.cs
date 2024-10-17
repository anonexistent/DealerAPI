using DealerAPI.Contracts.Input;
using DealerAPI.Contracts.Output;
using DealerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DealerAPI.Controllers;

[ApiController]
[Route("/dealer/dealer/")]
public class DealerController : ControllerBase
{
    private readonly DealerService _dealerService;

    public DealerController(DealerService dealerService)
    {
        _dealerService = dealerService;
    }

    [HttpGet("getList")]
    public async Task<IActionResult> GetList()
    {
        var dealers = await _dealerService.GetList();
        if (!dealers.Ok || dealers.Answer is null) return BadRequest(dealers.Errors);

        return Ok(new OutputDealerList(dealers.Answer));
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get([FromQuery] DealerIdQuery q)
    {
        var dealers = await _dealerService.Get(q.Id);
        if (!dealers.Ok || dealers.Answer is null) return BadRequest(dealers.Errors);

        return Ok(new OutputDealer(dealers.Answer));
    }    

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateDealerBody b)
    {
        var dealers = await _dealerService.Create(b.Name, b.Description, b.TypeId);
        if (!dealers.Ok || dealers.Answer is null) return BadRequest(dealers.Errors);

        return Ok(new OutputDealer(dealers.Answer));
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> Remove([FromQuery] DealerIdQuery q)
    {
        var dealers = await _dealerService.Remove(q.Id);
        if (!dealers.Ok || dealers.Answer is null) return BadRequest(dealers.Errors);

        return Ok(204);
    }

}
