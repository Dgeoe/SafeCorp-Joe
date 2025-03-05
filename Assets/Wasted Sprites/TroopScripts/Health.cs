using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private StatScript statScript;

    [SerializeField] private float hp; 
    private float damageReduction;

    // Start is called before the first frame update
    void Start()
    {
        statScript = GetComponent<StatScript>();

        if (statScript == null)
        {
            Debug.LogError("No StatScript found on this object!");
        }

        hp = statScript.health;
        damageReduction = statScript.dmgRed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTakeDamage(float dmg, float ArmorPiercing, string ID)
    {
        Debug.Log("DMG TAKEN!");
        float Armor = damageReduction - ArmorPiercing;
        if (Armor < 0 )
        {
            Armor = 0;
            hp = hp - dmg;
            Debug.Log($"{ID} hit {gameObject.name} for {dmg} damage!");
        }
        else 
        {
            float DealtDmg = dmg - Armor;
            hp = hp - DealtDmg;
            Debug.Log($"{ID} hit {gameObject.name} for {DealtDmg} damage!");
        }

        if (hp <= 0)
        {
            Die();
        }

    
    }
    private void Die()
    {
        Debug.Log(statScript.ID + " has been destroyed!");
        Destroy(gameObject);
    }
}
