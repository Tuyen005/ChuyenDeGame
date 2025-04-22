using Fusion;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class TopPlayerDisplay : NetworkBehaviour
{
    public static TopPlayerDisplay Instance;

    [SerializeField] private TextMeshProUGUI topPlayerNameText; // Text hiển thị tên
    [SerializeField] private TextMeshProUGUI topPlayerScoreText; // Text hiển thị điểm
    [SerializeField] private GameObject topPlayerPanel; // Panel chứa UI

    [Networked, Capacity(100)] // Lưu điểm số của tất cả người chơi
    private NetworkDictionary<NetworkString<_16>, int> PlayerScores { get; }

    private void Awake()
    {
        // Singleton pattern để dễ truy cập
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void Spawned()
    {
        // Ẩn panel khi khởi tạo
        if (topPlayerPanel != null)
        {
            topPlayerPanel.SetActive(false);
        }
    }

    // Thêm điểm khi người chơi tiêu diệt người khác (chỉ gọi từ StateAuthority)
    public void AddKillPoint(NetworkString<_16> playerName)
    {
        if (!Object.HasStateAuthority) return;

        if (!PlayerScores.ContainsKey(playerName))
        {
            PlayerScores.Set(playerName, 0);
        }

        PlayerScores.Set(playerName, PlayerScores[playerName] + 1);
        RPC_UpdateTopPlayerDisplay(); // Gọi RPC để cập nhật UI trên tất cả client
    }

    // Reset điểm khi bắt đầu game mới
    public void ResetScores()
    {
        if (!Object.HasStateAuthority) return;

        PlayerScores.Clear();
        RPC_UpdateTopPlayerDisplay();
    }

    // RPC để cập nhật UI trên tất cả client
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_UpdateTopPlayerDisplay()
    {
        if (PlayerScores.Count == 0)
        {
            if (topPlayerPanel != null)
            {
                topPlayerPanel.SetActive(false);
            }
            return;
        }

        // Tìm người chơi có điểm cao nhất
        var topPlayer = PlayerScores.OrderByDescending(x => x.Value).First();

        // Cập nhật UI
        if (topPlayerNameText != null)
        {
            topPlayerNameText.text = topPlayer.Key.ToString();
        }

        if (topPlayerScoreText != null)
        {
            topPlayerScoreText.text = $"Score: {topPlayer.Value}";
        }

        if (topPlayerPanel != null)
        {
            topPlayerPanel.SetActive(true);
        }
    }
}