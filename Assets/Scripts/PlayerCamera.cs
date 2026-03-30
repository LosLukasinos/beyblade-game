using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float distance = 15f;
    [SerializeField] float sens = 5f;

    float xRot = 0f;
    float yRot = 0f;

    Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        xRot += Input.GetAxis("Mouse X") * sens;
        yRot -= Input.GetAxis("Mouse Y") * sens;

        yRot = Mathf.Clamp(yRot, -40f, 80f);

        Quaternion rot = Quaternion.Euler(yRot, xRot, 0f);
        Vector3 pos = target.position - (rot * Vector3.forward * distance);

        transform.rotation = rot;
        transform.position = pos;
    }
}
