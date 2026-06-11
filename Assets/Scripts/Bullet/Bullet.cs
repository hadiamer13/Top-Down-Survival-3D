using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Bullet Attributes")]
    [SerializeField] private float bulletSpeed = 2.0f;
    [SerializeField] private float bulletLifeTime = 3.0f;
    [SerializeField] public int BulletDamage = 3;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    // OnEnable is called whenever setActive becomes true
    private void OnEnable()
    {
        rb.linearVelocity = transform.forward * bulletSpeed;

        // cancel previous timers of invoke (incase object was destroyed by collision)
        // invoke calls a function after a time delay
        CancelInvoke();
        Invoke(nameof(DeactivateBullet), bulletLifeTime);
    }


    private void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // bullet deactive no matter what thing it collides with
        DeactivateBullet();
    }
}
