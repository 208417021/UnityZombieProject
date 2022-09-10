using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] Camera FirstPersonCamera;
    [SerializeField] float shootingRange = 100f;
    [SerializeField] int weaponDamage = 50;
    [SerializeField] ParticleSystem Spirte;
    [SerializeField] GameObject FireImpact;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shooting();
        }
    }

    private void Shooting()
    {
        RaycastProcess();
        SpirteFiring();
    }

    private void SpirteFiring()
    {
        Spirte.Play();
    }

    private void RaycastProcess()
    {
        RaycastHit hit;

        if (Physics.Raycast(FirstPersonCamera.transform.position, FirstPersonCamera.transform.forward, out hit, shootingRange))
        /// Raycast for shooting in game, need: shooter position, direction, a object of RaycastHit. Range is optional.
        /// "out" keyword means that object doesn't need to be initialize, used to store something when Method process is end.
        /// If and only if shooting something, not null(hit to skybox), then do something.
        {
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            /// Get enemy target through RaycastHit (hit), and if it's enemy (object with EnemyHealth component), then do something.

            ImpactEffect(hit);
            if (target)
                target.TakeDamage(weaponDamage);

            Debug.Log(name + " hit: " + hit.transform.name);
        }
        else
            /// If shooting nothing like the skybox, then return shooting Method. 
            return;
    }

    private void ImpactEffect(RaycastHit hit)
    {
        GameObject impact = Instantiate(FireImpact, hit.point, Quaternion.LookRotation(hit.normal));
        /// Instantiate: clone object to the postition which you point(by raycast)
        /// Quaternion.LookRotation: make object rotate to fit where it's.
        Destroy(impact, .1f);
        /// Destory the impact object, make it look like effect
    }
}
