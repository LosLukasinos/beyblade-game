using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] float rotationStrength = 100f;
    Rigidbody rb;
    
    void Start()
    {
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

        Vector3 movement = new Vector3(inputX, 0f,inputY);

        rb.AddForce(movement * speed, ForceMode.Acceleration);
    }

    void StartRotation() 
    {
        rb.AddTorque(transform.up * rotationStrength);
    }
}
