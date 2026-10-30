using System;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons = new Weapon[9];
    public InventorySlot[] slots = new InventorySlot[9];
    [SerializeField] private Button[] inventorySlots = new Button[9];
    [HideInInspector] public InventorySlot activeSlot;
    public WeaponSlot equippedWeapon;
    public Transform hand;
    public GameObject usedWeapon;
    PlayerCombat combat;
    private int slcIndex = -1;
    void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            int index = i;
            Button slotButton = slots[i].GetComponent<Button>();
            if (slotButton != null)
            {
                slotButton.onClick.AddListener(() => OnSlotClicked(index));
            }
            slots[i].SetSelected(false);
        }
    }

    private void Awake()
    {
        combat = GetComponent<PlayerCombat>();
    }
    void OnSlotClicked(int clickedIndex)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            bool isSelected = (i == clickedIndex);
            slots[i].SetSelected(isSelected);

            if (isSelected == true)
            {
                activeSlot = slots[i];
                slcIndex = i;
            }
        }
    }
    private void OnDisable()
    {
        ClearSelection();
    }
    public void ClearSelection()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSelected(false);
        }
    }
    public void AddItem(Weapon newWeapon)
    {
        if (equippedWeapon.Weapon == null)
        {
            equippedWeapon.Weapon = newWeapon;
            equippedWeapon.weaponImage.sprite = newWeapon.weaponIcon;
            equippedWeapon.weaponModel = newWeapon.weaponModel;
            usedWeapon = Instantiate(equippedWeapon.weaponModel,hand.position,equippedWeapon.weaponModel.transform.rotation);
            usedWeapon.transform.SetParent(hand);
            equippedWeapon.Timer();
            combat.CalculateRange(usedWeapon);
            return;
        }
        for (int i = 0; i < slots.Length; i++)
        {
                if (weapons[i] == null)
                {
                    weapons[i] = newWeapon;
                    slots[i].weapon = newWeapon;
                    slots[i].slotSprite.sprite = newWeapon.weaponIcon;
                    slots[i].slotSprites.SetActive(true);
                    return;
                }
        }
    }

    public void RemoveItem()
    {
        if (activeSlot != null && activeSlot.weapon != null)
        {
            if (slcIndex != -1)
            {
                weapons[slcIndex] = null;
            }

            activeSlot.weapon = null;
            activeSlot.slotSprite.sprite = null;
            activeSlot.slotSprite.gameObject.SetActive(false);

            activeSlot.SetSelected(false);
            activeSlot = null;
            slcIndex = -1;
        }
    }

    public void SwapItem()
    {
        
        if (equippedWeapon.Weapon != null && activeSlot != null && activeSlot.weapon != null)
        {
            Weapon temporaryWeapon = equippedWeapon.Weapon;
            equippedWeapon.Weapon = activeSlot.weapon;
            activeSlot.weapon = temporaryWeapon;
            equippedWeapon.weaponImage.sprite = equippedWeapon.Weapon.weaponIcon;
            activeSlot.slotSprite.sprite = activeSlot.weapon.weaponIcon;
            if (usedWeapon != null)
            {
                Destroy(usedWeapon);
            }
            equippedWeapon.weaponModel = equippedWeapon.Weapon.weaponModel;
            usedWeapon = Instantiate(equippedWeapon.Weapon.weaponModel,hand.position,equippedWeapon.weaponModel.transform.rotation);
            usedWeapon.transform.SetParent(hand);
            combat.CalculateRange(usedWeapon);
        }
    }


}
