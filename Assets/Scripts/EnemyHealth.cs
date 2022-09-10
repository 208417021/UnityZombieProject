using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float enemyHealth = 100f;
    

    public void TakeDamage(int damage)
    {
        if (enemyHealth - damage <= 0)
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
        else
            enemyHealth -= damage;

        Debug.LogError("Health: " + enemyHealth);
    }
}
