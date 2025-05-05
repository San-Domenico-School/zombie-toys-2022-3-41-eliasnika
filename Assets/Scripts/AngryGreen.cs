using UnityEngine;

public class AngryGreen : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackDamage = 10f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            RedHealth redHealth = player.GetComponent<RedHealth>();
            if (redHealth != null)
            {
                redHealth.TakeDamage(attackDamage);
            }
        }
    }
}
