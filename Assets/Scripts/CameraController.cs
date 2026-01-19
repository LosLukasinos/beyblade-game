using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset = new Vector3(6, 10, 6);

    [SerializeField]
    private float smoothSpeed = 5f;

    private Camera mainCamera;
    private Vector3 currentOffset;

    void Awake()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
            Debug.LogError("Main camera not found.");
        if (target == null)
            Debug.LogError("Target is missing.");
        currentOffset = offset;

        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (mainCamera == null || target == null) return;

        Vector3 desiredPosition = target.position + currentOffset;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        mainCamera.transform.LookAt(target);
    }

    public void RotateCamera(float x, float y)
    {
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        currentOffset = rotation * offset;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetOffset(Vector3 newOffset)
    {
        offset = newOffset;
        currentOffset = offset;
    }
}
