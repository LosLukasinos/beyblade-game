using TMPro;
using UnityEngine;

public class HpDizplai : MonoBehaviour
{

    [SerializeField]
    private BeybladeController target1;
    [SerializeField]
    private BeybladeController target2;

    [SerializeField]
    private TMP_Text text1;
    [SerializeField]
    private TMP_Text text2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text1.text =  "Player HP: " + target1.CurrentHealth.ToString();
        text2.text = "Enemy HP: " + target2.CurrentHealth.ToString();
    }
}
