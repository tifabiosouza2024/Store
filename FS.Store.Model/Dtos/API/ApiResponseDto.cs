using System.Net;
using System.Text.Json.Serialization;

namespace FS.Store.Model.Dtos.API
{
    public class ApiResponseDto<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; init; }

        [JsonPropertyName("http_status")]
        public HttpStatusCode HttpStatus { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = default!;

        [JsonPropertyName("data")]
        public T? Data { get; set; }

        public IEnumerable<string>? Errors { get; init; }

        public static ApiResponseDto<T> Ok(T data, string message = "Operação realizada com sucesso.")
        {
            return new()
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponseDto<T> Fail(string message, IEnumerable<string>? errors = null)
        {
            return new()
            {
                Success = false,
                Message = message,
                Errors = errors
            };
        }
    }
}
