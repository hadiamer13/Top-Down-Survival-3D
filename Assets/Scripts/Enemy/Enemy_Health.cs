using UnityEngine;
using System.Collections;
using System;

public class Enemy_Health : MonoBehaviour
{
    [Header("External Attributes")]
    [SerializeField] private Bullet bullet;
    

    [Header("Internal Attributes")]
    [SerializeField] private int Health = 10;
    [SerializeField] private float DamageIndicatorTime= 0.02f;
    [SerializeField] private Color DamageIndicatorColor = Color.white;


    private Color OriginalColor;
    private int Damage;
    private Coroutine coroutine;


    // all materials are kept in renderer. so we call renderer to access material
    private Renderer renderer;

    private void Awake()
    {
        Damage = bullet.BulletDamage;
        renderer = GetComponent<Renderer>();
        OriginalColor = renderer.material.color;
    }


    private void HealthDepletion()
    {
        Health -= Damage;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            HealthDepletion();
            // checks so that corutines dont overlap
            if(coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(DamageIndicator());
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator DamageIndicator()
    {
        renderer.material.color = DamageIndicatorColor;
        yield return new WaitForSeconds(DamageIndicatorTime);
        renderer.material.color = OriginalColor;
        yield return new WaitForSeconds(DamageIndicatorTime);
        renderer.material.color = DamageIndicatorColor;
        yield return new WaitForSeconds(DamageIndicatorTime);
        renderer.material.color = OriginalColor;
        yield return new WaitForSeconds(DamageIndicatorTime);
        renderer.material.color = DamageIndicatorColor;
        yield return new WaitForSeconds(DamageIndicatorTime);
        renderer.material.color = OriginalColor;
    }
    
}
