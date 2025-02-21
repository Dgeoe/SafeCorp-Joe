using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();
    public LayerMask clickable;
    public LayerMask ground;
    public GameObject groundMarker;
    private Camera cam;

    public static UnitSelectionManager Instance { get; set;}
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else{
            Instance = this;
        }
        DeselectAll();
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectByClicking(hit.collider.gameObject);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift) == false)
                {
                    DeselectAll();
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && unitsSelected.Count > 0)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundMarker.transform.position = hit.point;
                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
            }
        }

        // Check if the Delete (Del) key is pressed
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            KillSelectedUnits();
        }
    }

    private void MultiSelect(GameObject unit)
    {
        if (!unitsSelected.Contains(unit))
        {
            unitsSelected.Add(unit);
            SelectUnit(unit, true);
        }
        else
        {
            SelectUnit(unit, false);
            unitsSelected.Remove(unit);
        }
    }

    public void DeselectAll()
    {
        foreach (var unit in unitsSelected)
        {
            SelectUnit(unit, false);
        }
        groundMarker.SetActive(false);
        unitsSelected.Clear();
    }

    private void SelectByClicking(GameObject unit)
    {
        DeselectAll();
        unitsSelected.Add(unit);
        SelectUnit(unit, true);
    }

    private void EnableUnitMovement(GameObject unit, bool shouldMove)
    {
        unit.GetComponent<UnitMovement>().enabled = shouldMove;
    }

    private void TriggerSelectionIndicator(GameObject unit, bool isVisable)
    {
        unit.transform.GetChild(0).gameObject.SetActive(isVisable);
    }

    internal void DragSelect(GameObject unit)
    {
        if (!unitsSelected.Contains(unit))
        {
            unitsSelected.Add(unit);
            SelectUnit(unit, true);
        }
    }

    private void SelectUnit(GameObject unit, bool isSelected)
    {
        // Find the "GroundMarker" child of the unit
        Transform marker = unit.transform.Find("GroundMarker");

        if (marker != null)
        {
            // Show or hide the marker based on selection status
            marker.gameObject.SetActive(isSelected);

            if (isSelected)
            {
                // Position the marker directly under the unit
                marker.position = unit.transform.position - new Vector3(0, 0.0f, 0);  // Adjust Y to be under the unit

                marker.rotation = Quaternion.Euler(180f, 0f, 0f);
            }
        }
        else
        {
            Debug.LogWarning("GroundMarker not found on " + unit.name);
        }

        // Enable or disable unit movement
        EnableUnitMovement(unit, isSelected);
    }

    // Method to kill selected units (play death animation and destroy them)
    private void KillSelectedUnits()
{
    foreach (var unit in unitsSelected)
    {
        if (unit != null)
        {
            UnitHealth unitHealth = unit.GetComponent<UnitHealth>();
            if (unitHealth != null)
            {
                unitHealth.Die();  // Call the Die method on the unit's health script
            }

           
            Animator animator = unit.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                // Trigger the death animation
                animator.SetBool("isDead", true);  // Set isDead to true to trigger the death animation

                
                animator.speed = 1; 
                animator.Play("Die01", 0, 0f); 
                
                // Set a callback when the animation reaches the last frame
                StartCoroutine(WaitForAnimationAndDestroy(unit, animator));
            }
        }
    }

    DeselectAll();  // Deselect all units after killing them
}

private IEnumerator WaitForAnimationAndDestroy(GameObject unit, Animator animator)
{
    // Wait until the animation reaches frame 24
    yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);  // Wait for the full animation to finish
    Destroy(unit);  // Destroy unit after animation
}
}

