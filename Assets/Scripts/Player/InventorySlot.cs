using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image slotSprite;
    public GameObject slotSprites;
    public Weapon weapon;
    public GameObject selectionSprite;
   public void SetSelected(bool isSelected)
    {
        if (selectionSprite != null)
        {
            selectionSprite.SetActive(isSelected);
        }
    } 
}
