using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] HealthPoint[] healthPoints;

    void Start()
    {
        SetMaxHealth(healthSystem.maxHealth);
        SetHealth(healthSystem.health);
        healthSystem.OnHealthChange.AddListener(SetHealth);
    }

    void SetHealth(float health)
    {
        int filled = (int)Mathf.Floor(health);
        int maxIndex = Mathf.Min(filled, healthPoints.Length);
        for (int i = 0; i < maxIndex; i++)
        {
            healthPoints[i].SetFill(1);
        }

        if (filled >= healthPoints.Length) return;

        healthPoints[filled].SetFill(health - filled);

        for (int i = filled + 1; i < healthPoints.Length; i++)
        {
            healthPoints[i].SetFill(0);
        }
    }

    void SetMaxHealth(float maxHealth)
    {
        int maxIndex = (int)Mathf.Min(healthPoints.Length, Mathf.Ceil(maxHealth));
        for (int i = 0; i < maxIndex; i++)
            healthPoints[i].gameObject.SetActive(true);

        for (int i = maxIndex; i < healthPoints.Length; i++)
            healthPoints[i].gameObject.SetActive(false);
    }
}
