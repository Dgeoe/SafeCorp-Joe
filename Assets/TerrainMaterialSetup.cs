using UnityEngine;

public class TerrainMaterialSetup : MonoBehaviour
{
    private void Awake()
    {
        ApplyMaterialToTerrainCollider();
    }

    private void ApplyMaterialToTerrainCollider()
    {
        // Get the TerrainCollider component
        TerrainCollider terrainCollider = GetComponent<TerrainCollider>();

        if (terrainCollider != null)
        {
            // Create a new PhysicMaterial
            PhysicMaterial terrainMaterial = new PhysicMaterial("GroundMaterial")
            {
                frictionCombine = PhysicMaterialCombine.Average, // Average friction
                bounceCombine = PhysicMaterialCombine.Minimum,  // No bouncing
                dynamicFriction = 0.6f,                         // Moderate friction
                staticFriction = 0.6f                           // Moderate friction
            };

            // Apply the material to the TerrainCollider
            terrainCollider.material = terrainMaterial;

            Debug.Log("Physics Material created and applied to TerrainCollider: " + terrainMaterial.name);
        }
        else
        {
            Debug.LogError("No TerrainCollider found on this GameObject.");
        }
    }
}