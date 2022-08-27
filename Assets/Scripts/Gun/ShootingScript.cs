using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    //[SerializeField] GameObject crosshair;
    [SerializeField] Transform gun;
    [SerializeField] Transform gunBarrelEnd;
    [SerializeField] GameObject bullet;
    [SerializeField] float aimSensitivity;
    [SerializeField] float shootingForce = 3f;
    [SerializeField] float shootCoolDown = 1f;


    //GameObject crosshairInstance;
    float xRotation = 0;
    float yRotation = 0;
    float lastShotTime = 0;
    private void Start()
    {
        //crosshairInstance = Instantiate(crosshair);
        xRotation = 0;
        yRotation = 0;
        
        
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            if(GetInput()) gun.transform.localRotation = Quaternion.Euler(yRotation, xRotation, gun.transform.localRotation.z);
        }
        
        lastShotTime += Time.deltaTime;

        if(lastShotTime >= shootCoolDown)
        {
            Shoot();
            lastShotTime = 0;
        }

    }

    private void Shoot()
    {
        var bulletRb = Instantiate(bullet, gunBarrelEnd.position, Quaternion.identity).GetComponent<Rigidbody>();
        var shootDir = gunBarrelEnd.forward;
        bulletRb.velocity = shootDir * shootingForce;
        StartCoroutine(destroyBullets(bulletRb.gameObject));
    }

    bool GetInput()
    {
        
        Touch touch = Input.GetTouch(0);

        if(touch.phase == TouchPhase.Moved)
        {
            xRotation = Mathf.Lerp(xRotation, xRotation + touch.deltaPosition.x, Time.deltaTime * aimSensitivity);
            yRotation = Mathf.Lerp(yRotation, yRotation - touch.deltaPosition.y, Time.deltaTime * aimSensitivity);

            xRotation = Mathf.Clamp(xRotation, -shootingForce * 4, shootingForce * 4);
            yRotation = Mathf.Clamp(yRotation, -shootingForce * 3, shootingForce * 1);
            return true;
        }
        

        return false;
    }
    IEnumerator destroyBullets(GameObject bullet)
    {
        yield return new WaitForSeconds(1.5f);
        GameObject.Destroy(bullet);
    }


}
