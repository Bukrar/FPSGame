using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startHealth = 100;
    public Slider healthSlider;
    private static int currentHealth;

    public AudioClip deathClip;
    public Image damgeImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1F, 0F, 0F);
    private bool damaged = false;
    private AudioSource playerAudio;

    private bool isDeath = false;
    private Animator playerAnimator;

    public delegate void PlayerDeathAction();
    public static event PlayerDeathAction PlayerDeathEvent;

    private void Awake()
    {
        healthSlider.maxValue = startHealth;
        if (currentHealth <= 0)
        {
            healthSlider.value = startHealth;
            currentHealth = startHealth;
        }
        else
        {
            healthSlider.value = startHealth;
        }

        playerAudio = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Death()
    {
        isDeath = true;
        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerAnimator.SetTrigger("Die");

        if (PlayerDeathEvent != null)
        {
            PlayerDeathEvent();
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDeath) return;
        playerAudio.Play();
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Update()
    {
        if (damaged)
        {
            damaged = false;
            damgeImage.color = this.flashColor;
        }
        else
        {
            damgeImage.color = Color.Lerp(damgeImage.color, Color.clear,
                Time.deltaTime * flashSpeed);
        }
    }
}
