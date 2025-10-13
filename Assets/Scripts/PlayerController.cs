using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    [SerializeField]
    CameraController cameraController;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        if (!rb || !cameraController) return;
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float mX = Input.GetAxis("Mouse X");
        float mY = Input.GetAxis("Mouse Y");
        cameraController.RotateCamera(mX, mY);
        Vector3 move = cameraController.transform.TransformDirection(input);
        move.y = 0;
        rb.AddForce(move * speed);
    }
}
