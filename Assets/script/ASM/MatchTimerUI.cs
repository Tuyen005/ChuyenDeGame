// MatchTimerUI.cs (hiển thị thời gian cho người chơi - không cần là NetworkBehaviour)
using UnityEngine;
using TMPro;

public class MatchTimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    void Update()
    {
        if (MatchTimer.Instance1 == null) return;

        float time = MatchTimer.Instance1.TimeRemaining;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }
}
