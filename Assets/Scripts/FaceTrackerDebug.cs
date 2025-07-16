using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FaceTrackerDebug : MonoBehaviour
{
    private ARFaceManager faceManager;

    void Start()
    {
        faceManager = GetComponent<ARFaceManager>();
    }

    void Update()
    {
        if (faceManager.trackables.count > 0)
        {
            Debug.Log("Face detected!");
        }
        else
        {
            Debug.Log("No face detected.");
        }
    }
}
