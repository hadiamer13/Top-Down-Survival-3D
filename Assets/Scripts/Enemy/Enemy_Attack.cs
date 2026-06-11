using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    [Header("External Attributes")]
    [SerializeField] private LayerMask PlayerLayer;

    [Header("Internal Attributes")]
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private float AttackDelay = 0.5f;
    [SerializeField] private int damage = 3;

    private float AttackTimer;
    private Player_Health PlayerHealth;
    

    private void Awake()
    {
        PlayerHealth = FindFirstObjectByType<Player_Health>();
        if(PlayerHealth == null)
        {
            Debug.Log("Player_Health Script not found for Enemy Attack");
        }
        AttackTimer = Time.time;
    }

    private void Update()
    {
        AttackTimer += Time.deltaTime;
        PlayerInRange();

    }

    private void PlayerInRange()
    {
        if (Physics.CheckSphere(transform.position, attackRadius, PlayerLayer) && AttackTimer >= AttackDelay)
        {
            Debug.Log("attacking");
            AttackTimer = 0;
            PlayerHealth.HealthDepletion(damage);

            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}
