using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 10;
    private bool playInRange;
    private PlayerHealth playerHealth;
    private float timer;
    private float timeBetweenAttacks = 0.5f;

    private Animator enemyAnimator;
    private bool playerIsDeath = false;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyAnimator = GetComponent<Animator>();
    }

    private void playerDeathAction()
    {
        this.playerIsDeath = true;
        enemyAnimator.SetTrigger("playIsDead");
        GetComponent<EnemyMove>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
    }

    private void OnEnable()
    {
        PlayerHealth.PlayerDeathEvent += playerDeathAction;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerDeathEvent -= playerDeathAction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerHealth.tag)
        {
            playInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == playerHealth.tag)
        {
            playInRange = false;
        }
    }

    private void Attack()
    {
        timer = 0;
        playerHealth.TakeDamage(attackDamage);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (playInRange)
        {
            if (timer >= timeBetweenAttacks && playerIsDeath == false)
            {
                Attack();
            }
        }
    }
}
