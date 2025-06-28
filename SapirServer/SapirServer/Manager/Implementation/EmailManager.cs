using SapirServer.Client.Interface;
using SapirServer.Contract.Request;
using SapirServer.Contract.Response;
using SapirServer.Manager.Interface;

namespace SapirServer.Manager.Implementation
{
    public class EmailManager : IEmailManager
    {
        private readonly ILogger<EmailManager> _logger;
        private readonly IEmailClient _emailClient;


        public EmailManager(ILogger<EmailManager> logger, IEmailClient emailClient)
        {
            _logger = logger;
            _emailClient = emailClient;
        }

        public Task<GeneralResponse> SendEmail(SendEmailRequest request)
        {
            return _emailClient.SendEmail(request);
        }
    }
}