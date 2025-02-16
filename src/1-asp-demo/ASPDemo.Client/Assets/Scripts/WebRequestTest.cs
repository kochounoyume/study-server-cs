using System;
using System.Net.Http;
using System.Text.Json;
using ASPDemo.Shared.Models;
using Cysharp.Threading.Tasks;
using MinimalUtility.WebRequest;
using TMPro;
using UnityEngine;

namespace ASPDemo.Client
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class WebRequestTest : MonoBehaviour
    {
        [SerializeField]
        private string baseurl = "https://localhost:7280";

        private async UniTaskVoid Start()
        {
            using var client = new HttpClient(new UnityWebRequestHttpMessageHandler(), true);
            client.BaseAddress = new Uri(baseurl);
            client.Timeout = TimeSpan.FromSeconds(10);

            var uniqueId = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
            var rawRequest = JsonSerializer.SerializeToUtf8Bytes(new DemoItem(uniqueId, "Hello", true), JsonSerializerOptions.Default);
            using var request = new HttpRequestMessage(HttpMethod.Post, "api/DemoItems");
            request.Content = new ByteArrayContent(rawRequest);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await client.SendAsync(request, destroyCancellationToken);
            var rawResponse = await response.Content.ReadAsByteArrayAsync();
            var result = JsonSerializer.Deserialize<DemoItem>(rawResponse, JsonSerializerOptions.Default);

            GetComponent<TextMeshProUGUI>().text = $"Response: {result.id}, {result.name}, {result.isComplete}";
        }
    }
}