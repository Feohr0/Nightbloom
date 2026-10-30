using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public Weapon Weapon;
    public Image weaponImage;
    public GameObject slot;
    public GameObject weaponModel;
    private Coroutine timer;
    private void FixedUpdate()
    {
        if (Weapon != null)
        {
            weaponImage.sprite = Weapon.weaponIcon;
        }
    }

   public void Timer()
    {
        if (slot != null)
        {
            slot.SetActive(true);
        }
        if (timer != null)
        {
            StopCoroutine(timer);
        }

        timer = StartCoroutine(HideSlot());
    }

    private IEnumerator HideSlot()
    {
        yield return new WaitForSeconds(3f);
        if (slot != null)
        {
            slot.SetActive(false);
        }
    }
}
