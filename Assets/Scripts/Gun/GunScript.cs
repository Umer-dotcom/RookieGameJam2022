using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gunBarrelEnd;
    [SerializeField] float aimSensitivity = 3f;
    [SerializeField] float shootingForce = 3f;
    [SerializeField] float shootCoolDown = 1f;

    ObjectPoolerScript objectPooler;


    float xRotation = 0;
    float yRotation = 0;
    float lastShotTime = 0;

    private void Start()
    {
        objectPooler = ObjectPoolerScript.Instance;
    }

    private void Shoot()
    {
        var bulletRb = objectPooler.SpawnFromPool(OPTag.BULLET, gunBarrelEnd.position, Quaternion.identity).GetComponent<Rigidbody>();
        //var bulletRb = Instantiate(bulletPrefab, gunBarrelEnd.position, Quaternion.identity).GetComponent<Rigidbody>();
        
        var shootDir = gunBarrelEnd.forward;
        bulletRb.velocity = shootDir * shootingForce;
        //StartCoroutine(DestroyBullets(bulletRb.gameObject));
    }
    bool GetInput()
    {

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {
            xRotation = Mathf.Lerp(xRotation, xRotation + touch.deltaPosition.x, Time.deltaTime * aimSensitivity);
            yRotation = Mathf.Lerp(yRotation, yRotation - touch.deltaPosition.y, Time.deltaTime * aimSensitivity);

            xRotation = Mathf.Clamp(xRotation, -shootingForce * 4, shootingForce * 4);
            yRotation = Mathf.Clamp(yRotation, -shootingForce * 3, shootingForce * 1);
            return true;
        }


        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (GetInput()) transform.localRotation = Quaternion.Euler(yRotation, xRotation, transform.localRotation.z);
        }

        lastShotTime += Time.deltaTime;

        if (lastShotTime >= shootCoolDown)
        {
            Shoot();
            lastShotTime = 0;
        }
    }
}
