namespace Logic.Models
{
    public class BaseResponse<T>
    {
        public int StatusCode { get; set; }
        public string Description { get; set; }
        public string PhoneDescription { get; set; }
        public string Token { get; set; }
        public T Claims { get; set; }
    }
}
