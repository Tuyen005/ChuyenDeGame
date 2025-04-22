////////using Fusion;
////////using UnityEngine;
////////using UnityEngine.UI;

////////public class HealthPlayer : NetworkBehaviour
////////{
////////    //[Networked, OnChangedRender(nameof(OnHealthChanged))]

////////    //public int Health { get; set; } = 100;

////////    //public Slider healthSlider;

////////    //private void Awake()
////////    //{
////////    //    healthSlider = GetComponentInChildren<Slider>();
////////    //}

////////    //public override void Spawned()
////////    //{
////////    //    if (HasStateAuthority)
////////    //    {
////////    //        Health = 100;
////////    //    }
////////    //    UpdateHealthUI();
////////    //}

////////    //private void OnHealthChanged()
////////    //{
////////    //    UpdateHealthUI();

////////    //    if (Health <= 0 && HasStateAuthority)
////////    //    {
////////    //        Debug.Log($"[HealthPlayer] Player {Object.InputAuthority.PlayerId} đã chết!");
////////    //        Runner.Despawn(Object);
////////    //    }
////////    //}

////////    //public void TakeDamage(int damage)
////////    //{
////////    //    if (!HasStateAuthority) return; // Chỉ máy chủ mới có thể cập nhật máu
////////    //    Health = Mathf.Max(Health - damage, 0); // Đảm bảo không xuống dưới 0
////////    //    Debug.Log($"[HealthPlayer] Player {Object.InputAuthority.PlayerId} bị trừ {damage} máu. Còn lại: {Health}");
////////    //}

////////    //private void UpdateHealthUI()
////////    //{
////////    //    if (healthSlider)
////////    //    {
////////    //        healthSlider.maxValue = 100;
////////    //        healthSlider.value = Health;
////////    //    }
////////    //}



////////    [Networked, OnChangedRender(nameof(OnHealthChanged))]
////////    public int Health { get; set; } = 100;

////////    public Slider healthSlider;

////////    public override void Spawned()
////////    {
////////        if (Object.HasStateAuthority)
////////        {
////////            Health = 100; // Khởi tạo máu ban đầu
////////        }

////////        healthSlider = GetComponentInChildren<Slider>(); // Tự động tìm thanh máu
////////        if (healthSlider == null)
////////        {
////////            Debug.LogError("Không tìm thấy Slider trong Player!");
////////        }

////////        UpdateHealthBar(); // Cập nhật thanh máu khi spawn
////////    }

////////    private void OnHealthChanged()
////////    {
////////        UpdateHealthBar(); // Gọi hàm cập nhật thanh máu khi máu thay đổi
////////        Debug.Log($"Health changed to {Health}");
////////    }

////////    private void UpdateHealthBar()
////////    {
////////        if (healthSlider != null)
////////        {
////////            healthSlider.value = Health;
////////        }
////////        Debug.Log($"Máu thay đổi: {Health}");
////////    }

////////    public void TakeDamage(int damage)
////////    {
////////        if (Object.HasStateAuthority)
////////        {
////////            Health = Mathf.Max(0, Health - damage);
////////            Debug.Log($"Nhận {damage} sát thương, máu còn lại: {Health}");
////////        }
////////    }

























////////    //[Networked, OnChangedRender(nameof(OnHealthChanged))]
////////    //public int Health { get; set; }
////////    //public Slider healthSlider;
////////    //public Slider healthSlider1;

////////    //private void OnHealthChanged()
////////    //{
////////    //    healthSlider.value = Health;
////////    //    healthSlider1.value = Health;

////////    //    Debug.Log($"[HealthPlayer] Máu của {Object.InputAuthority.PlayerId} thay đổi: {Health}");
////////    //}

////////    //public override void Spawned()
////////    //{

////////    //    healthSlider.value = Health;
////////    //    healthSlider1.value = Health;
////////    //    if (!Object.HasInputAuthority)
////////    //    {
////////    //        healthSlider1.gameObject.SetActive(false);
////////    //        gameObject.CompareTag("Target");
////////    //    }
////////    //    else
////////    //    {
////////    //        healthSlider.gameObject.SetActive(false);
////////    //        gameObject.CompareTag("Player");
////////    //    }
////////    //}

////////    //[Rpc(RpcSources.All, RpcTargets.StateAuthority)]

////////    //public void TakeDamage(int damage)
////////    //{
////////    //    if (!Object.HasInputAuthority) return;

////////    //    Health -= damage;
////////    //    Debug.Log($"Health : {Health}");

////////    //    if (Health <= 0)
////////    //    {
////////    //        Debug.Log($"[HealthPlayer] Player {Object.InputAuthority.PlayerId} đã chết!");
////////    //        Runner.Despawn(Object);
////////    //    }
////////    //}
////////    //public void Rpc_TakeDamage(int damage)
////////    //{
////////    //    TakeDamage(damage);
////////    //}








































////////    //private int Health { get; set; } = 100; // Khởi tạo máu ban đầu

////////    //public Slider healthSlider;

////////    //private void Awake()
////////    //{
////////    //    // Tìm slider trong prefab
////////    //    healthSlider = GetComponentInChildren<Slider>();
////////    //    if (healthSlider != null)
////////    //    {
////////    //        healthSlider.maxValue = 100; // Đặt giá trị tối đa cho slider
////////    //        healthSlider.value = Health; // Cập nhật giá trị ban đầu
////////    //    }
////////    //}

////////    //public override void Spawned()
////////    //{
////////    //    // Khởi tạo giá trị Health khi object được spawn
////////    //    if (HasStateAuthority)
////////    //    {
////////    //        Health = 100; // Đảm bảo server đặt giá trị ban đầu
////////    //    }
////////    //}

////////    //private void OnHealthChanged()
////////    //{
////////    //    // Cập nhật slider trên client sở hữu nhân vật
////////    //    if (Object.HasInputAuthority && healthSlider != null)
////////    //    {
////////    //        healthSlider.value = Health;
////////    //    }

////////    //    // Log để debug
////////    //    Debug.Log($"Health changed to {Health} on client {Runner.LocalPlayer.PlayerId}");

////////    //    // Kiểm tra chết
////////    //    if (Health <= 0 && HasStateAuthority)
////////    //    {
////////    //        Debug.Log("Player is dead");
////////    //        Runner.Despawn(Object); // Xóa nhân vật khỏi mạng
////////    //    }
////////    //}

////////    ////public void TakeDamage(int damage)
////////    ////{
////////    ////    if (HasStateAuthority) // Chỉ server cập nhật máu
////////    ////    {
////////    ////        Health = Mathf.Max(0, Health - damage);
////////    ////        Debug.Log("Player take damage: " + damage + " - Health: " + Health);
////////    ////    }
////////    ////}

////////    //void Update()
////////    //{
////////    //    // Dùng để test: nhấn G để tự trừ máu
////////    //    if (HasStateAuthority && Object.HasInputAuthority && Input.GetKeyDown(KeyCode.G))
////////    //    {
////////    //        TakeDamage(10);
////////    //    }
////////    //}
////////    //[Networked] public int currentHealth { get; set; } = 100; // Máu hiện tại của Player
////////    //private int maxHealth = 100;

////////    //public void TakeDamage(int damage)
////////    //{
////////    //    if (!HasStateAuthority) return; // Chỉ máy chủ (host) mới có quyền trừ máu

////////    //    currentHealth -= damage;
////////    //    Debug.Log($"Player {Object.InputAuthority.PlayerId} bị đánh! Máu còn lại: {currentHealth}");

////////    //    if (currentHealth <= 0)
////////    //    {
////////    //        Die();
////////    //    }
////////    //}

////////    //private void Die()
////////    //{
////////    //    Debug.Log($"Player {Object.InputAuthority.PlayerId} đã chết!");
////////    //    Runner.Despawn(Object); // Hủy nhân vật khi chết (có thể thay thế bằng hiệu ứng chết)
////////    //}




////////    //[Networked]
////////    //public int Health { get; set; } = 100;

////////    //public Slider healthSlider;

////////    //public override void Spawned()
////////    //{
////////    //    if (HasStateAuthority)
////////    //    {
////////    //        Health = 100;
////////    //        Debug.Log($"[HealthPlayer] Server khởi tạo máu cho {gameObject.name}: {Health}");
////////    //    }

////////    //    healthSlider = GetComponentInChildren<Slider>();
////////    //    if (healthSlider != null)
////////    //    {
////////    //        healthSlider.maxValue = 100;
////////    //        healthSlider.value = Health;
////////    //        Debug.Log($"[HealthPlayer] Đã khởi tạo Slider cho {gameObject.name}. Giá trị ban đầu: {Health}");
////////    //    }
////////    //    else
////////    //    {
////////    //        Debug.LogError("[HealthPlayer] Không tìm thấy Slider trong prefab!");
////////    //    }
////////    //}

////////    //public void TakeDamage(int damage)
////////    //{
////////    //    if (!HasStateAuthority) return;

////////    //    Debug.Log($"[HealthPlayer] Trước khi trừ: {gameObject.name} có {Health} máu.");
////////    //    Health = Mathf.Max(0, Health - damage);
////////    //    Debug.Log($"[HealthPlayer] Sau khi trừ: {gameObject.name} bị trừ {damage} máu. Máu còn lại: {Health}");

////////    //    Rpc_UpdateHealth(Health); // Gửi RPC để cập nhật UI trên tất cả client

////////    //    if (Health <= 0)
////////    //    {
////////    //        Debug.Log($"[HealthPlayer] {gameObject.name} đã chết!");
////////    //        Runner.Despawn(Object);
////////    //    }
////////    //}

////////    //[Rpc(RpcSources.StateAuthority, RpcTargets.All)]
////////    //public void Rpc_UpdateHealth(int newHealth)
////////    //{
////////    //    Health = newHealth;
////////    //    if (healthSlider != null)
////////    //    {
////////    //        healthSlider.value = Health;
////////    //        Debug.Log($"[HealthPlayer] RPC cập nhật Slider cho {gameObject.name}: {Health}");
////////    //    }
////////    //}

////////    //public override void Render()
////////    //{
////////    //    Debug.Log($"[HealthPlayer] Render gọi cho {gameObject.name}. HasInputAuthority: {Object.HasInputAuthority}");
////////    //    if (healthSlider != null)
////////    //    {
////////    //        healthSlider.value = Health;
////////    //        Debug.Log($"[HealthPlayer] Cập nhật Slider cho {gameObject.name}. Máu hiện tại: {Health}");
////////    //    }
////////    //    else
////////    //    {
////////    //        Debug.LogWarning($"[HealthPlayer] Slider là null cho {gameObject.name}");
////////    //    }
////////    //}
////////    //void Update()
////////    //{
////////    //    if (Input.GetKeyDown(KeyCode.H))
////////    //    {
////////    //        Debug.Log($"[HealthPlayer] Máu hiện tại của {gameObject.name}: {Health}");
////////    //    }
////////    //}
////////}

//////using Fusion;
//////using UnityEngine;
//////using UnityEngine.UI;

//////public class HealthPlayer : NetworkBehaviour
//////{
//////    [Networked, OnChangedRender(nameof(OnHealthChanged))]
//////    public int Health { get; set; } = 100;

//////    public Slider healthSlider;

//////    public override void Spawned()
//////    {
//////        if (Object.HasStateAuthority)
//////        {
//////            Health = 100;
//////        }

//////        healthSlider = GetComponentInChildren<Slider>();
//////        if (healthSlider == null)
//////        {
//////            Debug.LogError("Không tìm thấy Slider trong Player!");
//////        }

//////        UpdateHealthBar();

//////        if (Object.HasInputAuthority)
//////        {
//////            GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
//////            if (camObj != null)
//////            {
//////                var camFollow = camObj.GetComponent<CameraFollow>();
//////                if (camFollow != null)
//////                {
//////                    camFollow.AssignCamera(this.transform);
//////                    Debug.Log("Camera đã gán cho người chơi local.");
//////                }
//////            }
//////        }
//////    }

//////    private void OnHealthChanged()
//////    {
//////        UpdateHealthBar();
//////        Debug.Log($"[HealthPlayer] Health mới: {Health}");
//////    }

//////    private void UpdateHealthBar()
//////    {
//////        if (healthSlider != null)
//////        {
//////            healthSlider.value = Health;
//////        }
//////    }

//////    public void TakeDamage(int damage)
//////    {
//////        if (!Object.HasStateAuthority) return;

//////        Health = Mathf.Max(0, Health - damage);
//////        Debug.Log($"[HealthPlayer] Nhận {damage} sát thương, máu còn lại: {Health}");

//////        if (Health <= 0)
//////        {
//////            Die();
//////        }
//////    }

//////    private void Die()
//////    {
//////        //Debug.Log($"[HealthPlayer] {Object.InputAuthority} đã chết.");

//////        //// Nếu là người chơi local, camera sẽ tự động chuyển trong CameraFollow
//////        //if (Object.HasStateAuthority)
//////        //{
//////        //    Runner.Despawn(Object); // Xóa nhân vật khỏi mạng
//////        //}
//////        Debug.Log($"[Die] {Object.InputAuthority} đã chết.");

//////        // Nếu là người chơi local → tìm người chơi khác để camera theo
//////        if (Object.HasInputAuthority)
//////        {
//////            FollowAnotherAlivePlayer();
//////        }

//////        // Chỉ người có StateAuthority mới được phép despawn
//////        if (Object.HasStateAuthority)
//////        {
//////            Runner.Despawn(Object);
//////        }


//////    }


//////    private void FollowAnotherAlivePlayer()
//////    {
//////        var camObj = GameObject.FindGameObjectWithTag("MainCamera");
//////        if (camObj == null) return;

//////        var camFollow = camObj.GetComponent<CameraFollow>();
//////        if (camFollow == null) return;

//////#if UNITY_2023_1_OR_NEWER
//////        var allPlayers = FindObjectsByType<HealthPlayer>(FindObjectsSortMode.None);
//////#else
//////        var allPlayers = FindObjectsOfType<HealthPlayer>();
//////#endif

//////        foreach (var p in allPlayers)
//////        {
//////            if (p != this && p.Health > 0)
//////            {
//////                camFollow.AssignCamera(p.transform);
//////                Debug.Log("Camera đã chuyển sang người chơi còn sống.");
//////                return;
//////            }
//////        }

//////        Debug.Log("Không còn người chơi sống để camera theo.");
//////    }









//////}



//////using Fusion;
////using Fusion;
////using UnityEngine;
////using UnityEngine.UI;
////using static Unity.Collections.Unicode;

////public class HealthPlayer : NetworkBehaviour
////{
////    [Networked, OnChangedRender(nameof(OnHealthChanged))]
////    public int Health { get; set; } = 100;

////    public Slider healthSlider;



////    // Thêm reference để debug
////    [SerializeField] private GameObject debugTextPrefab;
////    private GameObject debugTextObj;

////    public override void Spawned()
////    {
////        Debug.Log($"[HealthPlayer] {Object.Name} Spawned, HasStateAuthority: {Object.HasStateAuthority}");

////        if (Object.HasStateAuthority)
////        {
////            Health = 100;
////            Debug.Log($"[HealthPlayer] Đặt máu ban đầu = 100 cho {Object.Name}");
////        }

////        healthSlider = GetComponentInChildren<Slider>();
////        if (healthSlider == null)
////        {
////            Debug.LogError("[HealthPlayer] Không tìm thấy Slider trong Player!");
////        }
////        else
////        {
////            // Hiển thị slider cho tất cả người chơi, nhưng chỉ cập nhật cho local player
////            healthSlider.maxValue = 100;
////            healthSlider.value = Health;
////            Debug.Log($"[HealthPlayer] Khởi tạo thanh máu: {Health}/100");
////        }

////        UpdateHealthBar();

////        if (Object.HasInputAuthority)
////        {
////            Debug.Log("[HealthPlayer] Người chơi local, tìm và gán camera");
////            GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
////            if (camObj != null)
////            {
////                var camFollow = camObj.GetComponent<CameraFollow>();
////                if (camFollow != null)
////                {
////                    camFollow.AssignCamera(this.transform);
////                    Debug.Log("[HealthPlayer] Camera đã gán cho người chơi local.");
////                }
////                else
////                {
////                    Debug.LogError("[HealthPlayer] Không tìm thấy CameraFollow trên MainCamera!");
////                }
////            }
////            else
////            {
////                Debug.LogError("[HealthPlayer] Không tìm thấy MainCamera!");
////            }
////        }

////        // Tạo text debug trên đầu nhân vật
////        CreateDebugText();
////    }

////    void CreateDebugText()
////    {
////        if (debugTextPrefab != null)
////        {
////            debugTextObj = Instantiate(debugTextPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
////            debugTextObj.transform.SetParent(transform);
////            var textComponent = debugTextObj.GetComponent<TextMesh>();
////            if (textComponent != null)
////            {
////                textComponent.text = $"{Object.Name}\nHP: {Health}";
////            }
////        }
////    }

////    private void OnHealthChanged()
////    {
////        UpdateHealthBar();
////        Debug.Log($"[HealthPlayer] {Object.Name} - Health mới: {Health}");

////        // Cập nhật text debug
////        if (debugTextObj != null)
////        {
////            var textComponent = debugTextObj.GetComponent<TextMesh>();
////            if (textComponent != null)
////            {
////                textComponent.text = $"{Object.Name}\nHP: {Health}";
////            }
////        }
////    }

////    private void UpdateHealthBar()
////    {
////        if (healthSlider != null)
////        {
////            healthSlider.value = Health;
////            Debug.Log($"[HealthPlayer] Cập nhật thanh máu: {Health}/100");
////        }
////    }

////    // Thêm kiểm tra thêm và debug chi tiết
////    public void TakeDamage(int damage)
////    {
////        Debug.Log($"[HealthPlayer] {Object.Name} - TakeDamage({damage}) được gọi, HasStateAuthority: {Object.HasStateAuthority}");

////        if (!Object.HasStateAuthority)
////        {
////            Debug.LogWarning($"[HealthPlayer] {Object.Name} - Không có StateAuthority, bỏ qua TakeDamage");
////            return;
////        }

////        int oldHealth = Health;
////        Health = Mathf.Max(0, Health - damage);

////        Debug.Log($"[HealthPlayer] {Object.Name} - TakeDamage: {oldHealth} -> {Health} (trừ {damage})");

////        if (Health <= 0)
////        {
////            Debug.Log($"[HealthPlayer] {Object.Name} - Health = 0, gọi Die()");
////            Die();
////        }
////    }

////    private void Die()
////    {
////        Debug.Log($"[HealthPlayer] {Object.Name} đã chết, InputAuthority: {Object.InputAuthority}");

////        // Nếu là người chơi local → tìm người chơi khác để camera theo
////        if (Object.HasInputAuthority)
////        {
////            FollowAnotherAlivePlayer();
////        }

////        // Chỉ người có StateAuthority mới được phép despawn
////        if (Object.HasStateAuthority)
////        {
////            Debug.Log($"[HealthPlayer] Despawn {Object.Name}");
////            Runner.Despawn(Object);

////        }
////        ShowGameOver();
////    }

////    private void ShowGameOver()
////    {
////        if (GameManager._instance != null)
////        {
////            GameManager._instance.ShowGameOver();
////        }
////    }
////    private void FollowAnotherAlivePlayer()
////    {
////        var camObj = GameObject.FindGameObjectWithTag("MainCamera");
////        if (camObj == null) return;

////        var camFollow = camObj.GetComponent<CameraFollow>();
////        if (camFollow == null) return;

////#if UNITY_2023_1_OR_NEWER
////        var allPlayers = FindObjectsByType<HealthPlayer>(FindObjectsSortMode.None);
////#else
////        var allPlayers = FindObjectsOfType<HealthPlayer>();
////#endif

////        foreach (var p in allPlayers)
////        {
////            if (p != this && p.Health > 0)
////            {
////                camFollow.AssignCamera(p.transform);
////                Debug.Log($"[HealthPlayer] Camera đã chuyển sang người chơi {p.name}");
////                return;
////            }
////        }

////        Debug.Log("[HealthPlayer] Không còn người chơi sống để camera theo.");
////    }

////    // Thêm phương thức test vào Inspector
////    [ContextMenu("Test Take Damage")]
////    void InspectorTestDamage()
////    {
////        Debug.Log($"[TEST] Gọi TakeDamage(10) trên {gameObject.name}");
////        TakeDamage(10);
////    }
////    private void Update()
////    {
////        if (Input.GetKeyDown(KeyCode.Q))
////        {
////            TakeDamage(100);
////        }
////    }

////}

//using Fusion;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.UI;

//public class HealthPlayer : NetworkBehaviour
//{
//    [Networked, OnChangedRender(nameof(OnHealthChanged))]
//    public int Health { get; set; } = 100;

//    [Networked]
//    public int PlayerNumber { get; set; }

//    [SerializeField] private Slider healthSlider;
//    [SerializeField] private Vector3 healthBarOffset = new Vector3(3f, 20f, 25f); // Vị trí trên đầu nhân vật
//    [SerializeField] private GameObject debugTextPrefab;
//    private GameObject debugTextObj;

//    public override void Spawned()
//    {
//        Debug.Log($"[HealthPlayer] {Object.Name} Spawned, HasStateAuthority: {Object.HasStateAuthority}");

//        if (Object.HasStateAuthority)
//        {
//            Health = 100;
//            // Gán PlayerNumber cho người chơi để phân biệt các nhân vật
//            PlayerNumber = Runner.ActivePlayers.Count();
//            Debug.Log($"[HealthPlayer] Đặt máu ban đầu = 100 và PlayerNumber = {PlayerNumber} cho {Object.Name}");
//        }

//        // Tìm và thiết lập healthSlider
//        healthSlider = GetComponentInChildren<Slider>();
//        if (healthSlider == null)
//        {
//            Debug.LogError("[HealthPlayer] Không tìm thấy Slider trong Player!");
//        }
//        else
//        {
//            // Thiết lập World Space Canvas cho thanh máu
//            SetupHealthBarCanvas();

//            // Khởi tạo giá trị thanh máu
//            healthSlider.maxValue = 100;
//            healthSlider.value = Health;
//            Debug.Log($"[HealthPlayer] Khởi tạo thanh máu: {Health}/100");
//        }

//        UpdateHealthBar();

//        // Thiết lập camera cho người chơi local
//        if (Object.HasInputAuthority)
//        {
//            Debug.Log("[HealthPlayer] Người chơi local, tìm và gán camera");
//            GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
//            if (camObj != null)
//            {
//                var camFollow = camObj.GetComponent<CameraFollow>();
//                if (camFollow != null)
//                {
//                    camFollow.AssignCamera(this.transform);
//                    Debug.Log("[HealthPlayer] Camera đã gán cho người chơi local.");
//                }
//                else
//                {
//                    Debug.LogError("[HealthPlayer] Không tìm thấy CameraFollow trên MainCamera!");
//                }
//            }
//            else
//            {
//                Debug.LogError("[HealthPlayer] Không tìm thấy MainCamera!");
//            }
//        }

//        // Tạo text debug trên đầu nhân vật
//        CreateDebugText();
//    }

//    private void SetupHealthBarCanvas()
//    {
//        Canvas canvas = healthSlider.GetComponentInParent<Canvas>();
//        if (canvas != null)
//        {
//            // Đảm bảo canvas là World Space để hiển thị trong không gian 3D
//            canvas.renderMode = RenderMode.WorldSpace;

//            // Đặt canvas làm con của nhân vật và di chuyển lên trên đầu
//            canvas.transform.SetParent(transform);
//            canvas.transform.localPosition = healthBarOffset;
//            canvas.transform.localRotation = Quaternion.identity;

//            // Điều chỉnh kích thước canvas phù hợp
//            canvas.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

//            // Đảm bảo thanh máu nằm trên cùng và không bị che khuất
//            Canvas.ForceUpdateCanvases();

//            Debug.Log($"[HealthPlayer] Đã thiết lập World Space Canvas cho {Object.Name}");
//        }
//    }

//    void CreateDebugText()
//    {
//        if (debugTextPrefab != null)
//        {
//            debugTextObj = Instantiate(debugTextPrefab, transform.position + Vector3.up * 2.5f, Quaternion.identity);
//            debugTextObj.transform.SetParent(transform);
//            var textComponent = debugTextObj.GetComponent<TextMesh>();
//            if (textComponent != null)
//            {
//                textComponent.text = $"{Object.Name}\nHP: {Health}\nPlayer #{PlayerNumber}";
//            }
//        }
//    }

//    private void OnHealthChanged()
//    {
//        UpdateHealthBar();
//        Debug.Log($"[HealthPlayer] {Object.Name} - Health mới: {Health}");

//        // Cập nhật text debug
//        if (debugTextObj != null)
//        {
//            var textComponent = debugTextObj.GetComponent<TextMesh>();
//            if (textComponent != null)
//            {
//                textComponent.text = $"{Object.Name}\nHP: {Health}\nPlayer #{PlayerNumber}";
//            }
//        }
//    }

//    private void UpdateHealthBar()
//    {
//        if (healthSlider != null)
//        {
//            healthSlider.value = Health;
//            Debug.Log($"[HealthPlayer] Cập nhật thanh máu: {Health}/100");
//        }
//    }

//    public void TakeDamage(int damage)
//    {
//        Debug.Log($"[HealthPlayer] {Object.Name} - TakeDamage({damage}) được gọi, HasStateAuthority: {Object.HasStateAuthority}");

//        if (!Object.HasStateAuthority)
//        {
//            Debug.LogWarning($"[HealthPlayer] {Object.Name} - Không có StateAuthority, bỏ qua TakeDamage");
//            return;
//        }

//        int oldHealth = Health;
//        Health = Mathf.Max(0, Health - damage);

//        Debug.Log($"[HealthPlayer] {Object.Name} - TakeDamage: {oldHealth} -> {Health} (trừ {damage})");

//        if (Health <= 0)
//        {
//            Debug.Log($"[HealthPlayer] {Object.Name} - Health = 0, gọi Die()");
//            Die();
//        }
//    }

//    private void Die()
//    {
//        Debug.Log($"[HealthPlayer] {Object.Name} đã chết, InputAuthority: {Object.InputAuthority}");

//        // Nếu là người chơi local → tìm người chơi khác để camera theo
//        if (Object.HasInputAuthority)
//        {
//            FollowAnotherAlivePlayer();
//        }

//        // Chỉ người có StateAuthority mới được phép despawn
//        if (Object.HasStateAuthority)
//        {
//            Debug.Log($"[HealthPlayer] Despawn {Object.Name}");
//            Runner.Despawn(Object);
//        }

//       // ShowGameOver();
//    }

//    //private void ShowGameOver()
//    //{
//    //    if (GameManager._instance != null)
//    //    {
//    //        GameManager._instance.ShowGameOver();
//    //    }
//    //}

//    private void FollowAnotherAlivePlayer()
//    {
//        var camObj = GameObject.FindGameObjectWithTag("MainCamera");
//        if (camObj == null) return;

//        var camFollow = camObj.GetComponent<CameraFollow>();
//        if (camFollow == null) return;

//#if UNITY_2023_1_OR_NEWER
//        var allPlayers = FindObjectsByType<HealthPlayer>(FindObjectsSortMode.None);
//#else
//        var allPlayers = FindObjectsOfType<HealthPlayer>();
//#endif

//        foreach (var p in allPlayers)
//        {
//            if (p != this && p.Health > 0)
//            {
//                camFollow.AssignCamera(p.transform);
//                Debug.Log($"[HealthPlayer] Camera đã chuyển sang người chơi {p.name}");
//                return;
//            }
//        }

//        Debug.Log("[HealthPlayer] Không còn người chơi sống để camera theo.");
//    }

//    // LateUpdate để đảm bảo thanh máu luôn quay về phía camera
//    private void LateUpdate()
//    {
//        if (healthSlider != null)
//        {
//            Canvas canvas = healthSlider.GetComponentInParent<Canvas>();
//            if (canvas != null && canvas.renderMode == RenderMode.WorldSpace && Camera.main != null)
//            {
//                // Quay thanh máu về phía camera
//                canvas.transform.LookAt(Camera.main.transform);
//                canvas.transform.Rotate(0, 180, 0); // Xoay để đối diện camera
//            }
//        }
//    }

//    // Phương thức test trong Inspector
//    [ContextMenu("Test Take Damage")]
//    void InspectorTestDamage()
//    {
//        Debug.Log($"[TEST] Gọi TakeDamage(10) trên {gameObject.name}");
//        TakeDamage(10);
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Q))
//        {
//            TakeDamage(100);
//        }
//    }
//    void OnPlayerKill(string killerName)
//    {
//        TopPlayerDisplay.Instance.AddKillPoint(killerName);
//    }
//}
//Phần đêm 22/4

using Fusion;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnHealthChanged))]
    public int Health { get; set; } = 100;

    [Networked]
    public int PlayerNumber { get; set; }

    [Networked]
    public NetworkString<_16> LastKillerName { get; set; } // Lưu tên người gây sát thương cuối cùng

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Vector3 healthBarOffset = new Vector3(3f, 20f, 25f);
    [SerializeField] private GameObject debugTextPrefab;
    private GameObject debugTextObj;

    public override void Spawned()
    {
        Debug.Log($"[HealthPlayer] {Object.Name} Spawned, HasStateAuthority: {Object.HasStateAuthority}");

        if (Object.HasStateAuthority)
        {
            Health = 100;
            PlayerNumber = Runner.ActivePlayers.Count();
            Debug.Log($"[HealthPlayer] Đặt máu ban đầu = 100 và PlayerNumber = {PlayerNumber} cho {Object.Name}");
        }

        healthSlider = GetComponentInChildren<Slider>();
        if (healthSlider == null)
        {
            Debug.LogError("[HealthPlayer] Không tìm thấy Slider trong Player!");
        }
        else
        {
            SetupHealthBarCanvas();
            healthSlider.maxValue = 100;
            healthSlider.value = Health;
            Debug.Log($"[HealthPlayer] Khởi tạo thanh máu: {Health}/100");
        }

        UpdateHealthBar();

        if (Object.HasInputAuthority)
        {
            Debug.Log("[HealthPlayer] Người chơi local, tìm và gán camera");
            GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
            if (camObj != null)
            {
                var camFollow = camObj.GetComponent<CameraFollow>();
                if (camFollow != null)
                {
                    camFollow.AssignCamera(this.transform);
                    Debug.Log("[HealthPlayer] Camera đã gán cho người chơi local.");
                }
                else
                {
                    Debug.LogError("[HealthPlayer] Không tìm thấy CameraFollow trên MainCamera!");
                }
            }
            else
            {
                Debug.LogError("[HealthPlayer] Không tìm thấy MainCamera!");
            }
        }

        CreateDebugText();
    }

    private void SetupHealthBarCanvas()
    {
        Canvas canvas = healthSlider.GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.transform.SetParent(transform);
            canvas.transform.localPosition = healthBarOffset;
            canvas.transform.localRotation = Quaternion.identity;
            canvas.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            Canvas.ForceUpdateCanvases();
            Debug.Log($"[HealthPlayer] Đã thiết lập World Space Canvas cho {Object.Name}");
        }
    }

    void CreateDebugText()
    {
        if (debugTextPrefab != null)
        {
            debugTextObj = Instantiate(debugTextPrefab, transform.position + Vector3.up * 2.5f, Quaternion.identity);
            debugTextObj.transform.SetParent(transform);
            var textComponent = debugTextObj.GetComponent<TextMesh>();
            if (textComponent != null)
            {
                textComponent.text = $"{Object.Name}\nHP: {Health}\nPlayer #{PlayerNumber}";
            }
        }
    }

    private void OnHealthChanged()
    {
        UpdateHealthBar();
        Debug.Log($"[HealthPlayer] {Object.Name} - Health mới: {Health}");

        if (debugTextObj != null)
        {
            var textComponent = debugTextObj.GetComponent<TextMesh>();
            if (textComponent != null)
            {
                textComponent.text = $"{Object.Name}\nHP: {Health}\nPlayer #{PlayerNumber}";
            }
        }
    }

    private void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = Health;
            Debug.Log($"[HealthPlayer] Cập nhật thanh máu: {Health}/100");
        }
    }

    public void TakeDamage(int damage, NetworkString<_16> killerName)
    {
        if (!Object.HasStateAuthority) return;

        LastKillerName = killerName;
        int oldHealth = Health;
        Health = Mathf.Max(0, Health - damage);

        Debug.Log($"[HealthPlayer] {Object.Name} - TakeDamage: {oldHealth} -> {Health} (trừ {damage}) bởi {killerName}");

        if (Health <= 0)
        {
            Debug.Log($"[HealthPlayer] {Object.Name} - Health = 0, gọi Die()");
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"[HealthPlayer] {Object.Name} đã chết, InputAuthority: {Object.InputAuthority}");

        if (!string.IsNullOrEmpty(LastKillerName.ToString()) && Object.HasStateAuthority)
        {
            TopPlayerDisplay.Instance.AddKillPoint(LastKillerName);
            Debug.Log($"[HealthPlayer] Ghi điểm kill cho {LastKillerName}");
        }

        if (Object.HasInputAuthority)
        {
            FollowAnotherAlivePlayer();
        }

        if (Object.HasStateAuthority)
        {
            Debug.Log($"[HealthPlayer] Despawn {Object.Name}");
            Runner.Despawn(Object);
        }
    }

    private void FollowAnotherAlivePlayer()
    {
        var camObj = GameObject.FindGameObjectWithTag("MainCamera");
        if (camObj == null) return;

        var camFollow = camObj.GetComponent<CameraFollow>();
        if (camFollow == null) return;

#if UNITY_2023_1_OR_NEWER
        var allPlayers = FindObjectsByType<HealthPlayer>(FindObjectsSortMode.None);
#else
        var allPlayers = FindObjectsOfType<HealthPlayer>();
#endif

        foreach (var p in allPlayers)
        {
            if (p != this && p.Health > 0)
            {
                camFollow.AssignCamera(p.transform);
                Debug.Log($"[HealthPlayer] Camera đã chuyển sang người chơi {p.name}");
                return;
            }
        }

        Debug.Log("[HealthPlayer] Không còn người chơi sống để camera theo.");
    }

    private void LateUpdate()
    {
        if (healthSlider != null)
        {
            Canvas canvas = healthSlider.GetComponentInParent<Canvas>();
            if (canvas != null && canvas.renderMode == RenderMode.WorldSpace && Camera.main != null)
            {
                canvas.transform.LookAt(Camera.main.transform);
                canvas.transform.Rotate(0, 180, 0);
            }
        }
    }

    [ContextMenu("Test Take Damage")]
    void InspectorTestDamage()
    {
        Debug.Log($"[TEST] Gọi TakeDamage(10) trên {gameObject.name}");
        TakeDamage(10, "TestKiller");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(100, "TestKiller");
        }
    }
}