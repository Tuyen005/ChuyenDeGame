using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NicknameUI : MonoBehaviour
{
    public TMP_InputField nicknameInput;
    public Button changeButton;
    public GameObject panel;

    private Player localPlayer;
    private bool isVisible = true;

    void Start()
    {
        // Nếu panel chưa được gán, tự tìm
        if (panel == null)
        {
            panel = transform.GetChild(0).gameObject;
        }

        // Tải nickname đã lưu (nếu có)
        //string savedName = PlayerPrefs.GetString("PlayerNickname", "");
        //if (!string.IsNullOrEmpty(savedName))
        //{
        //    nicknameInput.text = savedName;
        //}

        // Thêm listener cho button
        if (changeButton != null)
        {
            changeButton.onClick.AddListener(OnChangeNickname);
        }
        else
        {
            Debug.LogError("Change button chưa được gán!");
        }

        // Ẩn UI khi bắt đầu (tùy chọn)
        panel.SetActive(false);
        isVisible = false;
    }

    void Update()
    {
        // Tìm local player nếu chưa tìm thấy
        if (localPlayer == null)
        {
            FindLocalPlayer();
        }

        // Toggle hiển thị UI với phím Tab
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isVisible = !isVisible;
            panel.SetActive(isVisible);
        }
    }

    void FindLocalPlayer()
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player player in players)
        {
            if (player.Object != null && player.Object.HasInputAuthority)
            {
                localPlayer = player;
                Debug.Log("Đã tìm thấy local player");
                break;
            }
        }
    }

    void OnChangeNickname()
    {
        if (localPlayer != null && !string.IsNullOrEmpty(nicknameInput.text))
        {
            localPlayer.ChangePlayerName(nicknameInput.text);
            Debug.Log($"Đã đổi tên thành: {nicknameInput.text}");
        }
        else
        {
            Debug.LogWarning("Không thể đổi tên: local player chưa được tìm thấy hoặc tên rỗng");
        }
    }
}