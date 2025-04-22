////using Fusion;
////using System.Collections;
////using UnityEngine;

////public class Player : NetworkBehaviour
////{
////    public Leaderboard leaderboardManager;


////    [Networked] public NetworkString<_16> PlayerName { get; set; }
////    [Networked] public int Score { get; set; }

////    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
////    private void RPC_SetPlayerName(string name)
////    {
////        PlayerName = name;
////    }

////    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
////    public void RPC_AddScore(int points)
////    {
////        Score += points;
////    }


////    private void Start()
////    {
////        if (leaderboardManager == null)
////        {
////            leaderboardManager = FindAnyObjectByType<Leaderboard>();
////            if (leaderboardManager == null)
////            {
////                Debug.LogError("Không tìm thấy LeaderboardManager trong scene!");
////            }
////        }
////    }




////    public CharacterController controller;
////    public float speed = 5f;
////    public float gravity = -9.81f;
////    public float ForceJump = 5f;

////    private Vector3 velocity;
////    private Animator anim;
////    private Transform Cameras;

////    public GameObject Weapon;

////    [Networked] private NetworkBool IsRunning { get; set; }
////    [Networked] private Vector3 MoveDirection { get; set; }  // Đồng bộ hướng di chuyển.

////    [ Networked] private NetworkBool IsAttack { get; set; } // trạng thái tấn công.

////    private float attackCooldown = 0.5f;    // Thời gian giữa các lần tấn công.
////    private float LastAttackTime;           // Thời gian cuối cùng tấn công.



////    public override void Spawned()
////    {
////        // Gán tên người chơi khi spawn
////        if (Object.HasInputAuthority)
////        {
////            string name = $"{ Object.InputAuthority}" ; // Thay bằng tên thật nếu có
////            RPC_SetPlayerName(name);
////        }




////        Debug.Log($"[Player] Spawned. PlayerID: {Object.InputAuthority}");



////        anim = GetComponent<Animator>();
////        if (anim == null)
////        {
////            Debug.LogError("[Player] Không tìm thấy Animator!");
////        }

////        Cameras = Camera.main.transform;
////        if (Cameras == null)
////        {
////            Debug.LogError("[Player] Không tìm thấy Camera!");
////        }

////        Debug.Log($"[Player] Đã khởi tạo. HasInputAuthority: {Object.HasInputAuthority}, PlayerID: {Object.InputAuthority}");
////    }



////    public override void FixedUpdateNetwork()
////    {

////        if (!Object.HasInputAuthority) return;
////        var horizontal = Input.GetAxisRaw("Horizontal");
////        var vertical = Input.GetAxisRaw("Vertical");

////        Vector3 move = Vector3.zero;
////        if (Cameras != null)
////        {
////            Vector3 forward = Cameras.forward; // Hướng tiến của camera
////            Vector3 right = Cameras.right;     // Hướng phải của camera
////            forward.y = 0; // Bỏ thành phần Y để di chuyển trên mặt phẳng
////            right.y = 0;
////            forward.Normalize();
////            right.Normalize();

////            // Kết hợp input với hướng camera
////            move = (forward * vertical + right * horizontal).normalized;
////        }
////        if (move.magnitude > 0) // Dùng sqrMagnitude để tránh tính căn bậc 2.
////        {

////            controller.Move(move * speed * Runner.DeltaTime);
////            IsRunning = true;
////            MoveDirection = move;

////        }
////        else
////        {
////            IsRunning = false;
////            MoveDirection = Vector3.zero;
////        }

////        //xử lý nhảy
////        if (controller.isGrounded)
////        {
////            velocity.y = 0f;
////            if (Input.GetKey(KeyCode.Space))
////            {
////                velocity.y = ForceJump;
////                Debug.Log("Jump triggered!");
////            }
////        }


////        velocity.y += gravity * Runner.DeltaTime;
////        controller.Move(velocity * Runner.DeltaTime);
////    }
////    private void Update()
////    {
////        if (!Object.HasInputAuthority) return;

////        // Xử lý tấn công giống code cũ
////        if (Input.GetKey(KeyCode.F) && Runner.SimulationTime - LastAttackTime >= attackCooldown)
////        {
////            IsAttack = true; // Bật trạng thái tấn công
////            LastAttackTime = Runner.SimulationTime; // Cập nhật thời gian tấn công
////            Weapon.GetComponent<BoxCollider>().enabled = true; // Bật collider
////        }
////        else if (!Input.GetKey(KeyCode.F)) // Thả phím F
////        {
////            Weapon.GetComponent<BoxCollider>().enabled = false; // Tắt collider
////        }
////        if (Input.GetKeyDown(KeyCode.L)  && HasInputAuthority)
////        {
////            RPC_AddScore(10); // Gửi RPC để cộng điểm

////            Debug.Log($"Cộng điểm cho {PlayerName}: {Score}");
////        }
////    }



////    public override void Render()
////    {
////        // Đồng bộ animation trên tất cả client
////        anim.SetBool("run", IsRunning);

////        if (IsAttack)
////        {
////            anim.SetTrigger("attack");
////            IsAttack = false;
////        }

////        if (MoveDirection.magnitude > 0)
////        {
////            Quaternion targetRotation = Quaternion.LookRotation(MoveDirection);
////            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Runner.DeltaTime);
////        }





////    }



////}








////Phần đêm 22/4


////using Fusion;
////using System.Collections;
////using UnityEngine;
////using TMPro;

////public class Player : NetworkBehaviour
////{
////    public Leaderboard leaderboardManager;
////    public TextMeshProUGUI nicknameText; // Text hiển thị trên đầu nhân vật

////    [Networked] public NetworkString<_16> PlayerName { get; set; }
////    [Networked] public int Score { get; set; }

////    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
////    private void RPC_SetPlayerName(string name)
////    {
////        PlayerName = name;
////    }

////    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
////    public void RPC_AddScore(int points)
////    {
////        Score += points;
////    }

////    private void Start()
////    {
////        if (leaderboardManager == null)
////        {
////            leaderboardManager = FindAnyObjectByType<Leaderboard>();
////            if (leaderboardManager == null)
////            {
////                Debug.LogError("Không tìm thấy LeaderboardManager trong scene!");
////            }
////        }

////        // Đảm bảo có text để hiển thị tên trên đầu nhân vật
////        if (nicknameText == null)
////        {
////            Debug.LogWarning("nicknameText chưa được gán, tạo mới text hiển thị tên.");
////            GameObject nameTextObj = new GameObject("PlayerNameText");
////            nameTextObj.transform.SetParent(transform);
////            nameTextObj.transform.localPosition = new Vector3(0, 2f, 0); // Vị trí trên đầu nhân vật
////            nicknameText = nameTextObj.AddComponent<TextMeshProUGUI>();
////            nicknameText.alignment = TextAlignmentOptions.Center;
////            nicknameText.fontSize = 14;
////        }
////    }

////    public CharacterController controller;
////    public float speed = 5f;
////    public float gravity = -9.81f;
////    public float ForceJump = 5f;

////    private Vector3 velocity;
////    private Animator anim;
////    private Transform Cameras;

////    public GameObject Weapon;

////    [Networked] private NetworkBool IsRunning { get; set; }
////    [Networked] private Vector3 MoveDirection { get; set; }  // Đồng bộ hướng di chuyển.

////    [Networked] private NetworkBool IsAttack { get; set; } // trạng thái tấn công.

////    private float attackCooldown = 0.5f;    // Thời gian giữa các lần tấn công.
////    private float LastAttackTime;           // Thời gian cuối cùng tấn công.

////    public override void Spawned()
////    {
////        // Gán tên người chơi khi spawn
////        if (Object.HasInputAuthority)
////        {
////            // Lấy nickname từ PlayerPrefs nếu đã lưu, nếu không thì dùng ID mặc định
////            string savedName = PlayerPrefs.GetString("PlayerNickname", "");
////            string name = string.IsNullOrEmpty(savedName) ? $"Player {Object.InputAuthority}" : savedName;
////            RPC_SetPlayerName(name);
////        }

////        Debug.Log($"[Player] Spawned. PlayerID: {Object.InputAuthority}");

////        anim = GetComponent<Animator>();
////        if (anim == null)
////        {
////            Debug.LogError("[Player] Không tìm thấy Animator!");
////        }

////        Cameras = Camera.main.transform;
////        if (Cameras == null)
////        {
////            Debug.LogError("[Player] Không tìm thấy Camera!");
////        }

////        Debug.Log($"[Player] Đã khởi tạo. HasInputAuthority: {Object.HasInputAuthority}, PlayerID: {Object.InputAuthority}");
////    }

////    // Hàm công khai để UI gọi khi người chơi muốn đổi tên
////    public void ChangePlayerName(string newName)
////    {
////        if (Object.HasInputAuthority && !string.IsNullOrEmpty(newName))
////        {
////            // Lưu tên mới vào PlayerPrefs để lưu trữ giữa các phiên chơi
////            PlayerPrefs.SetString("PlayerNickname", newName);
////            PlayerPrefs.Save();

////            // Gửi RPC để thay đổi tên trên server
////            RPC_SetPlayerName(newName);
////        }
////    }

////    public override void FixedUpdateNetwork()
////    {
////        // Phần code di chuyển giữ nguyên
////        if (!Object.HasInputAuthority) return;
////        var horizontal = Input.GetAxisRaw("Horizontal");
////        var vertical = Input.GetAxisRaw("Vertical");

////        Vector3 move = Vector3.zero;
////        if (Cameras != null)
////        {
////            Vector3 forward = Cameras.forward; // Hướng tiến của camera
////            Vector3 right = Cameras.right;     // Hướng phải của camera
////            forward.y = 0; // Bỏ thành phần Y để di chuyển trên mặt phẳng
////            right.y = 0;
////            forward.Normalize();
////            right.Normalize();

////            // Kết hợp input với hướng camera
////            move = (forward * vertical + right * horizontal).normalized;
////        }
////        if (move.magnitude > 0) // Dùng sqrMagnitude để tránh tính căn bậc 2.
////        {

////            controller.Move(move * speed * Runner.DeltaTime);
////            IsRunning = true;
////            MoveDirection = move;

////        }
////        else
////        {
////            IsRunning = false;
////            MoveDirection = Vector3.zero;
////        }

////        //xử lý nhảy
////        if (controller.isGrounded)
////        {
////            velocity.y = 0f;
////            if (Input.GetKey(KeyCode.Space))
////            {
////                velocity.y = ForceJump;
////                Debug.Log("Jump triggered!");
////            }
////        }

////        velocity.y += gravity * Runner.DeltaTime;
////        controller.Move(velocity * Runner.DeltaTime);
////    }

////    private void Update()
////    {
////        if (!Object.HasInputAuthority) return;

////        // Xử lý tấn công giống code cũ
////        if (Input.GetKey(KeyCode.F) && Runner.SimulationTime - LastAttackTime >= attackCooldown)
////        {
////            IsAttack = true; // Bật trạng thái tấn công
////            LastAttackTime = Runner.SimulationTime; // Cập nhật thời gian tấn công
////            Weapon.GetComponent<BoxCollider>().enabled = true; // Bật collider
////        }
////        else if (!Input.GetKey(KeyCode.F)) // Thả phím F
////        {
////            Weapon.GetComponent<BoxCollider>().enabled = false; // Tắt collider
////        }
////        if (Input.GetKeyDown(KeyCode.L) && HasInputAuthority)
////        {
////            RPC_AddScore(10); // Gửi RPC để cộng điểm

////            Debug.Log($"Cộng điểm cho {PlayerName}: {Score}");
////        }

////        // Phím tắt để test đổi tên (có thể xóa sau khi thêm UI)
////        if (Input.GetKeyDown(KeyCode.N) && HasInputAuthority)
////        {
////            ChangePlayerName("Player_" + Random.Range(100, 999));
////        }
////    }

////    public override void Render()
////    {
////        // Đồng bộ animation trên tất cả client
////        anim.SetBool("run", IsRunning);

////        if (IsAttack)
////        {
////            anim.SetTrigger("attack");
////            IsAttack = false;
////        }

////        if (MoveDirection.magnitude > 0)
////        {
////            Quaternion targetRotation = Quaternion.LookRotation(MoveDirection);
////            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Runner.DeltaTime);
////        }

////        // Cập nhật hiển thị tên người chơi
////        if (nicknameText != null)
////        {
////            nicknameText.text = PlayerName.ToString();

////            // Đảm bảo text luôn quay về phía camera
////            if (Camera.main != null)
////            {
////                nicknameText.transform.rotation = Camera.main.transform.rotation;
////            }
////        }
////    }
////}


////Phần đêm 22/4

//using Fusion;
//using System.Collections;
//using UnityEngine;

//public class Player : NetworkBehaviour
//{
//    public Leaderboard leaderboardManager;

//    [Networked] public NetworkString<_16> PlayerName { get; set; }
//    [Networked] public int Score { get; set; }

//    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
//    private void RPC_SetPlayerName(string name)
//    {
//        PlayerName = name;
//    }

//    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
//    public void RPC_AddScore(int points)
//    {
//        Score += points;
//    }

//    // Hàm public để đổi tên người chơi (gọi từ UI)
//    public void ChangePlayerName(string newName)
//    {
//        if (Object.HasInputAuthority && !string.IsNullOrEmpty(newName))
//        {
//            // Lưu tên vào PlayerPrefs
//            PlayerPrefs.SetString("PlayerNickname", newName);
//            PlayerPrefs.Save();

//            // Gửi RPC để cập nhật tên trên server
//            RPC_SetPlayerName(newName);
//        }
//    }

//    private void Start()
//    {
//        if (leaderboardManager == null)
//        {
//            leaderboardManager = FindAnyObjectByType<Leaderboard>();
//            if (leaderboardManager == null)
//            {
//                Debug.LogError("Không tìm thấy LeaderboardManager trong scene!");
//            }
//        }
//    }

//    public CharacterController controller;
//    public float speed = 5f;
//    public float gravity = -9.81f;
//    public float ForceJump = 5f;

//    private Vector3 velocity;
//    private Animator anim;
//    private Transform Cameras;

//    public GameObject Weapon;

//    [Networked] private NetworkBool IsRunning { get; set; }
//    [Networked] private Vector3 MoveDirection { get; set; }  // Đồng bộ hướng di chuyển.

//    [Networked] private NetworkBool IsAttack { get; set; } // trạng thái tấn công.

//    private float attackCooldown = 0.5f;    // Thời gian giữa các lần tấn công.
//    private float LastAttackTime;           // Thời gian cuối cùng tấn công.

//    public override void Spawned()
//    {
//        // Gán tên người chơi khi spawn
//        if (Object.HasInputAuthority)
//        {
//            string savedName = PlayerPrefs.GetString("PlayerNickname", "");
//            string name = string.IsNullOrEmpty(savedName)
//                ? $"Player_{Object.InputAuthority}"
//                : savedName;
//            RPC_SetPlayerName(name);
//            Debug.Log($"[Player] Set name to: {name}");
//        }

//        Debug.Log($"[Player] Spawned. PlayerID: {Object.InputAuthority}");

//        anim = GetComponent<Animator>();
//        if (anim == null)
//        {
//            Debug.LogError("[Player] Không tìm thấy Animator!");
//        }

//        Cameras = Camera.main.transform;
//        if (Cameras == null)
//        {
//            Debug.LogError("[Player] Không tìm thấy Camera!");
//        }

//        Debug.Log($"[Player] Đã khởi tạo. HasInputAuthority: {Object.HasInputAuthority}, PlayerID: {Object.InputAuthority}");
//    }

//    public override void FixedUpdateNetwork()
//    {
//        if (!Object.HasInputAuthority) return;
//        var horizontal = Input.GetAxisRaw("Horizontal");
//        var vertical = Input.GetAxisRaw("Vertical");

//        Vector3 move = Vector3.zero;
//        if (Cameras != null)
//        {
//            Vector3 forward = Cameras.forward; // Hướng tiến của camera
//            Vector3 right = Cameras.right;     // Hướng phải của camera
//            forward.y = 0; // Bỏ thành phần Y để di chuyển trên mặt phẳng
//            right.y = 0;
//            forward.Normalize();
//            right.Normalize();

//            // Kết hợp input với hướng camera
//            move = (forward * vertical + right * horizontal).normalized;
//        }
//        if (move.magnitude > 0) // Dùng sqrMagnitude để tránh tính căn bậc 2.
//        {

//            controller.Move(move * speed * Runner.DeltaTime);
//            IsRunning = true;
//            MoveDirection = move;

//        }
//        else
//        {
//            IsRunning = false;
//            MoveDirection = Vector3.zero;
//        }

//        //xử lý nhảy
//        if (controller.isGrounded)
//        {
//            velocity.y = 0f;
//            if (Input.GetKey(KeyCode.Space))
//            {
//                velocity.y = ForceJump;
//                Debug.Log("Jump triggered!");
//            }
//        }

//        velocity.y += gravity * Runner.DeltaTime;
//        controller.Move(velocity * Runner.DeltaTime);
//    }

//    private void Update()
//    {
//        if (!Object.HasInputAuthority) return;

//        // Xử lý tấn công giống code cũ
//        if (Input.GetKey(KeyCode.F) && Runner.SimulationTime - LastAttackTime >= attackCooldown)
//        {
//            IsAttack = true; // Bật trạng thái tấn công
//            LastAttackTime = Runner.SimulationTime; // Cập nhật thời gian tấn công
//            Weapon.GetComponent<BoxCollider>().enabled = true; // Bật collider
//        }
//        else if (!Input.GetKey(KeyCode.F)) // Thả phím F
//        {
//            Weapon.GetComponent<BoxCollider>().enabled = false; // Tắt collider
//        }
//        if (Input.GetKeyDown(KeyCode.L) && HasInputAuthority)
//        {
//            RPC_AddScore(10); // Gửi RPC để cộng điểm

//            Debug.Log($"Cộng điểm cho {PlayerName}: {Score}");
//        }

//        // Test đổi tên với phím N
//        if (Input.GetKeyDown(KeyCode.N) && HasInputAuthority)
//        {
//            string randomName = "Player_" + Random.Range(100, 999);
//            ChangePlayerName(randomName);
//            Debug.Log($"Đã đổi tên thành: {randomName}");
//        }
//    }

//    public override void Render()
//    {
//        // Đồng bộ animation trên tất cả client
//        anim.SetBool("run", IsRunning);

//        if (IsAttack)
//        {
//            anim.SetTrigger("attack");
//            IsAttack = false;
//        }

//        if (MoveDirection.magnitude > 0)
//        {
//            Quaternion targetRotation = Quaternion.LookRotation(MoveDirection);
//            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Runner.DeltaTime);
//        }
//    }
//}
using Fusion;
using System.Collections;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public Leaderboard leaderboardManager;

    [Networked] public NetworkString<_16> PlayerName { get; set; }
    [Networked] public int Score { get; set; }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_SetPlayerName(string name)
    {
        PlayerName = name;
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_AddScore(int points)
    {
        Score += points;
    }

    public void ChangePlayerName(string newName)
    {
        if (Object.HasInputAuthority && !string.IsNullOrEmpty(newName))
        {
            PlayerPrefs.SetString("PlayerNickname", newName);
            PlayerPrefs.Save();
            RPC_SetPlayerName(newName);
        }
    }

    private void Start()
    {
        if (leaderboardManager == null)
        {
            leaderboardManager = FindAnyObjectByType<Leaderboard>();
            if (leaderboardManager == null)
            {
                Debug.LogError("Không tìm thấy LeaderboardManager trong scene!");
            }
        }
    }

    public CharacterController controller;
    public float speed = 5f;
    public float gravity = -9.81f;
    public float ForceJump = 5f;

    private Vector3 velocity;
    private Animator anim;
    private Transform Cameras;

    public GameObject Weapon;

    [Networked] private NetworkBool IsRunning { get; set; }
    [Networked] private Vector3 MoveDirection { get; set; }
    [Networked] private TickTimer AttackTriggerTick { get; set; } // ⬅️ đồng bộ thời điểm tấn công

    private float attackCooldown = 0.5f;
    private float LastAttackTime;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            string savedName = PlayerPrefs.GetString("PlayerNickname", "");
            string name = string.IsNullOrEmpty(savedName)
                ? $"Player_{Object.InputAuthority}"
                : savedName;
            RPC_SetPlayerName(name);
        }

        anim = GetComponent<Animator>();
        Cameras = Camera.main?.transform;
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasInputAuthority) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 move = Vector3.zero;
        if (Cameras != null)
        {
            Vector3 forward = Cameras.forward;
            Vector3 right = Cameras.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            move = (forward * vertical + right * horizontal).normalized;
        }

        if (move.magnitude > 0)
        {
            controller.Move(move * speed * Runner.DeltaTime);
            IsRunning = true;
            MoveDirection = move;
        }
        else
        {
            IsRunning = false;
            MoveDirection = Vector3.zero;
        }

        // Nhảy
        if (controller.isGrounded)
        {
            velocity.y = 0f;
            if (Input.GetKey(KeyCode.Space))
            {
                velocity.y = ForceJump;
            }
        }
        velocity.y += gravity * Runner.DeltaTime;
        controller.Move(velocity * Runner.DeltaTime);

        // Tấn công
        if (Input.GetKey(KeyCode.F) && Runner.SimulationTime - LastAttackTime >= attackCooldown)
        {
            LastAttackTime = Runner.SimulationTime;

            AttackTriggerTick = TickTimer.CreateFromTicks(Runner, Runner.Tick); // ⬅️ Ghi thời điểm tấn công

            Weapon.GetComponent<BoxCollider>().enabled = true;
        }
        else if (!Input.GetKey(KeyCode.F))
        {
            Weapon.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void Update()
    {
        if (!Object.HasInputAuthority) return;

        if (Input.GetKeyDown(KeyCode.L))
        {
            RPC_AddScore(10);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            string randomName = "Player_" + Random.Range(100, 999);
            ChangePlayerName(randomName);
        }
    }

    public override void Render()
    {
        if (anim == null) return;

        anim.SetBool("run", IsRunning);

        // ⬇️ Đồng bộ animation "attack" đúng lúc
        if (AttackTriggerTick.IsRunning &&
            !AttackTriggerTick.Expired(Runner) &&
            AttackTriggerTick.RemainingTime(Runner) <= Runner.DeltaTime)
        {
            anim.SetTrigger("attack");
        }

        // Xoay hướng nhân vật
        if (MoveDirection.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(MoveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Runner.DeltaTime);
        }
    }
}
