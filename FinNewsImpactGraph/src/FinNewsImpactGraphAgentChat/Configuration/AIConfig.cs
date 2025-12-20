using Azure;

namespace Neo4jMarketNewsChat.Configuration;

public static class AIConfig
{
    private static readonly Lazy<(Uri Endpoint, AzureKeyCredential KeyCredential)> s_values =
        new(() =>
        {
            var endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
            var key = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");

            if (string.IsNullOrWhiteSpace(endpoint) || string.IsNullOrWhiteSpace(key))
            {
                throw new InvalidOperationException(
                    "Environment variables 'AZURE_OPENAI_ENDPOINT' and 'AZURE_OPENAI_API_KEY' must be set.");
            }

            return (new Uri(endpoint), new AzureKeyCredential(key));
        }, isThreadSafe: true);

    public static Uri Endpoint => s_values.Value.Endpoint;
    public static AzureKeyCredential KeyCredential => s_values.Value.KeyCredential;

    // Keep this as a deployment name, not a model name. Change to your deployment in Azure OpenAI.
    public static string ModelDeployment => Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT") ?? "gpt-4o";

    public static (Uri Endpoint, AzureKeyCredential KeyCredential) GetValues() => s_values.Value;
}
