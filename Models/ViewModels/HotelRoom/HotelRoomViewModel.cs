namespace SolviaHotelManagement.Models.ViewModels.HotelRoom;
using SolviaHotelManagement.Models.Entities;

    public class HotelRoomViewModel
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; }
    public string Type { get; set; }
    public bool IsReserved { get; set; }
    public DateTime CreatedDate { get; set; }

}

