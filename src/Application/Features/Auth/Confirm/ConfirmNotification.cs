using Application.Commons.Constants;
using Application.Commons.Interfaces;
using Application.Commons.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Auth.Confirm
{
    public class ConfirmNotification : INotification
    {
        public User User { get; set; }
    }
    
    public class ConfirmNotificationHandler : INotificationHandler<ConfirmNotification>
    {
        private readonly ICacheService _cacheService;
        private readonly INotificationService _notificationService;

        public ConfirmNotificationHandler(ICacheService cacheService, INotificationService notificationService)
        {
            _cacheService = cacheService;
            _notificationService = notificationService;
        }

        public async Task Handle(ConfirmNotification notification, CancellationToken cancellationToken)
        {
            var link = $"{ConfigurationConstant.GetIntranetDomain}";
            
            var htmlContent = _cacheService.Template(TemplateConstant.TemplateWelcome);
            htmlContent = htmlContent.Replace("@{0}", notification.User.Name);
            htmlContent = htmlContent.Replace("@{1}", link);
            
            Console.WriteLine(link);

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
            
            await _notificationService.SendEmail(email);
        }
    }
}