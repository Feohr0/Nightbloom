using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private GameObject ıcon;
    public Weapon weaponData;
    [SerializeField] private CircleCollider2D coll;
    bool inRange;
    [SerializeField] Inventory inventory;
    PlayerController playerController;
    private bool isCollected = false;
    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        if (weaponData == null)
        {
            Debug.LogError("Data is null!!");
            ıcon.SetActive(false);
            float bound = coll.bounds.max.y;
            float boundx = coll.bounds.center.x;
            ıcon.transform.position = new Vector3(boundx, bound, ıcon.transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();
            inRange = true;
            ıcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            playerController = null;
            ıcon.SetActive(false);
        }
    }

    private void Update()
    {
        if (inRange && playerController != null && playerController.collecting == true && !isCollected)
        {
            isCollected = true;

            inventory.AddItem(this.weaponData);

            if (coll != null)
            {
                coll.enabled = false;
            }

            if (ıcon != null)
            {
                ıcon.SetActive(false);
            }

            Destroy(gameObject);
        }
    }
}