using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemyHp;
    bool isDead = false;
    public enum State
    {
        idle,
        chase,
        attack
    }

    public State currentState;
    private void Update()
    {
        switch (currentState)
        {
            case State.idle:

                break;
            case State.chase:

                break;
            case State.attack:

                break;
        }
    }

    public void TakeDamage(float attackDamage)
    {

        if (isDead) return;
        
        enemyHp -= attackDamage;
        
        if (enemyHp > 0)
        {

        }
        else
        {
            Die();
        }
        
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Enemy has died");

        CapsuleCollider2D coll = GetComponent<CapsuleCollider2D>();
        coll.enabled = false;

        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        Destroy(gameObject,2f);
    }
}