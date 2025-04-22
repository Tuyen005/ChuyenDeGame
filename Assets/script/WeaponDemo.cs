////using Fusion;
////using System.Collections;
////using UnityEngine;

////public class WeaponDemo : NetworkBehaviour
////{
////    //private void OnTriggerEnter(Collider other)
////    //{
////    //    if (!HasStateAuthority) return; // Chỉ máy chủ xử lý va chạm

////    //    if (other.CompareTag("Player") && other.gameObject != gameObject)
////    //    {
////    //        var targetNetworkObject = other.GetComponent<NetworkObject>();
////    //        if (targetNetworkObject != null)
////    //        {
////    //            PlayerRef targetPlayer = targetNetworkObject.InputAuthority;
////    //            if (targetPlayer != PlayerRef.None)
////    //            {
////    //                Debug.Log($"[Weapon] Đánh trúng Player {targetPlayer.PlayerId}, gửi yêu cầu trừ máu.");
////    //                RpcApplyDamageToPlayer(targetPlayer, 10);
////    //            }
////    //        }
////    //    }
////    //}

////    //[Rpc(RpcSources.All, RpcTargets.StateAuthority)]
////    //public void RpcApplyDamageToPlayer(PlayerRef targetPlayer, int damage)
////    //{
////    //    if (!HasStateAuthority) return; // Đảm bảo chỉ máy chủ xử lý trừ máu

////    //    if (Runner.TryGetPlayerObject(targetPlayer, out var playerObject))
////    //    {
////    //        var health = playerObject.GetComponent<HealthPlayer>();
////    //        if (health != null)
////    //        {
////    //            Debug.Log($"[Weapon] Trừ {damage} máu của Player {targetPlayer.PlayerId}");
////    //            health.TakeDamage(damage);
////    //        }
////    //        else
////    //        {
////    //            Debug.LogError($"[Weapon] Không tìm thấy HealthPlayer trên Player {targetPlayer.PlayerId}!");
////    //        }
////    //    }
////    //    else
////    //    {
////    //        Debug.LogError($"[Weapon] PlayerRef {targetPlayer.PlayerId} chưa được Runner đăng ký!");
////    //    }
////    //}


////    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
////    public void RpcApplyDamageToBothPlayers(NetworkObject playerA, NetworkObject playerB, int damage)
////    {
////        if (playerA != null)
////        {
////            var healthA = playerA.GetComponent<HealthPlayer>();
////            if (healthA != null) healthA.TakeDamage(damage);
////        }

////        if (playerB != null)
////        {
////            var healthB = playerB.GetComponent<HealthPlayer>();
////            if (healthB != null) healthB.TakeDamage(damage);
////        }
////    }
////    //public void RpcApplyDamageToBothPlayers(PlayerRef targetPlayer, int damagee)
////    //{
////    //    Runner.TryGetPlayerObject(targetPlayer, out var playerObject);
////    //    if (playerObject != null)
////    //    {
////    //        return;
////    //    }
////    //    playerObject.GetComponent<HealthPlayer>().TakeDamage(damagee);
////    //}

////    private void OnTriggerEnter(Collider other)
////    {
////        if (!other.CompareTag("Player")) return; // Chỉ va chạm với Player

////        var otherPlayer = other.GetComponent<NetworkObject>();
////        var myPlayer = GetComponentInParent<NetworkObject>();

////        if (otherPlayer == null || myPlayer == null || otherPlayer == myPlayer) return; // Không đánh chính mình

////        RpcApplyDamageToBothPlayers(myPlayer, otherPlayer, 10);
////        Debug.Log($"Hai người chơi {myPlayer.name} và {otherPlayer.name} bị trừ máu");
////        //if (other.gameObject.CompareTag("Player"))
////        //{
////        //    var targetPlayer = other.GetComponent<NetworkObject>().InputAuthority;
////        //    RpcApplyDamageToBothPlayers(targetPlayer, 10);
////        //}
////    }



////    //private void OnTriggerEnter(Collider other)
////    //{
////    //    var animal = other.GetComponent<HealthPlayer>();
////    //    if (animal != null)
////    //    {
////    //        animal.Rpc_TakeDamage(10);
////    //    }
////    //}



































////    //private void OnTriggerEnter(Collider other)
////    //{
////    //    if (!Object.HasInputAuthority) return;

////    //    Debug.Log($"[WeaponDemo] Va chạm với {other.gameObject.name}");

////    //    if (other.CompareTag("Player") && other.gameObject != gameObject)
////    //    {
////    //        var targetNetworkObject = other.GetComponent<NetworkObject>();
////    //        if (targetNetworkObject != null)
////    //        {
////    //            PlayerRef targetPlayer = targetNetworkObject.InputAuthority;
////    //            if (targetPlayer != PlayerRef.None)
////    //            {
////    //                Debug.Log($"[WeaponDemo] Đánh trúng Player {targetPlayer.PlayerId}. Gửi RPC trừ máu.");
////    //                RpcApplyDamageToPlayer(targetPlayer, 10);
////    //            }
////    //            else
////    //            {
////    //                Debug.LogError("[WeaponDemo] InputAuthority không hợp lệ (PlayerRef.None)!");
////    //            }
////    //        }
////    //        else
////    //        {
////    //            Debug.LogError("[WeaponDemo] Không tìm thấy NetworkObject trên đối tượng bị đánh!");
////    //        }
////    //    }
////    //}

////    //[Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
////    //public void RpcApplyDamageToPlayer(PlayerRef targetPlayer, int damage)
////    //{
////    //    Debug.Log($"[WeaponDemo] RPC được gọi để trừ {damage} máu của Player {targetPlayer.PlayerId}");
////    //    if (Runner.TryGetPlayerObject(targetPlayer, out var playerObject))
////    //    {
////    //        var health = playerObject.GetComponent<HealthPlayer>();
////    //        if (health != null)
////    //        {
////    //            Debug.Log($"[WeaponDemo] Gọi TakeDamage cho Player {targetPlayer.PlayerId}");
////    //            health.TakeDamage(damage);
////    //            Debug.Log($"[WeaponDemo] Đã gọi TakeDamage cho Player {targetPlayer.PlayerId}");
////    //        }
////    //        else
////    //        {
////    //            Debug.LogError($"[WeaponDemo] Không tìm thấy HealthPlayer trên Player {targetPlayer.PlayerId}!");
////    //        }
////    //    }
////    //    else
////    //    {
////    //        Debug.LogError($"[WeaponDemo] PlayerRef {targetPlayer.PlayerId} chưa được Runner đăng ký!");
////    //    }
////    //}
////}

//////private void OnTriggerEnter(Collider other)
//////{
//////    if (!Object.HasInputAuthority) return; // Chỉ client sở hữu vũ khí gửi yêu cầu

//////    if (other.gameObject.CompareTag("Player") && other.gameObject != gameObject)
//////    {
//////        var targetPlayer = other.gameObject.GetComponent<NetworkObject>().InputAuthority;
//////        RpcApplyDamageToPlayer(targetPlayer, 10);
//////    }
//////}

//////[Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
//////public void RpcApplyDamageToPlayer(PlayerRef targetPlayer, int damage)
//////{
//////    if (!Runner.TryGetPlayerObject(targetPlayer, out var plObject))
//////    {
//////        Debug.LogWarning($"Không tìm thấy người chơi {targetPlayer}");
//////        return;
//////    }

//////    var playerProps = plObject.GetComponent<HealthPlayer>();
//////    if (playerProps != null)
//////    {
//////        playerProps.TakeDamage(damage);
//////    }
//////}

//////using Fusion;
//////using UnityEngine;

//////public class WeaponDemo : NetworkBehaviour
//////{
//////    [SerializeField] private int damage = 10;
//////    [SerializeField] private float hitCooldown = 0.5f;
//////    private float lastHitTime;

//////    private void OnTriggerEnter(Collider other)
//////    {
//////        if (!Object.HasStateAuthority) return;
//////        if (!other.CompareTag("Player")) return;
//////        if (Time.time - lastHitTime < hitCooldown) return;

//////        // Lấy NetworkObject của đối phương
//////        var hitPlayer = other.GetComponent<NetworkObject>();
//////        if (hitPlayer == null) return;

//////        // Kiểm tra không đánh chính mình
//////        if (hitPlayer == Object) return;

//////        // Gọi RPC để trừ máu đối phương
//////        Rpc_DealDamage(hitPlayer);
//////        lastHitTime = Time.time;
//////    }

//////    [Rpc(RpcSources.StateAuthority, RpcTargets.StateAuthority)]
//////    private void Rpc_DealDamage(NetworkObject hitPlayer)
//////    {
//////        var health = hitPlayer.GetComponent<HealthPlayer>();
//////        if (health != null)
//////        {
//////            health.TakeDamage(damage);
//////            Debug.Log($"{Object.name} đã gây {damage} damage cho {hitPlayer.name}");
//////        }
//////    }
//////}

//using Fusion;
//using UnityEngine;

//public class WeaponDemo : NetworkBehaviour
//{
//    [Rpc(RpcSources.All, RpcTargets.All)]
//    public void RpcNotifyHit(NetworkObject targetPlayer, int damage)
//    {
//        Debug.Log($"[RPC] Phát hiện đòn đánh vào {targetPlayer.name} với {damage} damage");

//        // Mỗi máy chỉ xử lý damage cho các object mà mình có StateAuthority
//        if (targetPlayer != null && targetPlayer.HasStateAuthority)
//        {
//            var health = targetPlayer.GetComponent<HealthPlayer>();
//            if (health != null)
//            {
//                health.TakeDamage(damage);
//                Debug.Log($"[RPC] Đã trừ {damage} máu của {targetPlayer.name} (có StateAuthority)");
//            }
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (!other.CompareTag("Player")) return;

//        var targetPlayer = other.GetComponent<NetworkObject>();
//        var myPlayer = GetComponentInParent<NetworkObject>();

//        if (targetPlayer == null || myPlayer == null || targetPlayer == myPlayer) return;

//        Debug.Log($"Va chạm giữa {myPlayer.name} và {targetPlayer.name}");
//        RpcNotifyHit(targetPlayer, 10);
//    }
//}


//Phần đêm 22/4
using Fusion;
using UnityEngine;

public class WeaponDemo : NetworkBehaviour
{
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RpcNotifyHit(NetworkObject targetPlayer, int damage, NetworkString<_16> killerName)
    {
        Debug.Log($"[RPC] Phát hiện đòn đánh vào {targetPlayer.name} bởi {killerName} với {damage} damage");

        if (targetPlayer != null && targetPlayer.HasStateAuthority)
        {
            var health = targetPlayer.GetComponent<HealthPlayer>();
            if (health != null)
            {
                health.TakeDamage(damage, killerName);
                Debug.Log($"[= {damage} máu của {targetPlayer.name} (có StateAuthority)");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var targetPlayer = other.GetComponent<NetworkObject>();
        var myPlayer = GetComponentInParent<NetworkObject>();

        if (targetPlayer == null || myPlayer == null || targetPlayer == myPlayer) return;

        var killer = myPlayer.GetComponent<Player>();
        NetworkString<_16> killerName = killer != null ? killer.PlayerName : myPlayer.name;

        Debug.Log($"Va chạm giữa {myPlayer.name} và {targetPlayer.name}");
        RpcNotifyHit(targetPlayer, 10, killerName);
    }
}