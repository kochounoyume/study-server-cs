using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public sealed class ServerProcessManager : ScriptableSingleton<ServerProcessManager>
{
    public bool running { get; private set; }
    private int _currentProcessId;

    public void SwitchServer()
    {
        if (running)
        {
            var process = Process.GetProcessById(_currentProcessId);
            process.Kill();
            process.Dispose();
            running = false;
            return;
        }

        var startInfo = new ProcessStartInfo()
        {
            FileName = "dotnet",
            Arguments = "run",
            WorkingDirectory = Path.Combine(Path.GetFullPath(Path.Combine(Application.dataPath, @"..\..\")), "MagicOnionDemo.Server"),
            UseShellExecute = true,
        };

        try
        {
            var process = Process.Start(startInfo);
            if (process != null)
            {
                _currentProcessId = process.Id;
                running = true;
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogException(e);
            throw;
        }
    }
}