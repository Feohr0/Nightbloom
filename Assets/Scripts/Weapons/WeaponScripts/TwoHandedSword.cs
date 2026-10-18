using UnityEngine;
using UnityEngine.Assertions.Must;

public class TwoHandedSword : MonoBehaviour
{
    [SerializeField] Weapon weaponData;
    MeshRenderer weaponBounds;
    private RaycastHit2D hit;
    private void Awake()
    {
        weaponBounds = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
      hit = Physics2D.Linecast(new Vector2(weaponBounds.bounds.center.x, weaponBounds.bounds.min.y), new Vector2(weaponBounds.bounds.center.x, weaponBounds.bounds.max.y));
    }

    public void onAttack()
    {
        if (hit.collider == null)
        {

        }
    }
}
