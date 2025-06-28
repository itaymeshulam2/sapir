
using SapirServer.Contract.Request;
using SapirServer.Contract.Response;

namespace SapirServer.Client.Interface
{
    public interface IEmailClient
    {
        Task<GeneralResponse> SendEmail(SendEmailRequest request);
    }
}
