using System;
using MagicOnion;
using MagicOnion.Client;
using ChatApp.Shared;
using UnityEngine;

public class SampleScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        try
        {
            var channel = GrpcChannelx.ForAddress("http://localhost:5000");
            var client = MagicOnionClient.Create<IMyFirstService>(channel);

            var result = await client.SumAsync(100, 200);
            Debug.Log($"100 + 200 = {result}");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}