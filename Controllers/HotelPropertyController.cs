using Microsoft.AspNetCore.Mvc;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelProperty;
using SolviaHotelManagement.Models.VİewModels.HotelProperty;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class HotelPropertyController : ControllerBase
{
    private readonly IHotelPropertyService _hotelPropertyService;

    public HotelPropertyController(IHotelPropertyService hotelPropertyService)
    {
        _hotelPropertyService = hotelPropertyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var result = await _hotelPropertyService.GetListAsync();
        return Ok(result);
    }

    [HttpGet("byhotel/{hotelId}")]
    public async Task<IActionResult> GetListByHotelId(int hotelId)
    {
        var result = await _hotelPropertyService.GetListByHotelIdAsync(hotelId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] HotelPropertyViewModel viewModel)
    {
        var result = await _hotelPropertyService.AddHotelPropertyAsync(viewModel);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] HotelPropertyViewModel viewModel)
    {
        var result = await _hotelPropertyService.UpdateHotelPropertyAsync(viewModel);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _hotelPropertyService.DeleteHotelPropertyAsync(id);
        return Ok(result);
    }
}