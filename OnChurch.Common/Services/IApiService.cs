using OnChurch.Common.Requests;
using OnChurch.Common.Responses;
using System.Threading.Tasks;

namespace OnChurch.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

    }

}
