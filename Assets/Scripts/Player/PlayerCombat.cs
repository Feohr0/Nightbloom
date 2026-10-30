using NUnit.Framework.Interfaces;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerController controller;
    Inventory inv;
    [SerializeField] Animator animator;
    public float attackRange = 1.5f; // Varsayılan menzil (Inspector'dan da görülebilir)
    [SerializeField] LayerMask enemyLayer;
    MeshFilter meshFilter;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        inv = GetComponent<Inventory>();
    }

    public void CalculateRange(GameObject equippedWeapon)
    {
        // Alt objelerde olma ihtimaline karşı GetComponentInChildren kullanıldı
        meshFilter = equippedWeapon.GetComponentInChildren<MeshFilter>();
        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            Vector3 boundsSize = meshFilter.sharedMesh.bounds.size;
            float rawLength = Mathf.Max(boundsSize.x, boundsSize.y, boundsSize.z);
            float maxScale = Mathf.Max(meshFilter.transform.lossyScale.x, meshFilter.transform.lossyScale.y, meshFilter.transform.lossyScale.z);
            attackRange = rawLength * maxScale;
            Debug.Log("Yeni Otomatik Menzil Hesaplandı: " + attackRange);
        }
        else
        {
            Debug.LogWarning("Silah üzerinde MeshFilter bulunamadı! Varsayılan attackRange kullanılıyor: " + attackRange);
        }
    }

    public void PerformAttack()
    {
        if (inv.equippedWeapon == null || inv.equippedWeapon.Weapon == null || inv.usedWeapon == null) return;

        animator.SetTrigger("isAttacking");

        // UI slotu (equippedWeapon) yerine eldeki silahın (usedWeapon) konumunu kullanıyoruz:
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(inv.usedWeapon.transform.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent<EnemyController>(out EnemyController enemCont))
            {
                enemCont.TakeDamage(inv.equippedWeapon.Weapon.attackDamage);
            }
            Debug.Log("Enemy hit! " + inv.equippedWeapon.Weapon.attackDamage);
        }
    }

    private void OnDrawGizmos()
    {
        if (inv != null && inv.equippedWeapon != null && attackRange > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(inv.equippedWeapon.transform.position, attackRange);
        }
    }
}