namespace Logic.Models
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }
        public string Description { get; set; }
        public string PhoneDescription { get; set; }
        public string Token { get; set; }
    }
}
