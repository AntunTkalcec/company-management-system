using Newtonsoft.Json;

namespace CompanyManagementSystem.Infrastructure.Helpers;

public class ApiResponseHelper(int status, string? message = null)
{
    public int Status { get; set; } = status;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? Message { get; } = message ?? GetDefaultMessageForStatusCode(status);

    private static string GetDefaultMessageForStatusCode(int status)
    {
        return status switch
        {
            401 => "That action is unauthorized.",
            404 => "Resource could not be found.",
            500 => "A server error has occurred.",
            _ => null,
        };
    }
}
