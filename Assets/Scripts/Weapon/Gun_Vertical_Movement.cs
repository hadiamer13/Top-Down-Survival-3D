using TMPro;
using UnityEngine;

public class Gun_Vertical_Movement : MonoBehaviour
{
    [SerializeField] private Player_rotation player_Rotation;

    [SerializeField] private float rotateSpeed = 0.5f;
    [SerializeField] private float maxAngle = 45;
    [SerializeField] private float minAngle = -45;
    private void Update()
    {
        RotateGun();
    }

    private void RotateGun()
    {
        Vector3 targetPosition = player_Rotation.GetTargetPositionY();

        Vector3 localTargetPosition = transform.parent.InverseTransformPoint(targetPosition);

        Vector3 lookDirection = localTargetPosition - transform.position;

        float angleRadian = Mathf.Atan2(lookDirection.y, lookDirection.x);
        float angleDegree = angleRadian * Mathf.Rad2Deg;

        // in unity upwards rotation is -ve
        float ClampAngle = Mathf.Clamp(-angleDegree ,minAngle , maxAngle);

        Quaternion targetRotation = Quaternion.Euler(ClampAngle,0,0);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation,rotateSpeed * Time.deltaTime);

    }



}
