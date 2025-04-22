//using UnityEngine;
//using Fusion;
//using TMPro;
//public class MatchTimer : NetworkBehaviour
//{
//    public TextMeshProUGUI timerText;

//    [Networked]
//    public float TimeRemaining { get; set; }

//    public override void Spawned()
//    {
//        if (Object.HasStateAuthority)
//        {
//            TimeRemaining = 500f; // Ví dụ: 2 phút
//        }
//    }

//    public override void FixedUpdateNetwork()
//    {
//        if (Object.HasStateAuthority && TimeRemaining > 0f)
//        {
//            TimeRemaining -= Runner.DeltaTime;
//        }

//        UpdateTimerUI();
//    }

//    private void UpdateTimerUI()
//    {
//        int minutes = Mathf.FloorToInt(TimeRemaining / 60);
//        int seconds = Mathf.FloorToInt(TimeRemaining % 60);
//        timerText.text = $"{minutes:D2}:{seconds:D2}";
//    }
//}
// MatchTimer.cs (chỉ xử lý logic đếm thời gian)
using UnityEngine;
using Fusion;

public class MatchTimer : NetworkBehaviour
{
    [Networked] public float TimeRemaining { get; set; }

    public static MatchTimer Instance1;

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            TimeRemaining = 500f;
        }

        Instance1 = this;
    }

    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority && TimeRemaining > 0f)
        {
            TimeRemaining -= Runner.DeltaTime;
        }
    }
}
