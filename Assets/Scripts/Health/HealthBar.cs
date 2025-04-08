using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarSprite;
    [SerializeField] private Health health;

    private void Start()
    {
        health = GetComponentInParent<Health>();

        if (health == null)
        {
            Debug.Log("No Health obect in parent of healthbar");
        } else
        {
            health.OnIncrementHealth += UpdateHealthBar;
            health.OnDecrementHealth += UpdateHealthBar;
        }
    }

    public void UpdateHealthBar()
    {
        healthBarSprite.fillAmount = health.currentHealth / health.maxHealth;
    }
}
