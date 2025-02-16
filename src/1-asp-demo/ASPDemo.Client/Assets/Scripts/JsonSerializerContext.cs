using System.Text.Json.Serialization;

namespace ASPDemo.Client
{
    [JsonSourceGenerationOptions(
        GenerationMode = JsonSourceGenerationMode.Default,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false)]
    [JsonSerializable(typeof(Shared.Models.DemoItem))]
    public partial class JsonSerializerContext : System.Text.Json.Serialization.JsonSerializerContext
    {
    }
}