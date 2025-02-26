using System;
using Cysharp.Threading.Tasks;
using MagicOnion;
using MagicOnion.Client;
using MagicOnionDemo.Shared;
using UnityEngine;

public class SampleScene : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
    const string endpoint = "http://localhost:5000";
#else
    const string endpoint = "http://localhost:5001";
#endif
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async UniTaskVoid Start()
    {
        var debugView = DebugView.instance;
        var channel = GrpcChannelx.ForAddress(endpoint);
  
        try
        {
            var client = MagicOnionClient.Create<IMyFirstService>(channel);

            var result = await client.SumAsync(100, 200);
            debugView.message += @$"100 + 200 = {result}
";
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            debugView.message += @$"Error: {e.Message}
";
        }

        try
        {
            var receiver = new GreeterHubReceiver();
            var hub = await StreamingHubClient.ConnectAsync<IGreeterHub, IGreeterHubReceiver>(channel, receiver);
            var result = await hub.HelloAsync("Alice");
            debugView.message += @$"HelloAsync: {result}
";
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            debugView.message += @$"Error: {e.Message}
";
            throw;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}