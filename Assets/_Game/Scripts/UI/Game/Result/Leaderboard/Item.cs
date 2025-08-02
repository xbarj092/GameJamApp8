using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI Position { get; set; }
    [field: SerializeField] public TextMeshProUGUI Score { get; set; }
    [field: SerializeField] public TextMeshProUGUI Name { get; set; }
}
