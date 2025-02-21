// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;

// public class BearAI : MonoBehaviour
// {
//     public float detectionRadius = 10f;
//     public float attackRange = 2f;
//     public float attackCooldown = 1.5f;
//     public int attackDamage = 10;
    
//     private NavMeshAgent agent;
//     private GameObject target;
//     private bool canAttack = true;
//     private Animator animator;

//     void Start()
//     {
//         agent = GetComponent<NavMeshAgent>();
//         animator = GetComponentInChildren<Animator>(); // Assumes Animator is on the child model
//     }

//     void Update()
//     {
//         FindNearestUnit();

//         if (target != null)
//         {
//             float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

//             if (distanceToTarget > attackRange)
//             {
//                 MoveToTarget();
//             }
//             else
//             {
//                 AttackTarget();
//             }
//         }
//         else
//         {
//             animator.SetBool("isMoving", false); // Stop moving if no target
//         }
//     }

//     void FindNearestUnit()
//     {
//         GameObject[] units = GameObject.FindGameObjectsWithTag("Unit"); // Ensure all units have the "Unit" tag
//         float shortestDistance = detectionRadius;
//         GameObject nearestUnit = null;

//         foreach (GameObject unit in units)
//         {
//             float distance = Vector3.Distance(transform.position, unit.transform.position);
//             if (distance < shortestDistance)
//             {
//                 shortestDistance = distance;
//                 nearestUnit = unit;
//             }
//         }

//         target = nearestUnit;
//     }

//     void MoveToTarget()
//     {
//         if (target != null)
//         {
//             agent.SetDestination(target.transform.position);
//             animator.SetBool("isMoving", true);
//         }
//     }

//     void AttackTarget()
//     {
//         if (canAttack)
//         {
//             animator.SetTrigger("Attack");
//             StartCoroutine(AttackCooldown());
//         }
//     }

//     IEnumerator AttackCooldown()
//     {
//         canAttack = false;
//         yield return new WaitForSeconds(attackCooldown);
//         canAttack = true;
//     }

//     // Called by animation event
//     public void DealDamage()
//     {
//         if (target != null)
//         {
//             target.GetComponent<UnitHealth>().TakeDamage(attackDamage);
//         }
//     }
// }