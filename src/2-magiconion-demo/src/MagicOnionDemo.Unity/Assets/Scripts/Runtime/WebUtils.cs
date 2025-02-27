using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public static class WebUtils
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void ConsoleLog(string message);
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Log(string message)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ConsoleLog(message);
#endif
        UnityEngine.Debug.LogError(message);
    }
}