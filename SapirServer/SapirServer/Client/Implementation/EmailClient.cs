using System.Net;
using System.Net.Mail;
using SapirServer.Client.Interface;
using SapirServer.Contract.Request;
using SapirServer.Contract.Response;
using SapirServer.Helper;
using SapirServer.Model;

namespace SapirServer.Client.Implementation
{
    public class EmailClient: IEmailClient
    {

        private readonly ILogger<EmailClient> _logger;
        
        public EmailClient(ILogger<EmailClient> logger)
        {
            _logger = logger;
        }

        public async Task<GeneralResponse> SendEmail(SendEmailRequest request)
        {
            var res = new GeneralResponse();
            try
            {
                SendUserEmail(request);
                SendSapirEmail(request);
            }
            catch (Exception e)
            {
                _logger.LogError($"failed to send email. name: {request.Name} email: {request.Email}, pohne: {request.Phone} "  + e.Message);
                res.Success = false;
            }

            return res;
        }

        private void SendSapirEmail(SendEmailRequest request)
        {
            string fromAddress = "sapir.995@gmail.com";
            
            // Create the email message
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(fromAddress);
            mail.IsBodyHtml = true;
            mail.To.Add("sapir.995@gmail.com");
            mail.Subject = request.Name + " - " + request.Email + " - " + request.Phone;
            // Set up the SMTP client
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("sapir.995@gmail.com", SettingsDetails.EmailPassword);
            smtp.EnableSsl = true;

            // Send the email
            smtp.Send(mail);

        }

        private void SendUserEmail(SendEmailRequest request)
        {
            string fromAddress = "sapir.995@gmail.com";
            string subject = "חוברת פעילות לקראת החופש הגדול - כאן אצלך";
            string body = @"
<div dir='rtl' style='text-align:right; font-family:Arial; font-size:14px;'>
    היי :)<br><br>
    איזה כיף שאת כאן, ואיזה כיף שבחרת להוריד את החוברת שהכנתי.<br><br>
    אז עם סיום השנה, יש לך הזדמנות לסכם אותה ביחד עם הילדים דרך משחק חוויתי שהכנתי.<br>
    ומשם, להמשיך לתכנן ביחד איך החופש הגדול שלכם יראה.<br><br>
    יש לך שאלה? תכתבי לי!<br>
    וכמובן תחזרי לעדכן אותי איך היה ❤️<br><br>
    באהבה גדולה,<br>
    ספיר
</div>";
            
            // Create the email message
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(fromAddress);
            mail.IsBodyHtml = true;
            mail.To.Add(request.Email);
            mail.Subject = subject;
            mail.Body = body;
            string pdfFilePath = GeneralHelper.GetPath();
            _logger.LogDebug("file path: " + pdfFilePath);
            Attachment pdfAttachment = new Attachment(pdfFilePath);
            mail.Attachments.Add(pdfAttachment);

            // Set up the SMTP client
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("sapir.995@gmail.com", SettingsDetails.EmailPassword);
            smtp.EnableSsl = true;

            // Send the email
            smtp.Send(mail);
        }
    }
}
