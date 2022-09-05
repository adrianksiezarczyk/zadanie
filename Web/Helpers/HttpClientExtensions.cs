using System.Text.Json;

namespace Web.Helpers;

public static class HttpClientExtensions
{
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Error calling API {response.ReasonPhrase}");

        var dataAsString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }
}
