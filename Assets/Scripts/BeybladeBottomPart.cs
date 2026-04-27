using UnityEngine;

[CreateAssetMenu(fileName = "NewBottomPart", menuName = "Beyblade/Bottom Part")]
public class BeybladeBottomPart : ScriptableObject
{
    [Header("Bottom Part Stats")]
    [Tooltip("Initial spin speed when the beyblade is launched")]
    public float initialSpinSpeed = 500f;

    [Tooltip("Movement speed of the beyblade on the arena")]
    public float moveSpeed = 4f;

    [Header("Info")]
    public string partName = "Standard Bottom";
    [TextArea] public string description;
}