using SapirServer.Contract.Request;
using SapirServer.Contract.Response;

namespace SapirServer.Manager.Interface
{
    public interface IEmailManager
    {
        Task<GeneralResponse> SendEmail(SendEmailRequest request);
    }
}
