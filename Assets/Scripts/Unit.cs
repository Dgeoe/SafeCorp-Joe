using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    void Start()
    {
       UnitSelectionManager.Instance.allUnitsList.Add(gameObject);
    }
    private void OnDestroy()
    {
       UnitSelectionManager.Instance.allUnitsList.Remove(gameObject); 
    }
    void Awake()
    {
      AddRigidbodyAndCollider();
    }
    private void AddRigidbodyAndCollider()
    {
        // Add Rigidbody if it doesn't already exist
        // if (GetComponent<Rigidbody>() == null)
        // {
        //     Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        //     rb.mass = 1f; // Customize mass
        //     rb.drag = 0.5f; // Customize drag
        //     Debug.Log("Rigidbody added to " + gameObject.name);
        // }

        // Add CapsuleCollider if it doesn't already exist
        if (GetComponent<CapsuleCollider>() == null)
        {
            CapsuleCollider collider = gameObject.AddComponent<CapsuleCollider>();
            collider.height = 2f; // Customize height
            collider.radius = 0.3f; // Customize radius
            collider.center = Vector3.zero; // Adjust center if necessary
            Debug.Log("CapsuleCollider added to " + gameObject.name);
        }
    }
}

