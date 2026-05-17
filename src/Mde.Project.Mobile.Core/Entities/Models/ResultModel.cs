namespace Mde.Project.Mobile.Core.Entities.Models
{
    public class ResultModel<T> : BaseResult
    {
        // properties
        public T Data { get; set; }

        public static ResultModel<T> Success(T data, string? userMessage = null)
        {
            return new ResultModel<T>
            {
                Data = data,
                UserMessage = userMessage
            };
        }
        public static ResultModel<T> Failure(string developerError, string? userMessage = null, int? statusCode = null)
        {
            return new ResultModel<T>
            {
                Errors = new List<string> { developerError },
                UserMessage = userMessage,
                StatusCode = statusCode
            };
        }
    }
}
