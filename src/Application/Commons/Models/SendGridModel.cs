namespace Application.Commons.Models;

public class SendGridModel
{
    public SenderModel Sender { get; set; }
        
    public List<ToDto> To { get; set; }
    
    public List<ToDto> Cc { get; set; }
    
    public List<ToDto> Bcc { get; set; }

    public string Subject { get; set; }

    public string HtmlContent { get; set; }
}

public class SenderModel
{
    public string Name { get; set; }

    public string Email { get; set; }
}

public class ToDto
{
    public string Email { get; set; }

    public string Name { get; set; }
}