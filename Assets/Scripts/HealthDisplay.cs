using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{

    TextMeshProUGUI healthText;
    PlayerController player;
    
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        healthText.text = player.GetHealth().ToString();
    }

}