using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public float delayTime = 5f;
    public float repeatRate = 3f;
    public Transform[] spawnPoint;
    private bool playerIsDead = false;

    private void playerDeathAction()
    {
        playerIsDead = true;
    }

    private void OnEnable()
    {
        PlayerHealth.PlayerDeathEvent += playerDeathAction;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerDeathEvent -= playerDeathAction;
    }

    private void Spawn()
    {
        if (playerIsDead)
        {
            CancelInvoke("Spawn");
            return;
        }
        int pointIndex = Random.Range(0, spawnPoint.Length);
        Instantiate(enemy, spawnPoint[pointIndex].position,
            spawnPoint[pointIndex].rotation);
    }

    void Start()
    {
        InvokeRepeating("Spawn", delayTime, repeatRate);
    }
}
