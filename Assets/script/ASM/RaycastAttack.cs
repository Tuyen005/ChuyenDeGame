//using Fusion;
//using UnityEngine;

//public class RaycastAttack : NetworkBehaviour
//{
//    public float Damage = 10;

//    public Player PlayerMovement;

//    void Update()
//    {
//        if (HasStateAuthority == false)
//        {
//            return;
//        }

//        Ray ray = PlayerMovement.Camera.ScreenPointToRay(Input.mousePosition);
//        ray.origin += PlayerMovement.Camera.transform.forward;

//        if (Input.GetKeyDown(KeyCode.Mouse1))
//        {
//            Debug.DrawRay(ray.origin, ray.direction, Color.red, 1f);
//        }
//    }
//}