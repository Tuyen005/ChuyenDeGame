////////using Fusion;
////////using UnityEngine;

////////public class PlayerSpwaner : SimulationBehaviour, IPlayerJoined
////////{
////////    public GameObject PlayerPrefab;


////////    public void PlayerJoined(PlayerRef player)
////////    {
////////        if (player == Runner.LocalPlayer)
////////        {
////////            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
////////            Runner.Spawn(PlayerPrefab, randomPosition, Quaternion.identity);
////////        }
////////    }

////////}
////using Fusion;
////using UnityEngine;

////public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
////{



////    public GameObject PlayerPrefab;

////    public void PlayerJoined(PlayerRef player)
////    {
////        Debug.Log($"[PlayerSpawner] Player {player.PlayerId} đã tham gia.");

////        if (player == Runner.LocalPlayer)
////        {
////            Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
////            NetworkObject playerObject = Runner.Spawn(
////                PlayerPrefab,
////                spawnPosition,
////                Quaternion.identity,
////                player,
////                (runner, obj) =>
////                {
////                    Debug.Log($"[PlayerSpawner] Đã spawn Player {player.PlayerId} tại {spawnPosition}.");
////                    runner.SetPlayerObject(player, obj); // Đăng ký Player với Runner
////                    Debug.Log($"[PlayerSpawner] Đã đăng ký Player {player.PlayerId} với Runner.");
////                    obj.GetComponent<PlayerSetup>()?.SetupCamera();
////                }
////            );
////            Debug.Log($"[PlayerSpawner] Gửi yêu cầu spawn Player {player.PlayerId}.");
////        }
////    }
////}
//using Fusion;
//using UnityEngine;

//public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
//{
//    public GameObject PlayerPrefab;
//    public GameObject SpectatorPrefab; // Camera cho người chơi đến muộn

//    public void PlayerJoined(PlayerRef player)
//    {
//        Debug.Log($"[PlayerSpawner] Player {player.PlayerId} đã tham gia.");

//        // Cập nhật số lượng người chơi và kiểm tra điều kiện bắt đầu game
//        GameManager.Instance?.CheckAndStartGame();

//        if (player == Runner.LocalPlayer)
//        {
//            // Kiểm tra xem có thể spawn người chơi không
//            bool canSpawn = GameManager.Instance == null || !GameManager.Instance.GameStarted;

//            if (canSpawn)
//            {
//                SpawnPlayer(player);
//            }
//            else
//            {
//                SpawnSpectator(player);
//            }
//        }
//    }

//    private void SpawnPlayer(PlayerRef player)
//    {
//        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
//        NetworkObject playerObject = Runner.Spawn(
//            PlayerPrefab,
//            spawnPosition,
//            Quaternion.identity,
//            player,
//            (runner, obj) =>
//            {
//                Debug.Log($"[PlayerSpawner] Đã spawn Player {player.PlayerId} tại {spawnPosition}.");
//                runner.SetPlayerObject(player, obj); // Đăng ký Player với Runner
//                Debug.Log($"[PlayerSpawner] Đã đăng ký Player {player.PlayerId} với Runner.");
//                obj.GetComponent<PlayerSetup>()?.SetupCamera();
//            }
//        );
//        Debug.Log($"[PlayerSpawner] Gửi yêu cầu spawn Player {player.PlayerId}.");
//    }

//    private void SpawnSpectator(PlayerRef player)
//    {
//        Debug.Log($"[PlayerSpawner] Player {player.PlayerId} vào muộn, spawn spectator.");

//        // Spawn một camera spectator cho người chơi vào muộn
//        if (SpectatorPrefab != null)
//        {
//            Vector3 spectatorPosition = new Vector3(0, 10, 0); // Vị trí cao để quan sát
//            NetworkObject spectatorObject = Runner.Spawn(
//                SpectatorPrefab,
//                spectatorPosition,
//                Quaternion.Euler(90, 0, 0), // Nhìn xuống
//                player
//            );

//            // Cũng đặt làm player object để tracking
//            Runner.SetPlayerObject(player, spectatorObject);
//        }
//    }
//}
using Fusion;
using UnityEngine;
using System.Collections.Generic;

public class PlayerSpwaner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public int MaxPlayers = 2; // Số người chơi tối đa
    private bool gameStarted = false;
    private HashSet<PlayerRef> activePlayers = new HashSet<PlayerRef>();

    public void PlayerJoined(PlayerRef player)
    {
        // Kiểm tra xem game đã bắt đầu chưa
        if (gameStarted)
        {
            Debug.Log("Game đã bắt đầu, người chơi mới không thể tham gia");
            return;
        }

        // Kiểm tra số lượng người chơi
        if (activePlayers.Count >= MaxPlayers)
        {
            Debug.Log("Đã đạt giới hạn người chơi tối đa");
            gameStarted = true;
            return;
        }

        // Spawn người chơi nếu là người chơi local
        if (player == Runner.LocalPlayer)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
            Runner.Spawn(PlayerPrefab, randomPosition, Quaternion.identity);
        }

        // Thêm người chơi vào danh sách
        activePlayers.Add(player);

        // Kiểm tra nếu số lượng người chơi đã đạt max thì bắt đầu game
        if (activePlayers.Count >= MaxPlayers)
        {
            gameStarted = true;
            Debug.Log("Đã đủ người chơi, game bắt đầu!");
        }
    }
}