using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARCameraManager))]
public class ForceFrontCamera : MonoBehaviour
{
    void Awake()
    {
        var cameraManager = GetComponent<ARCameraManager>();
        cameraManager.requestedFacingDirection = CameraFacingDirection.User;
    }
}
