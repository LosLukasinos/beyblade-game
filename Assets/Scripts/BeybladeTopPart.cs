using UnityEngine;

[CreateAssetMenu(fileName = "NewTopPart", menuName = "Beyblade/Top Part")]
public class BeybladeTopPart : ScriptableObject
{
    [Header("Top Part Stats")]
    [Tooltip("Base damage dealt on collision")]
    public float damage = 10f;

    [Tooltip("Knockback force applied to the opponent on hit")]
    public float knockbackForce = 5f;

    [Header("Info")]
    public string partName = "Standard Top";
    [TextArea] public string description;
}