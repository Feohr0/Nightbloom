using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Weapon[] weapons = new Weapon[9];
    public InventorySlot[] slots = new InventorySlot[9];

    public void AddItem(Weapon newWeapon)
    {
      for (int i = 0; i < slots.Length; i++)
        {
            if (weapons[i] == null)
            {
                weapons[i] = newWeapon;
                slots[i].weapon = newWeapon;
                slots[i].slotSprite.sprite = newWeapon.weaponIcon;
                return;
            }
        }
    }

  
}
