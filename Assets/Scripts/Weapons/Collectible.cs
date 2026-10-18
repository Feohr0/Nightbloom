using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Weapon weaponData;
    bool inRange;
    [SerializeField] Inventory inventory;
    PlayerController playerController;
    private void Awake()
    {
        if (weaponData == null)
        {
            Debug.LogError("Data is null!!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            playerController = null;
        }
    }

    private void Update()
    {
        if (inRange && playerController.collecting ==true)
        {
            inventory.AddItem(this.weaponData);
            Destroy(gameObject);
        }
    }
}
