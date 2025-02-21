using UnityEngine;
using UnityEngine.AI;

public class UnitGatherer : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private bool isChopping = false;
    private Transform targetTree;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void MoveToTree(Vector3 treePosition)
    {
        agent.SetDestination(treePosition);
        targetTree = null;
        isChopping = false;
    }

    private void Update()
    {
        if (targetTree != null && !isChopping)
        {
            float distance = Vector3.Distance(transform.position, targetTree.position);
            if (distance <= 1.5f) // Chopping range
            {
                StartChopping();
            }
        }
    }

    private void StartChopping()
    {
        isChopping = true;
        agent.isStopped = true;
        animator.SetTrigger("Attack"); // Make sure you have a "Chop" animation in Animator
    }
}
