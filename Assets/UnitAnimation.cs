using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAnimation : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    private void Start()
    {
        animator = GetComponent<Animator>(); // Animator is on this GameObject (the model)
        agent = GetComponentInParent<NavMeshAgent>(); // Get NavMeshAgent from the parent
    }

    private void Update()
    {
        if (agent == null) return; // Safety check

       
        float speed = agent.velocity.magnitude;

        // Set the Speed parameter in the Animator based on the unit's velocity
        animator.SetFloat("Speed", speed);

        // Check if this unit is selected in the UnitSelectionManager
        bool isSelected = UnitSelectionManager.Instance.unitsSelected.Contains(gameObject);

        
    }
}
