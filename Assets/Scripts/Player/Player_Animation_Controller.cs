using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Animation_Controller : MonoBehaviour
{
    //[Header("External Components")]
    
    
    private Animator animator;
    private Vector2 MoveInput;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(Walking())
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
    private void OnMovement(InputValue inputValue)
    {
        MoveInput = inputValue.Get<Vector2>();
    }

    private bool Walking()
    {
        if (MoveInput.magnitude > 0)
        {
            return true;
        }
        return false;
    }

}
