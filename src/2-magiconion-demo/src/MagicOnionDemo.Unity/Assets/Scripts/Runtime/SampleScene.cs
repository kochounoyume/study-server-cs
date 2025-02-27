using System;
using Cysharp.Threading.Tasks;
using Grpc.Core;
using MagicOnion;
using MagicOnion.Client;
using MagicOnionDemo.Shared;
using UnityEngine;

public class SampleScene : MonoBehaviour
{
//#if UNITY_WEBGL
    const string endpoint = "http://localhost:5000";
//#else
    // const string endpoint = "http://localhost:5001";
//#endif
    
    private readonly Lazy<ChannelBase> channel = new Lazy<ChannelBase>(() => GrpcChannelx.ForAddress(endpoint));
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async UniTaskVoid Start()
    {
        WebUtils.Log("Start");
        WebUtils.Log("ほげほげ");

        await UniTask.NextFrame(destroyCancellationToken);
        WebUtils.Log("1フレーム待機テスト");
        
        try
        {
            var client = MagicOnionClient.Create<IMyFirstService>(channel.Value);

            // thread number debug
            var result = await client.SumAsync(100, 200);
            WebUtils.Log($"100 + 200 = {result}");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            WebUtils.Log("API ERROR " + e.Message);
        }
        
        WebUtils.Log("ふがふが");
        
        try
        {
            var receiver = new GreeterHubReceiver();
            var hub = await StreamingHubClient.ConnectAsync<IGreeterHub, IGreeterHubReceiver>(channel.Value, receiver);
            var result = await hub.HelloAsync("Alice");
            WebUtils.Log($"HelloAsync = {result}");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            WebUtils.Log("STREAMING ERROR " + e.Message);
            throw;
        }
    }
}