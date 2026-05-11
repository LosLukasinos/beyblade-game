using UnityEngine;

/// <summary>
/// Core Beyblade component. Attach this to your beyblade GameObject.
/// Holds references to all three parts and exposes their combined stats.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class BeybladeController : MonoBehaviour
{
    [Header("Parts")]
    public BeybladeTopPart topPart;
    public BeybladeBodyPart bodyPart;
    public BeybladeBottomPart bottomPart;

    [Header("Runtime State")]
    [SerializeField] private float currentSpinSpeed;
    [SerializeField] private float currentHealth = 100f;

    public float CurrentHealth { get => currentHealth; }

    [Header("Spin Decay")]
    [Tooltip("How fast the beyblade loses spin speed per second")]
    public float spinDecayRate = 20f;
    [Tooltip("Beyblade stops fighting below this spin speed")]
    public float minSpinSpeed = 50f;

    [Header("Safety Settings")]
    [Tooltip("Minimum time between taking hits to prevent instant death")]
    public float hitCooldown = 2f;
    private float lastHitTime;

    private Rigidbody rb;
    private bool isAlive = true;

    // -- Public read-only state --
    public float SpinSpeed => currentSpinSpeed;
    public bool IsAlive => isAlive;
    public float Health => currentHealth;

    // -- Derived stats from parts --
    public float Damage => topPart != null ? topPart.damage : 0f;
    public float KnockbackForce => topPart != null ? topPart.knockbackForce : 0f;
    public float KnockbackResistance => bodyPart != null ? bodyPart.knockbackResistance : 1f;
    public float MoveSpeed => bottomPart != null ? bottomPart.moveSpeed : 3f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        currentSpinSpeed = bottomPart != null ? bottomPart.initialSpinSpeed : 300f;
        lastHitTime = -hitCooldown; // Initialize so we can be hit immediately at start
    }

    void Update()
    {
        if (!isAlive) return;

        //currentSpinSpeed -= spinDecayRate * Time.deltaTime;

        if (currentSpinSpeed <= minSpinSpeed)
        {
            Die();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isAlive) return;

        BeybladeController other = collision.gameObject.GetComponent<BeybladeController>();
        if (other == null || !other.isAlive) return;

        // Apply damage and knockback TO the other beyblade
        Vector3 hitDirection = (other.transform.position - transform.position).normalized;
        other.ReceiveHit(Damage, hitDirection, KnockbackForce);
    }

    public void ReceiveHit(float incomingDamage, Vector3 direction, float incomingKnockback)
    {
        // 1. Check if alive OR if we are currently in the cooldown period
        if (!isAlive || Time.time < lastHitTime + hitCooldown) return;

        // 2. Update the timestamp to "lock" the beyblade from further hits
        lastHitTime = Time.time;

        // Body part reduces received knockback
        float finalKnockback = incomingKnockback * (1f - Mathf.Clamp01(KnockbackResistance));
        rb.AddForce(direction * finalKnockback, ForceMode.Impulse);

        // Damage also reduces spin speed proportionally
        currentSpinSpeed -= incomingDamage;
        currentHealth -= incomingDamage;

        Debug.Log($"{gameObject.name} hit! Health: {currentHealth:F0} | Spin: {currentSpinSpeed:F0}");

        if (currentHealth <= 0f || currentSpinSpeed <= minSpinSpeed)
            Die();
    }

    void Die()
    {
        if (!isAlive) return;
        isAlive = false;
        currentSpinSpeed = 0f;
        rb.constraints = RigidbodyConstraints.None;
        Debug.Log($"{gameObject.name} has been knocked out!");
        
    }

    void LateUpdate()
    {
        if (!isAlive) return;
        transform.Rotate(Vector3.up, currentSpinSpeed * Time.deltaTime, Space.Self);
    }
}