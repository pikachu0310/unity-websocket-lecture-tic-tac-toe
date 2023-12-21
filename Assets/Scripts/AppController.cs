using System;
using System.Linq;
using System.Threading;
using UnityEngine;

public class AppController : MonoBehaviour
{
    private Client _client;
    private SynchronizationContext _context;
    [SerializeField] private Board _board;

    private void Start()
    {
        _context = SynchronizationContext.Current;   
        _board.SetCellClickEvent(position =>
        {
            Debug.Log($"Cell {position} is Clicked");
            var message = new CellClickSendMessage {
                messageType = "CellClick",
                x = position.x,
                y = position.y
            };
            var jsonData = JsonUtility.ToJson(message);
            _client.Send(jsonData);
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Key Down S");
            _client = new Client("ws://tic-tac-toe.trap.games:39999/ws", OnReceiveMessage);
            _client.Connect();
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.LogWarning("Key Down A");
            _client.Send("Hello From Client");
        }
    }
    
    private void OnReceiveMessage(string data)
    {
        var message = JsonUtility.FromJson<BoardUpdateReceiveMessage>(data);
        if (message.messageType == "BoardUpdate")
        {
            var cellTypes = message.cells.Select(typeInt => (Cell.ViewType)(typeInt + 1)).ToArray();
            _context.Post(_ =>
            {
                _board.UpdateView(cellTypes);
            }, null);
        }
    }
}