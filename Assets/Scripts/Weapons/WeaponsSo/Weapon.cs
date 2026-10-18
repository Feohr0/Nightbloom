using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon")]
public class Weapon: ScriptableObject
{
    [Header("Weapon Info")]
    public string weaponName;
    public Sprite weaponIcon;
    [Header("Weapon In-Game Stats")]
    public float attackSpeed;
    public float attackDamage;
    public WeaponTypes type;

    public enum WeaponTypes
    {
        Spear,
        Sword,
        THSword,
        Axe
    }
}
