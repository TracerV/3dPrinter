using _3dPrinter.Domain.Enum;

namespace _3dPrinter.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }
        public StatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }
    public interface IBaseResponse<T>
    {
        T Data { get; }
        StatusCode StatusCode { get; }
        string Description { get; }
    }
}
