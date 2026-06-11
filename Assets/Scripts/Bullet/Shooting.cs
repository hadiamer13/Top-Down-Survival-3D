using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [Header("Gun Attributes")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform BulletHolder; 
    [SerializeField] private int poolSize = 20;

    [Header("Shooting Mechanics")]
    [SerializeField] private float shootDelay = 0.5f;
    [SerializeField] private float reloadTime = 1.5f;
    [SerializeField] private int magazineSize = 8;

    private bool isReloading = false;
    private float currentAmmo;
    private float shootTimeCounter;

    // same as vector in c++
    private List<GameObject> bulletPool;

    private void Awake()
    {
        shootTimeCounter = Time.time;
        currentAmmo = magazineSize;
        InitializeBulletPool();
    }

    private void Update()
    {
        shootTimeCounter += Time.deltaTime;
        if(currentAmmo <= 0)
        {
            StartCoroutine(ReloadComplete());
        }
        
    }

    private void OnReload(InputValue inputvalue)
    {
        isReloading = inputvalue.isPressed;
        if(isReloading && currentAmmo < magazineSize)
        {
            
            StartCoroutine(ReloadComplete());
        }
    }

    private void OnFire(InputValue inputValue)
    {
        bool isShooting = inputValue.isPressed;
        if(isShooting && !isReloading && shootTimeCounter > shootDelay && currentAmmo >0)
        {
            GameObject Ammo = FindDisabledBullet();
            if(Ammo != null)
            {
                Debug.Log("Shooting");
                shootTimeCounter = 0;

                currentAmmo--;
                Debug.Log(currentAmmo);


                // matches ammo position and rotaition with bulletholder position and rotation
                Ammo.transform.position = BulletHolder.position;
                Ammo.transform.rotation = BulletHolder.rotation;
                // activate later otherwise bullet will move in wrong direction in bulllet script
                Ammo.SetActive(true);
            }

        }
    }

    private GameObject FindDisabledBullet()
    {
        for(int i = 0; i <poolSize;i++)
        {
            if(!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }
        return null;
    }

    private IEnumerator ReloadComplete()
    {
        Debug.Log("Reload Started");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magazineSize;
        Debug.Log("Reload Complete");
        isReloading = false;
    }

    private void InitializeBulletPool()
    {
        bulletPool = new List<GameObject>();

        for(int i = 0;i< poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);

            bulletPool.Add(bullet);
        }
    }
}
