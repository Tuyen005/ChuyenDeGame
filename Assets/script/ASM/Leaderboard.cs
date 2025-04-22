using UnityEngine;

using TMPro;
using System.Collections.Generic;
using Fusion;
using static Unity.Collections.Unicode;

public class Leaderboard : NetworkBehaviour
{
    public CanvasGroup leaderboardPanel;
    public TextMeshProUGUI leaderboardText;

    void Start()
    {
        if (leaderboardPanel != null)
        {
            leaderboardPanel.alpha = 0;
            leaderboardPanel.interactable = false;
            leaderboardPanel.blocksRaycasts = false;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            ShowLeaderboard();
        }
        else
        {
            HideLeaderboard();
        }
    }

    private void ShowLeaderboard()
    {
        if (leaderboardPanel == null || leaderboardText == null) return;

        leaderboardPanel.alpha = 1;
        leaderboardPanel.interactable = true;
        leaderboardPanel.blocksRaycasts = true;

        leaderboardText.text = "Bảng Xếp Hạng\n";
        List<(string, int)> sortedScores = new List<(string, int)>();

        foreach (PlayerRef player in Runner.ActivePlayers)
        {
            if (Runner.TryGetPlayerObject(player, out NetworkObject playerObject))
            {
                var playerScript = playerObject.GetComponent<Player>();
                if (playerScript != null && playerScript.PlayerName != "")
                {
                    sortedScores.Add((playerScript.PlayerName.ToString(), playerScript.Score));
                }
            }
        }

        sortedScores.Sort((a, b) => b.Item2.CompareTo(a.Item2));
        for (int i = 0; i < sortedScores.Count && i < 5; i++)
        {
            leaderboardText.text += $"{sortedScores[i].Item1}: {sortedScores[i].Item2}\n";
        }
    }

    private void HideLeaderboard()
    {
        if (leaderboardPanel == null) return;
        leaderboardPanel.alpha = 0;
        leaderboardPanel.interactable = false;
        leaderboardPanel.blocksRaycasts = false;
    }
}