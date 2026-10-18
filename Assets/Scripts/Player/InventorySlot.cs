using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image slotSprite;
    public Weapon weapon;
    private void Awake()
    {
        slotSprite = GetComponent<Image>();
    }
}
