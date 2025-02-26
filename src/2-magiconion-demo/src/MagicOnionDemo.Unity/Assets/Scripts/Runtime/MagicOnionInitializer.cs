using Cysharp.Net.Http;
using Grpc.Net.Client;
using MagicOnion.Unity;
using MessagePack.Resolvers;
using UnityEngine;

public class MagicOnionInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void OnRuntimeInitialize()
    {
        StaticCompositeResolver.Instance.Register(MagicOnionClientInitializer.Resolver);

        // Initialize gRPC channel provider when the application is loaded.
        GrpcChannelProviderHost.Initialize(new DefaultGrpcChannelProvider(static () => new GrpcChannelOptions()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            HttpHandler = new GrpcWebSocketBridge.Client.GrpcWebSocketBridgeHandler(),
#else
            HttpHandler = new YetAnotherHttpHandler()
            {
                Http2Only = true,
            },
#endif
            DisposeHttpClient = true,
        }));

#if UNITY_WEBGL && !UNITY_EDITOR
        System.Threading.SynchronizationContext.SetSynchronizationContext(null);
#endif
    }
}