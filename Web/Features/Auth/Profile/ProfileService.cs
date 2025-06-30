using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.JSInterop;
using Web.Shared.Bases;

namespace Web.Features.Auth.Profile;

public interface IProfileService
{
    Task<ProfileResponse> Invoke();
}

public class ProfileService(HttpClient http, IJSRuntime js) : IProfileService
{
    public async Task<ProfileResponse> Invoke()
    {
        var token = await js.InvokeAsync<string>("localStorage.getItem", "authToken");
        
        if (!string.IsNullOrEmpty(token))
        {
            http.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token);
        }
        
        var response = await http.GetAsync("/api/auth/account/me");
        
        if (response.StatusCode == HttpStatusCode.Unauthorized)
            return null;

        await using var stream = await response.Content.ReadAsStreamAsync();
        
        var result = await JsonSerializer.DeserializeAsync<ApiResponse<ProfileResponse>>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result?.Data;
    }
}