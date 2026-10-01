using System.Net.NetworkInformation;

namespace Mde.SourceOfTruth.Core.Services.Models
{
    public class ResultModel<T> :BaseResult
    {
        public T Data { get; set; }

        public void AddError(string message)
        {
            Errors.Add(message);
        }
        public static ResultModel<T> Success(T data)
        {
            return new ResultModel<T>
            {
                Data = data
            };
        }
        public static ResultModel<T> Failure(string message)
        {
            return new ResultModel<T>
            {
                Errors = new List<string>
                {
                    message
                }
            };
        }
    }
}
