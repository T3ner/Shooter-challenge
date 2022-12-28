using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] Camera fpsCam;
    [SerializeField] int damage;

    [SerializeField] ParticleSystem gunFlash;
    [SerializeField] Transform gunHolder;
   
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && transform.parent == gunHolder)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        gunFlash.Play();
        RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) 
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                Debug.Log("Shot");
                enemy.Hurt(damage);
            }
        }
    }
}
