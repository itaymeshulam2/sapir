using Microsoft.AspNetCore.Mvc;
using SapirServer.Contract.Request;
using SapirServer.Contract.Response;
using SapirServer.Manager.Interface;

namespace SapirServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IEmailManager _emailManager;


        public EmailController(ILogger<EmailController> logger, IEmailManager emailManager)
        {
            _logger = logger;
            _emailManager = emailManager;
        }
        
        [HttpPost("send-email")]
        public Task<GeneralResponse> SendEmail(SendEmailRequest request)
        {
            return _emailManager.SendEmail(request);
        }
    }
}