using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Camera mainCamera;
    BeybladeController bey;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        bey = GetComponent<BeybladeController>();

        StartRotation();
    }

    void Update()
    {
        DoMovement();
    }

    void DoMovement()
    {
        if (bey != null && !bey.IsAlive) return;

        var inputX = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(inputX, 0f, inputY);

        if (mainCamera != null)
        {
            movement = mainCamera.transform.TransformDirection(movement);
            movement.y = 0f;
        }

        // Speed now comes from the bottom part
        float speed = bey != null ? bey.MoveSpeed : 1.25f;
        rb.AddForce(movement * speed, ForceMode.Impulse);
    }

    void StartRotation()
    {
        // Initial spin now comes from the bottom part
        float spinSpeed = bey != null ? bey.SpinSpeed : 100f;
        rb.AddTorque(transform.up * spinSpeed);
    }
}