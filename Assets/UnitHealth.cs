using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    private float health = 100f;

    // Call this method to simulate the unit's death
    public void Die()
    {
        health = 0f;
        // You can add additional logic for unit death, like playing sound effects, etc.
        Debug.Log(gameObject.name + " has died.");
    }
}
