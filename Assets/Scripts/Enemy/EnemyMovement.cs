using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody rb;

    
    

    [Header("Enemy Attributes")]
    [SerializeField] private float moveSpeed = 2;

    private NavMeshAgent navMeshAgent;
    private GameObject Player;
    

    private void Awake()
    {
        if(GameObject.FindWithTag("Player"))
        {
            Player = GameObject.FindWithTag("Player");
        }
        else
        {
            Player = null;
        }
            navMeshAgent = GetComponent<NavMeshAgent>();
    

    }

    private void FixedUpdate()
    {
        MoveToPlayer();
    }
    private void MoveToPlayer()
    {
        if(Player != null)
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

    
}
