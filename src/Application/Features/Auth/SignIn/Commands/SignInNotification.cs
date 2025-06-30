using Application.Commons.Constants;
using Application.Commons.Interfaces;
using Application.Commons.Models;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.SignIn.Commands;

public class SignInNotification : INotification
{
    public User User { get; set; }
}

public class SignInNotificationHandler(ICacheService cacheService, INotificationService notificationService) : INotificationHandler<SignInNotification>
{
    public async Task Handle(SignInNotification notification, CancellationToken cancellationToken)
    {
        var htmlContent = cacheService.Template(TemplateConstant.TemplateLocked);

        var email = new SendGridModel
        {
            Sender = new SenderModel
            {
                Name = GlobalConstant.DefaultName,
                Email = GlobalConstant.DefaultUsername
            },
            To =
            [
                new ToDto
                {
                    Name = notification.User.Name,
                    Email = notification.User.Email
                }
            ],
            Subject = TemplateConstant.SubjectActivateAccount,
            HtmlContent = htmlContent
        };
            
        await notificationService.SendEmail(email);
    }
}