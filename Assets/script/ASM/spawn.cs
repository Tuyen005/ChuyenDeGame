////using UnityEngine;
////using UnityEngine.UI;
////using Fusion;
////using System.Collections;
////using TMPro; // Nếu dùng TextMeshPro

////public class spawn : SimulationBehaviour, IPlayerJoined
////{
////    public GameObject PlayerPrefab;
////    public TextMeshProUGUI countdownText; // Thêm Text UI đếm ngược
////    private static int playerCount = 0;
////    private static bool gameStarted = false;
////    private static bool countdownStarted = false;
////    public GameObject ItemPrefab;
////    public int numberOfItemsToSpawn = 10; // Số item muốn spawn
////    public Vector2 spawnAreaMin = new Vector2(20, 20); // Giới hạn X-Z nhỏ nhất
////    public Vector2 spawnAreaMax = new Vector2(60, 60); // Giới hạn X-Z lớn nhất
////    public void PlayerJoined(PlayerRef player)
////    {
////        playerCount++;
////        Debug.Log($"Số người chơi hiện tại: {playerCount}");

////        if (playerCount < 2)
////        {
////            Debug.Log("Chưa đủ người chơi, vui lòng chờ...");
////            if (countdownText != null) countdownText.text = "Chưa đủ người chơi...";
////            return;
////        }

////        if (playerCount >= 2 && !countdownStarted)
////        {
////            countdownStarted = true;
////            StartCoroutine(StartCountdown());
////        }
////    }

////    IEnumerator StartCountdown()
////    {
////        if (countdownText != null) countdownText.gameObject.SetActive(true); // Hiện UI

////        Debug.Log("Đủ người chơi! Game sẽ bắt đầu sau 3 giây...");
////        for (int i = 3; i > 0; i--)
////        {
////            if (countdownText != null) countdownText.text = i.ToString();
////            yield return new WaitForSeconds(1);
////        }

////        if (countdownText != null) countdownText.text = "BẮT ĐẦU!";
////        yield return new WaitForSeconds(1);

////        gameStarted = true;
////        Debug.Log("Game BẮT ĐẦU!");
////        if (countdownText != null) countdownText.gameObject.SetActive(false); // Ẩn UI

////        SpawnPlayers();

////        SpawnItems();
////    }

////    void SpawnPlayers()
////    {
////        foreach (var player in Runner.ActivePlayers)
////        {
////            if (player == Runner.LocalPlayer)
////            {
////                Runner.Spawn(PlayerPrefab, new Vector3(Random.Range(30f, 60f), 0, Random.Range(30f, 55f)), Quaternion.identity,
////                    Runner.LocalPlayer, (runner, obj) =>
////                    {
////                        var _player = obj.GetComponent<PlayerSetup>();
////                        _player.SetupCamera();
////                    });
////            }
////        }
////    }
////    void SpawnItems()
////    {
////        Debug.Log("Bắt đầu Spawn Items...");

////        if (Runner == null)
////        {
////            Debug.LogError("Runner chưa được khởi tạo!");
////            return;
////        }

////        if (ItemPrefab.GetComponent<NetworkObject>() == null)
////        {
////            Debug.LogError("ItemPrefab chưa có NetworkObject!");
////            return;
////        }
////        for (int i = 0; i < numberOfItemsToSpawn; i++)
////        {
////            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
////            float z = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
////            Vector3 randomPosition = new Vector3(x, 0, z);

////            Runner.Spawn(ItemPrefab, randomPosition, Quaternion.identity);
////        }

////        Debug.Log($"{numberOfItemsToSpawn} item đã được spawn ngẫu nhiên trên map!");
////    }

////}

////using UnityEngine;
////using UnityEngine.UI;
////using Fusion;
////using System.Collections;
////using System.Collections.Generic;
////using TMPro; // Sử dụng TextMeshPro

////public class spawn : SimulationBehaviour, IPlayerJoined
////{
////    public GameObject PlayerPrefab;
////    public TextMeshProUGUI countdownText; // Text UI đếm ngược
////    private int maxPlayers = 2; // Số người chơi tối đa
////    public GameObject ItemPrefab;
////    public int numberOfItemsToSpawn = 10; // Số item muốn spawn
////    public Vector2 spawnAreaMin = new Vector2(20, 20); // Giới hạn X-Z nhỏ nhất
////    public Vector2 spawnAreaMax = new Vector2(60, 60); // Giới hạn X-Z lớn nhất

////    private static int playerCount = 0;
////    private static bool gameStarted = false;
////    private static bool countdownStarted = false;
////    private HashSet<PlayerRef> joinedPlayers = new HashSet<PlayerRef>();

////    public void PlayerJoined(PlayerRef player)
////    {
////        // Nếu game đã bắt đầu, người chơi không được tham gia
////        if (gameStarted)
////        {
////            Debug.Log($"Người chơi {player} không thể tham gia vì game đã bắt đầu");
////            return;
////        }

////        // Nếu người chơi đã vượt quá giới hạn
////        if (playerCount >= maxPlayers)
////        {
////            Debug.Log($"Người chơi {player} không thể tham gia vì đã đạt giới hạn số người chơi");
////            return;
////        }

////        // Thêm người chơi vào danh sách
////        joinedPlayers.Add(player);
////        playerCount++;
////        Debug.Log($"Số người chơi hiện tại: {playerCount}");

////        // Kiểm tra số lượng người chơi tối thiểu (2 người)
////        if (playerCount < 2)
////        {
////            Debug.Log("Chưa đủ người chơi, vui lòng chờ...");
////            if (countdownText != null) countdownText.text = "Chưa đủ người chơi...";
////            return;
////        }

////        // Bắt đầu đếm ngược khi đủ 2 người chơi
////        if (playerCount == 2 && !countdownStarted)
////        {
////            countdownStarted = true;
////            StartCoroutine(StartCountdown());
////        }

////        // Nếu đạt số lượng người tối đa, bắt đầu game ngay lập tức
////        if (playerCount == maxPlayers && !gameStarted && !countdownStarted)
////        {
////            countdownStarted = true;
////            StartCoroutine(StartCountdown());
////        }
////    }

////    IEnumerator StartCountdown()
////    {
////        if (countdownText != null) countdownText.gameObject.SetActive(true); // Hiện UI
////        Debug.Log("Đủ người chơi! Game sẽ bắt đầu sau 3 giây...");
////        for (int i = 3; i > 0; i--)
////        {
////            if (countdownText != null) countdownText.text = i.ToString();
////            yield return new WaitForSeconds(1);
////        }
////        if (countdownText != null) countdownText.text = "BẮT ĐẦU!";
////        yield return new WaitForSeconds(1);

////        // Đánh dấu game đã bắt đầu
////        gameStarted = true;
////        Debug.Log("Game BẮT ĐẦU!");

////        if (countdownText != null) countdownText.gameObject.SetActive(false); // Ẩn UI

////        // Spawn các nhân vật cho những người chơi đã tham gia trước khi game bắt đầu
////        SpawnPlayers();

////        // Spawn các item
////        SpawnItems();
////    }

////    void SpawnPlayers()
////    {
////        foreach (var player in joinedPlayers)
////        {
////            if (player == Runner.LocalPlayer)
////            {
////                Runner.Spawn(PlayerPrefab, new Vector3(Random.Range(30f, 60f), 0, Random.Range(30f, 55f)), Quaternion.identity,
////                    Runner.LocalPlayer, (runner, obj) =>
////                    {
////                        // Nếu PlayerSetup tồn tại, gọi phương thức SetupCamera
////                        var _player = obj.GetComponent<PlayerSetup>();
////                        if (_player != null)
////                        {
////                            _player.SetupCamera();
////                        }
////                    });
////            }
////        }
////    }

////    void SpawnItems()
////    {
////        Debug.Log("Bắt đầu Spawn Items...");
////        if (Runner == null)
////        {
////            Debug.LogError("Runner chưa được khởi tạo!");
////            return;
////        }

////        if (ItemPrefab == null)
////        {
////            Debug.LogError("ItemPrefab chưa được gán!");
////            return;
////        }

////        if (ItemPrefab.GetComponent<NetworkObject>() == null)
////        {
////            Debug.LogError("ItemPrefab chưa có NetworkObject!");
////            return;
////        }

////        for (int i = 0; i < numberOfItemsToSpawn; i++)
////        {
////            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
////            float z = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
////            Vector3 randomPosition = new Vector3(x, 0, z);
////            Runner.Spawn(ItemPrefab, randomPosition, Quaternion.identity);
////        }

////        Debug.Log($"{numberOfItemsToSpawn} item đã được spawn ngẫu nhiên trên map!");
////    }
////}
///






//Phần đêm 22/4
using UnityEngine;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro; // Sử dụng TextMeshPro
using System.Linq; // Để sử dụng FirstOrDefault

public class spawn : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public TextMeshProUGUI countdownText; // Text UI đếm ngược
    private int maxPlayers = 2; // Số người chơi tối đa
    public GameObject ItemPrefab;
    public int numberOfItemsToSpawn = 10; // Số item muốn spawn
    public Vector2 spawnAreaMin = new Vector2(20, 20); // Giới hạn X-Z nhỏ nhất
    public Vector2 spawnAreaMax = new Vector2(60, 60); // Giới hạn X-Z lớn nhất

    private static int playerCount = 0;
    private static bool gameStarted = false;
    private static bool countdownStarted = false;
    private static HashSet<PlayerRef> joinedPlayers = new HashSet<PlayerRef>();
    private static readonly object lockObject = new object(); // Đối tượng khóa để đồng bộ

    void Awake()
    {
        //  Debug.Log($"Script spawn khởi động. Runner: {(Runner != null ? "Có" : "Không")}, IsSharedMode: {(Runner != null ? Runner.IsSharedMode.ToString() : "N/A")}");
    }

    public void PlayerJoined(PlayerRef player)
    {
        //Debug.Log($"Người chơi {player} đang cố gắng tham gia. IsSharedMode: {(Runner != null ? Runner.IsSharedMode.ToString() : "N/A")}, playerCount: {playerCount}, joinedPlayers: {joinedPlayers.Count}, gameStarted: {gameStarted}");

        // Nếu game đã bắt đầu, người chơi không được tham gia
        if (gameStarted)
        {
            Debug.Log($"Người chơi {player} không thể tham gia vì game đã bắt đầu");
            if (countdownText != null)
            {
                countdownText.text = "Game đã bắt đầu!";
                countdownText.gameObject.SetActive(true);
            }
            return;
        }

        // Đồng bộ truy cập để ngăn race condition
        lock (lockObject)
        {
            // Kiểm tra cả playerCount và joinedPlayers để đảm bảo không vượt quá maxPlayers
            if (playerCount > maxPlayers || joinedPlayers.Count > maxPlayers)
            {
                Debug.Log($"Người chơi {player} không thể tham gia vì đã đạt giới hạn số người chơi (playerCount: {playerCount}, joinedPlayers: {joinedPlayers.Count})");
                if (countdownText != null)
                {
                    countdownText.text = "Đã đủ người chơi!";
                    countdownText.gameObject.SetActive(true);
                }
                return;
            }

            // Thêm người chơi vào danh sách
            joinedPlayers.Add(player);
            playerCount++;
            Debug.Log($"Số người chơi hiện tại: {playerCount}/{maxPlayers}, joinedPlayers: {joinedPlayers.Count}");
        }

        // Kiểm tra số lượng người chơi tối thiểu (2 người)
        if (playerCount < maxPlayers)
        {
            Debug.Log("Chưa đủ người chơi, vui lòng chờ...");
            if (countdownText != null) countdownText.text = "Đang chờ người chơi thứ 2...";
            return;
        }

        // Bắt đầu đếm ngược khi đủ 2 người chơi
        if (!countdownStarted)
        {
            countdownStarted = true;
            if (countdownText != null) countdownText.text = "Đã đủ người chơi! Chuẩn bị bắt đầu!";
            StartCoroutine(StartCountdown());
        }
    }

    IEnumerator StartCountdown()
    {
        if (countdownText != null) countdownText.gameObject.SetActive(true); // Hiện UI
        Debug.Log("Đủ người chơi! Game sẽ bắt đầu sau 3 giây...");
        for (int i = 3; i > 0; i--)
        {
            if (countdownText != null) countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        if (countdownText != null) countdownText.text = "BẮT ĐẦU!";
        yield return new WaitForSeconds(1);

        // Đánh dấu game đã bắt đầu
        gameStarted = true;
        Debug.Log("Game BẮT ĐẦU! Số người chơi: " + joinedPlayers.Count);

        if (countdownText != null) countdownText.gameObject.SetActive(false); // Ẩn UI

        // Spawn các nhân vật cho những người chơi đã tham gia
        SpawnPlayers();

        // Spawn các item
        SpawnItems();
    }

    void SpawnPlayers()
    {
        if (Runner == null)
        {
            Debug.LogError("Runner chưa được khởi tạo! Không thể spawn người chơi.");
            return;
        }

        if (PlayerPrefab == null)
        {
            Debug.LogError("PlayerPrefab chưa được gán! Không thể spawn người chơi.");
            return;
        }

        if (PlayerPrefab.GetComponent<NetworkObject>() == null)
        {
            Debug.LogError("PlayerPrefab chưa có NetworkObject! Không thể spawn người chơi.");
            return;
        }

        // Kiểm tra số lượng người chơi để tránh spawn dư
        if (joinedPlayers.Count > maxPlayers || playerCount > maxPlayers)
        {
            Debug.LogWarning($"Cảnh báo: joinedPlayers ({joinedPlayers.Count}) hoặc playerCount ({playerCount}) vượt quá maxPlayers ({maxPlayers}). Chỉ spawn tối đa {maxPlayers} người chơi.");
        }

        Debug.Log($"Spawning players for joined players: {joinedPlayers.Count}, playerCount: {playerCount}");
        foreach (var player in joinedPlayers)
        {
            if (player == Runner.LocalPlayer && joinedPlayers.Count <= maxPlayers && playerCount <= maxPlayers)
            {
                Debug.Log($"Spawning player for LocalPlayer: {player}");
                Runner.Spawn(PlayerPrefab,
                    new Vector3(Random.Range(30f, 60f), 0, Random.Range(30f, 55f)),
                    Quaternion.identity,
                    Runner.LocalPlayer,
                    (runner, obj) =>
                    {
                        // Nếu PlayerSetup tồn tại, gọi phương thức SetupCamera
                        var _player = obj.GetComponent<PlayerSetup>();
                        if (_player != null)
                        {
                            _player.SetupCamera();
                        }
                        else
                        {
                            Debug.LogError("PlayerSetup không tìm thấy trên PlayerPrefab!");
                        }
                    });
            }
            else if (player == Runner.LocalPlayer)
            {
                Debug.Log($"Không spawn LocalPlayer {player} vì joinedPlayers ({joinedPlayers.Count}) hoặc playerCount ({playerCount}) vượt quá maxPlayers ({maxPlayers})");
            }
        }
    }

    void SpawnItems()
    {
        if (Runner == null)
        {
            Debug.LogError("Runner chưa được khởi tạo!");
            return;
        }

        if (ItemPrefab == null)
        {
            Debug.LogError("ItemPrefab chưa được gán!");
            return;
        }

        if (ItemPrefab.GetComponent<NetworkObject>() == null)
        {
            Debug.LogError("ItemPrefab chưa có NetworkObject!");
            return;
        }

        // Chỉ client đầu tiên (người chơi đầu tiên trong joinedPlayers) spawn items
        if (Runner.LocalPlayer == joinedPlayers.FirstOrDefault())
        {
            Debug.Log("Bắt đầu Spawn Items...");
            for (int i = 0; i < numberOfItemsToSpawn; i++)
            {
                float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
                float z = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
                Vector3 randomPosition = new Vector3(x, 0, z);
                Runner.Spawn(ItemPrefab, randomPosition, Quaternion.identity);
            }
            Debug.Log($"{numberOfItemsToSpawn} item đã được spawn ngẫu nhiên trên map!");
        }
    }
}

