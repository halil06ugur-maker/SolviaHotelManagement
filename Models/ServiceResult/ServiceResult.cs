namespace SolviaHotelManagement.Models.ServiceResult
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }

        public ServiceResult(object data)
        {
            Success = true;
            Data = data;
        }

        public ServiceResult(string messege)
        {
            Success = false;
            Message = messege;
        }

        public ServiceResult(object data, string messege)
        {
            Success = true;
            Data = data;
            Message = messege;
        }
    }
}
