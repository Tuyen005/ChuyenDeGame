using UnityEngine;
using Fusion;

public class PlayerSetup : NetworkBehaviour
{

    public override void Spawned()
    {
        // Chỉ setup camera cho player local
        if (Object.HasInputAuthority)
        {
            SetupCamera();
        }
    }

    public void SetupCamera()
    {
        CameraFollow cameraFollow = FindFirstObjectByType<CameraFollow>();
        if (cameraFollow != null)
        {
            cameraFollow.AssignCamera(transform);
        }
    }


    //public void SetupCamera()
    //{
    //    if (Object.HasInputAuthority)
    //    {
    //        //CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
    //        //CameraFollow cameraFollow = FindFirstObjectByType<CameraFollow>();
    //        //if (cameraFollow != null)
    //        //{
    //        //    cameraFollow.AssignCamera(transform);
    //        //}


    //    }
    //}
}
