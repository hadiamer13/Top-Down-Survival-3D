using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Animation_Controller : MonoBehaviour
{
    //[Header("External Components")]


    private Animator animator;
    private Player_rotation playerRotation;
    private Vector2 lookDirection;
    private Vector2 MoveInput;
    int running;
    int walkingBack;
    bool iswalking;
    bool isrunning;

    private void Awake()
    {
        playerRotation = GetComponent<Player_rotation>();
        animator = GetComponent<Animator>();
        running = Animator.StringToHash("isRunning");
        walkingBack = Animator.StringToHash("isWalkingBack");

    }

    private void FixedUpdate()
    {
        lookDirection = new Vector2(playerRotation.lookDirection.normalized.x, playerRotation.lookDirection.normalized.z);
        Debug.Log(lookDirection);
        iswalking = WalkingBack();
        isrunning = Running();

        if (iswalking && isrunning)
        {
            animator.SetBool(walkingBack, true);
            animator.SetBool(running, false);

        }

    }
    private void OnMovement(InputValue inputValue)
    {
        MoveInput = inputValue.Get<Vector2>();
    }

    private bool Running()
    {
        if (MoveInput.magnitude > 0)
        {
            animator.SetBool(running, true);
            return true;
        }
        //Debug.Log("Running DEactive");
        animator.SetBool(running, false);
        return false;
    }

    //make direction limited to 45 degree rotation modify
    private bool WalkingBack()
    {
        if (Mathf.Sign(lookDirection.x) != Mathf.Sign(MoveInput.x) && Mathf.Sign(lookDirection.y) != Mathf.Sign(MoveInput.y) && MoveInput != Vector2.zero)
        {
            //Debug.Log("walking back active");
            animator.SetBool(walkingBack, true);
            return true;
        }
        //Debug.Log("walking back DEactive");

        animator.SetBool(walkingBack, false);
        return false;
    }

}
