using UnityEngine;
using UnityEngine.InputSystem;

public class Player_rotation : MonoBehaviour
{
    [Header("Components Assigner")]
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private float rotateSpeed = 0.5f;

    private Vector3 TargetYPos = Vector3.up;

    private Camera camera;
    private void Start()
    {
        camera = FindAnyObjectByType<Camera>();
    }

    private void FixedUpdate()
    {
        RotatePlayer();
    }
    private void RotatePlayer()
    {
        // gets mouse position on screen. bottom left is (0,0) (new input system)
        Vector2 MousePosition = Mouse.current.position.ReadValue();
        
        // converts 2D point to a ray which starts from camera and goes into the mouse position. (starting point + direction vector)
        Ray ray = camera.ScreenPointToRay(MousePosition);

        //RayCast is the actual action of shooting the ray
        // out raycast hits tells what object was hit and where it was hit
        if(Physics.Raycast(ray,out RaycastHit hitInfo, Mathf.Infinity))
        {
            Vector3 targetPosition = hitInfo.point;
            TargetYPos = targetPosition;

            // so that player doesnt go into the ground
            targetPosition.y = transform.position.y;
            
            Vector3 lookDirection = targetPosition - transform.position;

            // to ensure value doesn't reach 0. it will cause crashes when divided by 0
            if(lookDirection.sqrMagnitude > 0.1f)
            {
                // deals with 4D. used for rotation
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

                transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,rotateSpeed);
            }
        }
    }

    public Vector3 GetTargetPositionY()
    {
        return TargetYPos;
    }

}
