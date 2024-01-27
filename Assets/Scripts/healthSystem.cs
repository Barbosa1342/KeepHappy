using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public int MaxHealth => (int)maxHealth;

    float currentHealth;
    public float CurrentHealth => currentHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float amount)
    {
        currentHealth += amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Enemy"))
        {
            ChangeHealth(-1);
        }
    }
}
