using OnChurch.Common.Requests;
using OnChurch.Common.Responses;
using System.Threading.Tasks;

namespace OnChurch.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> GetMeetingAsync(string urlBase, string servicePrefix, string controller, string token);

        Task<Response> GetMembersAsync(string urlBase, string servicePrefix, string controller, TokenResponse token);
        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest);

        Task<Response> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest);
        Task<Response> ModifyUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest, string token);

        Task<Response> ChangePasswordAsync(string urlBase, string servicePrefix, string controller, ChangePasswordRequest changePasswordRequest, string token);

        Task<Response> CreateMeetAsync(string urlBase, string servicePrefix, string controller, MeetingRequest meetingRequest, string token);
    }

}
