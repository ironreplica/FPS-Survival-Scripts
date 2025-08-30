using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDataPersistence
{
    // This script controls the players health, damage, and respawning
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Animator screenEffect;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject deathScreen;
    private float curHealth;
    void Start()
    {
        healthSlider.value = curHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float getHealth()
    {
        return curHealth;
    }
    public float getMaxHealth()
    {
        return maxHealth;
    }
    public void TakeDamage(float damage)
    {
        curHealth -= damage;
        healthSlider.value = curHealth;
        if(curHealth <= 0)
        {
            /*deathScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;*/
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        /*screenEffect.gameObject.SetActive(true);
        screenEffect.SetTrigger("TakeDamage");*/
    }
    public void AddHealth(float health)
    {
        curHealth += health;
        healthSlider.value = curHealth;
    }

    public void LoadData(GameData data)
    {
        this.curHealth = data.playerHealth;
    }

    public void SaveData(ref GameData data)
    {
        data.playerHealth = this.curHealth;
    }
}
