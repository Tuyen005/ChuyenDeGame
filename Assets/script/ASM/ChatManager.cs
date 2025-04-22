using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : NetworkBehaviour
{
    public static ChatManager Instance;
    private List<string> chatMessages = new List<string>();
    public ChatUI ChatUI;

    private void Awake()
    {
        Instance = this;
    }


    [Rpc (RpcSources.All, RpcTargets.All)]
    public void RpcReceiveMessage( string playerName, string message )
    {
        string formattedMessage = $"{playerName}: {message}";
        chatMessages.Add(formattedMessage);
        ChatUI.chatContent.text += formattedMessage + "\n"; // Cập nhật nội dung chat UI
    }

    public void SendChatMessage(string message)
    {
        string playerName = Runner.LocalPlayer.PlayerId.ToString(); // Lấy tên người chơi từ PlayerRef
        RpcReceiveMessage( playerName, message );
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
