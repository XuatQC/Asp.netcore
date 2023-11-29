using System.Text.Json;

namespace FNDSystem.API.Modules
{
    public class RequestHeaderDto
    {
        public string PlantName { get; set; } = string.Empty;

        public string UserInitial { get; set; } = string.Empty;

        public static bool TryParse(string input, out RequestHeaderDto result)
        {
            try
            {
                result = JsonSerializer.Deserialize<RequestHeaderDto>(input, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new();
                return true;
            }
            catch
            {
                result = new();
                return false;
            }
        }
    }
}
