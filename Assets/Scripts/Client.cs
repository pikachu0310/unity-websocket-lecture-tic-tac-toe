using System;
using UnityEngine;
using WebSocketSharp;

public class Client
{
    private readonly WebSocket _ws;
    private event Action<string> OnReceiveMessage;

    public Client(string url, Action<string> receiveMessageCallback)
    {
        _ws = new WebSocket(url);
        OnReceiveMessage = receiveMessageCallback;
    }
    
    public void Connect()
    {
        _ws.OnOpen += (_, __) => { Debug.Log("Open WebSocket"); };
        _ws.OnMessage += (_, args) =>
        {
            Debug.LogWarning($"Receive Message {args.Data}");
            OnReceiveMessage?.Invoke(args.Data);
        };
        _ws.OnClose += (_, __) => { Debug.Log("Close WebSocket"); };
        _ws.Connect();
    }
    
    public void Send(string text)
    {
        Debug.Log("[SEND] " + text);
        _ws.Send(text);
    }
}