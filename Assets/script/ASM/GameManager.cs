//////using UnityEngine;

//////public class GameManager : MonoBehaviour
//////{
//////    public static GameManager _instance;

//////    public GameObject gameOverPanel;

//////    private void Awake()
//////    {
//////        if (_instance == null)
//////            _instance = this;
//////        else
//////            Destroy(gameObject);

//////        if (gameOverPanel != null)
//////            gameOverPanel.SetActive(false); // Ẩn ban đầu
//////    }

//////    public void ShowGameOver()
//////    {
//////        if (gameOverPanel != null)
//////        {
//////            gameOverPanel.SetActive(true);
//////            // Bạn có thể thêm code dừng thời gian nếu muốn:
//////            // Time.timeScale = 0;
//////        }
//////    }
//////}

//using Fusion;
//using System.Collections.Generic;
//using UnityEngine;

//public class GameManager : NetworkBehaviour
//{
//    public static GameManager Instance;

//    [Networked]
//    public GameState State { get; set; }

//    [Networked]
//    public int MaxPlayers { get; set; } = 2;

//    [Networked]
//    public NetworkBool GameStarted { get; set; }

//    private GameState _previousState;

//    public enum GameState
//    {
//        Waiting,
//        Playing,
//        GameOver
//    }

//    private PlayerSpawner _playerSpawner;

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//        }
//        else if (Instance != this)
//        {
//            Destroy(gameObject);
//        }

//        _playerSpawner = GetComponent<PlayerSpawner>();
//    }

//    public override void Spawned()
//    {
//        if (Object.HasStateAuthority)
//        {
//            State = GameState.Waiting;
//            GameStarted = false;
//        }

//        _previousState = State;
//    }

//    public override void FixedUpdateNetwork()
//    {
//        // Kiểm tra nếu trạng thái thay đổi
//        if (_previousState != State)
//        {
//            Debug.Log($"Game state changed to: {State}");

//            // Thông báo thay đổi trạng thái cho UI
//            UIManager.Instance?.UpdateGameStateUI(State);

//            _previousState = State;
//        }
//    }

//    public void CheckAndStartGame()
//    {
//        if (!Object.HasStateAuthority) return;

//        // Đếm số người chơi hiện tại
//        int playerCount = 0;
//        foreach (PlayerRef player in Runner.ActivePlayers)
//        {
//            playerCount++;
//        }

//        Debug.Log($"Current player count: {playerCount}, Max players: {MaxPlayers}");

//        // Nếu đủ số người chơi và game chưa bắt đầu
//        if (playerCount >= MaxPlayers && !GameStarted)
//        {
//            StartGame();
//        }
//    }

//    public void StartGame()
//    {
//        if (!Object.HasStateAuthority) return;

//        GameStarted = true;
//        State = GameState.Playing;
//        Debug.Log("Game has started!");
//    }

//    // Phương thức để kiểm tra xem người chơi có thể spawn không
//    public bool CanSpawnPlayer(PlayerRef player)
//    {
//        return !GameStarted || Runner.GetPlayerObject(player) != null;
//    }
//}