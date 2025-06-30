using System.Text;
using Application.Commons.Constants;
using Application.Commons.Interfaces;
using Application.Commons.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Application.Features.Auth.SignUp.Commands;

public class SignUpNotification : INotification
{
    public User User { get; set; }
}

public class SignUpNotificationHandler(ICacheService cacheService, UserManager<User> userManager, INotificationService notificationService) : INotificationHandler<SignUpNotification>
{
    public async Task Handle(SignUpNotification notification, CancellationToken cancellationToken)
    {
        var token = await userManager.GenerateEmailConfirmationTokenAsync(notification.User);
        var tokenEncodedBytes = Encoding.UTF8.GetBytes(token);
        var tokenEncoded = WebEncoders.Base64UrlEncode(tokenEncodedBytes);
            
        var link = $"{ConfigurationConstant.GetIntranetDomain}/account/confirm/{notification.User.Id}?token={tokenEncoded}";
            
        Console.WriteLine(link);

        var htmlContent = cacheService.Template(TemplateConstant.TemplateActivation);
        htmlContent = htmlContent.Replace("@{0}", notification.User.Name);
        htmlContent = htmlContent.Replace("@{1}", link);

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