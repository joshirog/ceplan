using Application.Commons.Models;

namespace Application.Commons.Interfaces;

public interface INotificationService
{
    Task<string> SendEmail(SendGridModel entity);
}