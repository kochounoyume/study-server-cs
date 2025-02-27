using Grpc.Net.Client;
using MagicOnion.Unity;
using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;

public class MagicOnionInitializer
{
#if UNITY_WEBGL && !UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeSynchronizationContext()
        {
            System.Threading.SynchronizationContext.SetSynchronizationContext(null);
        }
#endif

    [RuntimeInitializeOnLoadMethod]
    public static void OnRuntimeInitialize()
    {
        StaticCompositeResolver.Instance.Register(
            MagicOnionClientInitializer.Resolver,
            MagicOnionClientInitializer.Resolver,
            BuiltinResolver.Instance,
            PrimitiveObjectResolver.Instance
        );
        
        MessagePackSerializer.DefaultOptions = MessagePackSerializer.DefaultOptions
            .WithResolver(StaticCompositeResolver.Instance);

        // Initialize gRPC channel provider when the application is loaded.
        GrpcChannelProviderHost.Initialize(new DefaultGrpcChannelProvider(static () => new GrpcChannelOptions()
        {
//#if UNITY_WEBGL
            HttpHandler = new GrpcWebSocketBridge.Client.GrpcWebSocketBridgeHandler(),
//#else
            // HttpHandler = new Cysharp.Net.Http.YetAnotherHttpHandler()
            // {
            //     Http2Only = true,
            // },
//#endif
            DisposeHttpClient = true,
        }));
    }
}