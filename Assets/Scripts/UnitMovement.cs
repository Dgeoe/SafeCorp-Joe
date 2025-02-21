using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    private Camera cam;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isAttacking = false; // Track if the unit is currently attacking
    public float attackRange = 2f; // Distance to start attacking
    public UnitSelectionManager selectionManager;
   public GameObject currentTarget;


    private void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        if (selectionManager == null)
        {
            selectionManager = FindObjectOfType<UnitSelectionManager>();  // Find it if not assigned
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && UnitSelectionManager.Instance.unitsSelected.Count > 0) // Right-click while units are selected
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"))) // Check if clicked on the ground
            {
                MoveUnitsToPosition(hit.point); // Move selected units
                
            }
            
            
        }
        
    }

    // Move all selected units to a target position
    private void MoveUnitsToPosition(Vector3 targetPosition)
    {
        foreach (var unit in UnitSelectionManager.Instance.unitsSelected)
        {
            NavMeshAgent unitAgent = unit.GetComponent<NavMeshAgent>();
            Animator unitAnimator = unit.GetComponentInChildren<Animator>();

            unitAgent.SetDestination(targetPosition);  // Move the unit
            unitAnimator.SetBool("isMoving", true); // Set the moving animation
        }
    }

    
    
   
}
   
