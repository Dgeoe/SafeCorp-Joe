using UnityEngine;

public class SimpleEnemyFollow : MonoBehaviour
{
    public float detectionRadius = 10f; // Detection radius
    public float moveSpeed = 3f; // Movement speed to follow the player
    public Transform target; // The unit to follow

    private void Update()
    {
        if (target != null)
        {
            // Move towards the target (unit)
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the object that entered the trigger is a unit, start evaluating the closest one
        if (other.CompareTag("Unit"))
        {
            // Find the closest unit within the radius
            EvaluateTarget();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Continuously evaluate the closest unit if there are multiple units in range
        if (other.CompareTag("Unit"))
        {
            EvaluateTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Stop following if the target leaves the detection range
        if (other.CompareTag("Unit"))
        {
            target = null; // Clear the target
        }
    }

    private void EvaluateTarget()
    {
        // Get all colliders within the detection radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        float closestDistance = Mathf.Infinity;
        Transform closestUnit = null;

        foreach (var collider in colliders)
        {
            // Check if the collider is a player unit
            if (collider.CompareTag("Unit"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                
                // If this unit is closer than the previous one, set it as the target
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestUnit = collider.transform;
                }
            }
        }

        // If we found a closest unit, set it as the target
        if (closestUnit != null)
        {
            target = closestUnit;
        }
    }
}
