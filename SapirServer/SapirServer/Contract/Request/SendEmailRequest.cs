namespace SapirServer.Contract.Request;

public class SendEmailRequest
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
}