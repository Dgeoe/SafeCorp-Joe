using System.Collections.Generic;
using UnityEngine;

public class TreeResource : MonoBehaviour
{
    private void OnMouseDown()  
    {
        if (Input.GetMouseButtonDown(1)) // Right-click
        {
            List<GameObject> selectedUnits = UnitSelectionManager.Instance.unitsSelected;
            
            if (selectedUnits.Count > 0)
            {
                foreach (GameObject unit in selectedUnits)
                {
                    UnitGatherer gatherer = unit.GetComponent<UnitGatherer>();
                    if (gatherer != null)
                    {
                        gatherer.MoveToTree(this.transform.position);
                    }
                }
            }
        }
    }
}
