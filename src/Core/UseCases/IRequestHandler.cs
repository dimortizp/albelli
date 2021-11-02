using System.Threading.Tasks;

namespace Core.UseCases
{
    public interface IRequestHandler<in TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest data);
    }

    public interface IRequestHandler<TResponse>
    {
        Task<TResponse> HandleAsync();
    }
}