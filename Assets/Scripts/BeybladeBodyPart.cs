using UnityEngine;

[CreateAssetMenu(fileName = "NewBodyPart", menuName = "Beyblade/Body Part")]
public class BeybladeBodyPart : ScriptableObject
{
    [Header("Body Part Stats")]
    [Tooltip("Multiplier that reduces received knockback. 1 = full knockback, 0 = no knockback")]
    [Range(0f, 1f)]
    public float knockbackResistance = 0.5f;

    [Header("Info")]
    public string partName = "Standard Body";
    [TextArea] public string description;
}