using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Application.Commons.Constants;
using Application.Commons.Interfaces;
using Application.Commons.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class NotificationService(
    ILogger<NotificationService> logger,
    IDateTimeService dateTimeService,
    IHttpClientFactory clientFactory) : INotificationService
{
    public async Task<string> SendEmail(SendGridModel entity)
    {
        if (ConfigurationConstant.IsLocal)
        {
            var emails = ConfigurationConstant.GetBcc.Split(",").Select(bcc => new ToDto { Email = bcc }).ToList();
            entity.Bcc = emails;
        }
        
        return await Send(entity);
    }
    

    private async Task<string> Send(SendGridModel entity)
    {
        logger.LogInformation("[SendInBlueClient - {@Datetime}] : Sending email ... to {@Subject}", dateTimeService.Now, entity.Subject);
            
        var client = clientFactory.CreateClient("SendInBlue");
            
        var response = await client.PostAsync("/v3/smtp/email", 
            new StringContent(JsonSerializer.Serialize(entity, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }), Encoding.UTF8, MediaTypeNames.Application.Json));
            
        var result = await response.Content.ReadAsStringAsync();
            
        if (response.IsSuccessStatusCode)
            logger.LogInformation("[SendEmail - {@Datetime}] : {@Id}", dateTimeService.Now, result);
        else
            logger.LogError("[SendEmail - {@Datetime}] : error {@Id}", dateTimeService.Now, result);

        return result;
    }
}