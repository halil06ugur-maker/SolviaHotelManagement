using Microsoft.AspNetCore.Mvc;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelRoomService;
using SolviaHotelManagement.Models.ViewModels.HotelRoom;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class HotelRoomController : ControllerBase
{
    private readonly IHotelRoomService _hotelRoomService;

    public HotelRoomController(IHotelRoomService hotelRoomService)
    {
        _hotelRoomService = hotelRoomService;
    }

    // Otel ID'sine göre odaları getir
    [HttpGet("byhotel/{hotelId}")]
    public async Task<IActionResult> GetListByHotelId(int hotelId)
    {
        var result = await _hotelRoomService.GetListByHotelIdAsync(hotelId);
        return Ok(result);
    }

    // Oda ID'sine göre tek bir odayı getir
    [HttpGet("{id}")]
    public async Task<IActionResult> GetHotelRoomById(int id)
    {
        var result = await _hotelRoomService.GetHotelRoomByIdAsync(id);
        return Ok(result);
    }

    // Yeni bir HotelRoom oluştur
    [HttpPost]
    public async Task<IActionResult> CreateHotelRoom([FromBody] HotelRoomViewModel viewModel)
    {
        var result = await _hotelRoomService.CreateHotelRoomAsync(viewModel);
        return Ok(result);
    }

    // HotelRoom'u güncelle
    [HttpPut]
    public async Task<IActionResult> UpdateHotelRoom([FromBody] HotelRoomViewModel viewModel)
    {
        var result = await _hotelRoomService.UpdateHotelRoomAsync(viewModel);
        return Ok(result);
    }

    // HotelRoom'u sil
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotelRoom(int id)
    {
        var result = await _hotelRoomService.DeleteHotelRoomAsync(id);
        return Ok(result);
    }
}
