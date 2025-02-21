using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class UnitWorker : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // This method is called to start chopping the tree
    public void StartChopping(GameObject tree)
    {
        StartCoroutine(ChopTree(tree));
    }

    private IEnumerator ChopTree(GameObject tree)
    {
        while (Vector3.Distance(transform.position, tree.transform.position) > 2.0f)
        {
            yield return null; // Wait until unit reaches tree
        }

        agent.isStopped = true; // Stop moving
        animator.SetBool("isChopping", true); // Play chopping animation

        // Add any tree health reduction or destruction logic here
        Debug.Log($"{gameObject.name} is chopping the tree!");

        // Example: Destroy the tree after some time or after certain conditions (health depletion, etc.)
        // Destroy(tree);
    }

    // Call this method to stop chopping if needed
    public void StopChopping()
    {
        animator.SetBool("isChopping", false); // Stop the chopping animation
        agent.isStopped = false; // Resume movement
    }
}
