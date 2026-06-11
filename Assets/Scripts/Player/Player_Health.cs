using UnityEngine;
using System;
using System.Collections;

public class Player_Health : MonoBehaviour
{
    [Header("External Attributes")]
    [SerializeField] private LayerMask EnemyLayer;

    [Header("Internal Attributes")]
    [SerializeField] private int Health;
    [SerializeField] private float DamageIndicatorTime = 0.02f;
    [SerializeField] private Color DamageIndicatorColor = Color.darkRed;

    

    private Color OriginalColor;
    private Coroutine coroutine;
    private float DamageTimeCounter;


    // all materials are kept in renderer. so we call renderer to access material
    private Renderer renderer;

    private void Awake()
    {
        
        renderer = GetComponent<Renderer>();
        OriginalColor = renderer.material.color;
        DamageTimeCounter = Time.time;
    }

   
    public void HealthDepletion(int damage)
    {
        Health -= damage;
        Debug.Log("Health --");
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(DamageIndicator());
        if (Health <= 0)
        {
            Debug.Log("Player ded");
            Destroy(gameObject);
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
