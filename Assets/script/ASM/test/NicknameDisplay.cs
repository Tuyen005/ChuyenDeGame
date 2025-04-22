//using UnityEngine;
//using TMPro;
//using UnityEngine.UI;

//public class NicknameDisplay : MonoBehaviour
//{
//    public TMP_InputField nicknameInput;
//    public Button changeButton;
//    private Player localPlayer;

//    void Start()
//    {
//        // Tải nickname hiện tại (nếu có)
//        string savedNickname = PlayerPrefs.GetString("PlayerNickname", "");
//        if (!string.IsNullOrEmpty(savedNickname))
//        {
//            nicknameInput.text = savedNickname;
//        }

//        // Thêm listener cho nút đổi tên
//        changeButton.onClick.AddListener(ChangeNickname);
//    }

//    void Update()
//    {
//        // Tìm người chơi local nếu chưa tìm thấy
//        if (localPlayer == null)
//        {
//            FindLocalPlayer();
//        }
//    }

//    private void FindLocalPlayer()
//    {
//        // Tìm tất cả người chơi trong scene
//        Player[] players = FindObjectsOfType<Player>();
//        foreach (Player player in players)
//        {
//            // Kiểm tra xem player có phải là local player không
//            if (player.Object != null && player.Object.HasInputAuthority)
//            {
//                localPlayer = player;
//                break;
//            }
//        }
//    }

//    public void ChangeNickname()
//    {
//        if (localPlayer != null && !string.IsNullOrEmpty(nicknameInput.text))
//        {
//            localPlayer.ChangePlayerName(nicknameInput.text);
//        }
//        else
//        {
//            Debug.LogWarning("Không thể đổi tên: localPlayer chưa được tìm thấy hoặc tên rỗng");
//        }
//    }
//}

using UnityEngine;
using TMPro;
using Fusion;

public class NicknameDisplay : MonoBehaviour
{
    private TextMeshPro textComponent;
    private Player playerScript;

    void Awake()
    {
        // Lấy component TextMeshPro
        textComponent = GetComponent<TextMeshPro>();
        if (textComponent == null)
        {
            Debug.LogError("Không tìm thấy component TextMeshPro!");
            enabled = false;
            return;
        }

        // Tìm script Player (thường là component trên GameObject cha)
        playerScript = GetComponentInParent<Player>();
        if (playerScript == null)
        {
            Debug.LogError("Không tìm thấy script Player!");
            enabled = false;
            return;
        }
    }

    void LateUpdate()
    {
        // Cập nhật tên hiển thị từ Player script
        if (textComponent != null && playerScript != null)
        {
            textComponent.text = playerScript.PlayerName.ToString();
        }

        // Đảm bảo text luôn quay về phía camera
        if (Camera.main != null)
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward);
            // Đảo ngược để text hiển thị đúng
            transform.Rotate(0, 360, 0);
        }
    }
}