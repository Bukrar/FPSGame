using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int score = 10;
    public int startHealth = 100;
    private int currentHealth;
    private Animator animator;
    private bool isDead;
    private bool isSinkling = false;
    public AudioClip deadClip;
    private AudioSource enemyAudio;
    private ParticleSystem hitParticle;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentHealth = startHealth;
        enemyAudio = GetComponent<AudioSource>();
        hitParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void Death()
    {
        isDead = true;
        animator.SetTrigger("isDead");
        enemyAudio.clip = deadClip;
        enemyAudio.Play();

        Score.score += 10;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyMove>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
    }

    public void TakeDamage(int amount, Vector3 postion)
    {
        if (isDead) return;
        currentHealth -= amount;
        enemyAudio.Play();
        hitParticle.transform.position = postion;
        hitParticle.Play();
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void StartSinking()
    {
        isSinkling = true;
        Destroy(gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSinkling)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }
    }
}
