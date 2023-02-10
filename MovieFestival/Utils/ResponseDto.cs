namespace MovieFestival.Utils
{
    public class ResponseDto
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = "success";
        public object Data { get; set; }


        public ResponseDto(object data)
        {
            Data = data;
        }

        public ResponseDto(object data, string message, int statusCode = 200)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
}
