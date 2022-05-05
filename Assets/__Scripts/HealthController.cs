using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private RectTransform healthBar;

    [SerializeField] private Text healthText;

    private float healthBarStartWidth;

    private float currentHealth;

    [SerializeField]
    private float maxHealth;

    private bool isDead;

    private Explode explode;

    [SerializeField] private float score;

    // Start is called before the first frame update
    void Start()
    {
        //get green panels width
        healthBarStartWidth = healthBar.sizeDelta.x;

        isDead = false;
        
        //set enemy to full health
        currentHealth = maxHealth;
        UpdateHealthUI();

        explode=GetComponent<Explode>();
    }

    public void ApplyDamage(float damage)
    {   
        if (isDead) return;

        //take away damage from health
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Score.Instance.SetScore(score);
            explode.TriggerExplosion();
        }

        UpdateHealthUI();
    }

    //Update Health on Ui
    private void UpdateHealthUI()
    {        
        float percentOutOf = (currentHealth / maxHealth) * 100;
        float newWidth = (percentOutOf/ 100f) * healthBarStartWidth;

        healthBar.sizeDelta = new Vector2(newWidth, healthBar.sizeDelta.y);
        healthText.text = currentHealth + "/" + maxHealth;
    }
}