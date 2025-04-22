using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatUI : MonoBehaviour
{
    public TMP_InputField chatInputField; // Trường nhập liệu cho chat
    public Button sendButton; // Nút gửi chat
    public TextMeshProUGUI chatContent; // Nội dung chat hiển thị

    private void Start()
    {
        sendButton.onClick.AddListener(SendMessage); // Gán sự kiện cho nút gửi
    }

    private void SendMessage()
    {
        string message = chatInputField.text; // Lấy nội dung từ trường nhập liệu
        if (!string.IsNullOrEmpty(message) )
        {
            ChatManager.Instance.SendChatMessage(message); // Gửi tin nhắn qua ChatManager
            chatInputField.text = ""; // Xóa trường nhập liệu sau khi gửi
        }
    }
}
