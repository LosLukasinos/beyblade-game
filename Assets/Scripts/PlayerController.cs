using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] float rotationStrength = 100f;
    Rigidbody rb;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        StartRotation();
    }

    void Update()
    {
        DoMovement();
    }

    void DoMovement()
    {
        var inputX = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(inputX, 0f, inputY);

        if (mainCamera != null)
        {
            movement = mainCamera.transform.TransformDirection(movement);
            movement.y = 0f;
        }

        rb.AddForce(movement * speed, ForceMode.Acceleration);
    }

    void StartRotation()
    {
        rb.AddTorque(transform.up * rotationStrength);
    }
}
