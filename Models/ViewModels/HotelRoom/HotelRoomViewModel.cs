namespace SolviaHotelManagement.Models.ViewModels.HotelRoom;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ViewModels.Hotel;
using SolviaHotelManagement.Models.ViewModels.Room;
using System.Text.Json.Serialization;

public class HotelRoomViewModel
{
    public int Id { get; set; }
    public int HotelId { get; set; }  // Hotel'in ID'si
    public int RoomId { get; set; }   // Room'un ID'si

    [JsonIgnore] public  Hotel? Hotel { get; set; }  // Hotel ile ilişki (Navigation Property)
    [JsonIgnore] public  Room? Room { get; set; }   // Room ile ilişki (Navigation Property)

    public string Type { get; set; }
    public bool IsReserved { get; set; } = true;
    public DateTime CreatedDate { get; set; }
}

