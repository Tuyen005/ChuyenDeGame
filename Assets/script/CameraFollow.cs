using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;



public class CameraFollow : MonoBehaviour
{

    private Transform targetTransform;
    //public CinemachineVirtualCamera virtualCamera;
    //Sửa thành CinemachineCamera thay vì CinemachineVirtualCamera
    public CinemachineCamera virtualCamera;
    //private CinemachineFramingTransposer cinemachineFramingTransposer;
    //Sửa thành CinemachineFollow (thay cho FramingTransposer trong phiên bản mới)
    //private CinemachineFollow cinemachineFollow;
    private CinemachinePositionComposer positionComposer;

    public CinemachineCamera tpsCamera;  // Camera góc nhìn thứ ba (xa)
    public CinemachineCamera shoulderCamera; // Camera ngang vai (gần hơn TPS)
    

    private int cameraMode = 0; // 0 = FPS, 1 = TPS, 2 = Shoulder

    public void AssignCamera(Transform playerTransform)
    {
        // Gán mục tiêu Follow và LookAt cho camera
        virtualCamera.Follow = playerTransform;
        virtualCamera.LookAt = playerTransform;

       // tpsCamera.Follow = playerTransform;
       // tpsCamera.LookAt = playerTransform;

        shoulderCamera.Follow = playerTransform;
        shoulderCamera.LookAt = playerTransform;

        targetTransform = playerTransform; // Lưu lại mục tiêu để sử dụng trong các phương thức khác


    }


    private void Start()
    {
        //cinemachineFollow = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body) as CinemachineFollow;
        // Lấy component Position Composer từ Virtual Camera
        positionComposer = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body) as CinemachinePositionComposer;

        if (positionComposer == null)
        {
            Debug.LogError("CinemachinePositionComposer không được tìm thấy trong Virtual Camera!");
        }
        SwitchCamera(0);
    }
    void SwitchCamera(int mode)
    {
        virtualCamera.Priority = (mode == 0) ? 10 : 5;
        tpsCamera.Priority = (mode == 1) ? 10 : 5;
        shoulderCamera.Priority = (mode == 2) ? 10 : 5;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) // Nhấn V để chuyển đổi
        {
            cameraMode = (cameraMode + 1) % 3; // Chuyển đổi giữa 0 → 1 → 2 → 0
            SwitchCamera(cameraMode);
        }

        //if (cinemachineFollow == null)
        //    return;

        //float scroll = Input.GetAxis("Mouse ScrollWheel");

        //// 4. Sử dụng property CameraDistance thay vì m_CameraDistance
        //cinemachineFollow.CameraDistance -= scroll * 5f; // Nhân tốc độ zoom
        //cinemachineFollow.CameraDistance = Mathf.Clamp(cinemachineFollow.CameraDistance, 2f, 10f);
        if (positionComposer != null)
        {
            // Lấy giá trị cuộn chuột
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            // Điều chỉnh khoảng cách camera bằng cách thay đổi giá trị Z của TargetOffset
            Vector3 offset = positionComposer.TargetOffset;
            offset.z -= scroll * 5f; // Nhân tốc độ zoom
            offset.z = Mathf.Clamp(offset.z, -10f, 5f); // Giới hạn khoảng cách Z
            positionComposer.TargetOffset = offset; // Gán lại giá trị mới
        }
        HandleRotation();


        ChuyenCam();

    }
    private void ChuyenCam()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            // Kiểm tra xem người đang được theo dõi có còn sống không
            HealthPlayer currentTarget = targetTransform != null ? targetTransform.GetComponent<HealthPlayer>() : null;

            if (currentTarget != null && currentTarget.Health > 0)
            {
                Debug.Log("Không thể chuyển camera vì nhân vật còn sống.");
                return; // Không cho chuyển camera nếu nhân vật còn sống
            }

            RefreshAlivePlayers();

            if (alivePlayers.Count > 0)
            {
                currentIndex = (currentIndex + 1) % alivePlayers.Count;
                AssignCamera(alivePlayers[currentIndex]);
                Debug.Log($"[Spectator] Camera chuyển đến người chơi thứ {currentIndex}");
            }
        }
    }


    [Header("Camera Rotation Settings")]
    public float rotationSpeed = 100f;
    public bool invertYAxis = false;
    public float minVerticalAngle = -30f;
    public float maxVerticalAngle = 60f;

    // To store the current rotation
    private float currentXRotation = 0f;
    private float currentYRotation = 0f;

    // The camera's target
   
    private void HandleRotation()
    {
        // Only rotate when right mouse button is held down
       
            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            // Apply inversion if needed
            if (invertYAxis)
                mouseY = -mouseY;

            // Update rotation values
            currentYRotation += mouseX;
            currentXRotation -= mouseY; // Subtract to get correct direction

            // Clamp vertical rotation
            currentXRotation = Mathf.Clamp(currentXRotation, minVerticalAngle, maxVerticalAngle);

            // Create rotation from updated values
            Quaternion rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);

            // Option 1: Directly rotate the virtual camera
            virtualCamera.transform.rotation = rotation;

            // Option 2: Alternative method - If you want to maintain Follow but override LookAt behavior
            // Adjust the LookAtOffset instead of direct rotation
            // if (positionComposer != null)
            // {
            //     // Calculate direction vector
            //     Vector3 direction = rotation * Vector3.forward;
            //     
            //     // Adjust the LookAt offset to point in this direction
            //     positionComposer.TrackedObjectOffset = direction * 2f; // adjust distance as needed
            // }
        
    }



    //public class CameraFollow : MonoBehaviour
    //{
    //    public CinemachineCamera virtualCamera;
    //    private CinemachineFramingTransposer framingTransposer;

    //    public void AssignCamera(Transform playerTransform)
    //    {
    //        virtualCamera.Follow = playerTransform;
    //        virtualCamera.LookAt = playerTransform;
    //    }

    //    private void Start()
    //    {
    //        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    //    }

    //    private void Update()
    //    {
    //        float scroll = Input.GetAxis("Mouse ScrollWheel");
    //        framingTransposer.m_CameraDistance -= scroll;
    //        framingTransposer.m_CameraDistance = Mathf.Clamp(framingTransposer.m_CameraDistance, 2f, 10f);
    //    }
    //}







   

    private List<Transform> alivePlayers = new List<Transform>();
    private int currentIndex = 0;

   

  
   

    private void RefreshAlivePlayers()
    {
        alivePlayers.Clear();

 
        var players = FindObjectsByType<HealthPlayer>(FindObjectsSortMode.None);


        foreach (var player in players)
        {
            if (player.Health > 0)
            {
                alivePlayers.Add(player.transform);
            }
        }

        if (currentIndex >= alivePlayers.Count)
        {
            currentIndex = 0;
        }
    }
}
