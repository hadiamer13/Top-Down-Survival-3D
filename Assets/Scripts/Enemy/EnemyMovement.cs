using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody rb;

    
    

    [Header("Enemy Attributes")]
    [SerializeField] private bool isKnockbackEnabled = true;
    [SerializeField] private bool isSquashEffectEnable = true;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float squashMagnitude = 0.2f;
    [SerializeField] private float knockbackSpeed = 2f;

    private NavMeshAgent navMeshAgent;
    private GameObject Player;
    private float knockbackease = 0.0001f;   // knockbackSpeed was goin into 0.001 so to make number realistic knockback ease is multiplied
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(GameObject.FindWithTag("Player"))
        {
            Player = GameObject.FindWithTag("Player");
        }
        else
        {
            Player = null;
        }
       
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;


    }

    private void FixedUpdate()
    {
        MoveToPlayer();
    }
    private void MoveToPlayer()
    {
        navMeshAgent.speed = moveSpeed;
        if (Player != null)
        {
            navMeshAgent.destination = Player.transform.position;
            //Vector3 targetDirection = Player.transform.position - transform.position;
            //targetDirection.y = 0;
            //rb.linearVelocity = targetDirection * moveSpeed;

        }
        else
        {
          navMeshAgent.destination = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Weapon"))
        {
            if(isKnockbackEnabled)
            {
                // takes center of both objects to get direction
                Vector3 knockbackDirection = transform.position - collision.transform.position;
                Knockback(knockbackDirection);
            }

            if(isSquashEffectEnable)
            {

            }

        }
    }

    private void Knockback(Vector3 knockbackDirection)
    {
        // top down game so no y movement
        knockbackDirection.y = 0;

        knockbackDirection = knockbackDirection.normalized;

        navMeshAgent.speed = 0;
        rb.AddForce(knockbackDirection * knockbackSpeed * knockbackease, ForceMode.Impulse);
        navMeshAgent.speed = moveSpeed;
    }

}

