using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    public int attackDamage = 10;
    private bool playInRange;
    private PlayerHealth playerHealth;
    private float timer;
    private float timeBetweenAttacks = 0.5f;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
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
            if (timer >= timeBetweenAttacks)
            {
                Attack();
            }
        }
    }
}
