using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [Header("Gun Settings")]
    public Transform firePoint; 
    public LayerMask enemyLayer; // Define enemy layer for raycast detection
    private AudioSource audioSource;  
    public AudioClip shoot;      

    private StatScript statScript;
    private float nextFireTime = 0f;

    private void Start()
    {
        statScript = GetComponent<StatScript>();
        audioSource = GetComponent<AudioSource>();

        if (statScript == null)
        {
            Debug.LogError("No StatScript found on this object!");
        }
    }

    private void Update()
    {
        // Check for enemies and attack when possible
        FindAndAttackEnemy();
    }

    private void FindAndAttackEnemy()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, statScript.range, enemyLayer);

        if (enemiesInRange.Length > 0)
        {
            Transform closestEnemy = GetClosestEnemy(enemiesInRange);
            if (closestEnemy != null && Time.time >= nextFireTime)
            {
                Shoot(closestEnemy);
                nextFireTime = Time.time + (1f / statScript.attackSpeed); // Control fire rate
            }
        }
    }

    private Transform GetClosestEnemy(Collider[] enemies)
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }
        return closest;
    }

    private void Shoot(Transform enemy)
    {
        Vector3 direction = (enemy.position - firePoint.position).normalized;
        Debug.DrawRay(firePoint.position, direction * statScript.range, Color.red, statScript.attackSpeed);
        // Simulate shooting with raycast
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, direction, out hit, statScript.range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                ShootSFX();
                Debug.Log("HIT");
                Health enemyHealth = hit.collider.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.OnTakeDamage(statScript.dmg, statScript.armorPiercing, statScript.ID);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw turret range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, statScript != null ? statScript.range : 0);
    }

    private void ShootSFX()
    {
        float minPitch = 0.8f;    
        float maxPitch = 1.2f;

        if (audioSource != null && shoot != null)
        {
            audioSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch); 
            audioSource.PlayOneShot(shoot); 
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }    

    }
}

