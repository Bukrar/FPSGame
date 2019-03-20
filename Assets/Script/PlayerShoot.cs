using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public int damage = 20;
    public float range = 100f;
    private Ray ShootRay;
    private RaycastHit shootHit;
    private int enemyMask;
    private Light gunLight;
    private ParticleSystem gunParticle;
    private AudioSource gunAudioSource;
    private LineRenderer gunLineRenderer;
    public float timeBetweenBullets = 0.15f;
    private float effectsDisplayTime = 0.2f;
    float time;

    private void Awake()
    {
        enemyMask = LayerMask.GetMask("Enemy");
        gunParticle = GetComponent<ParticleSystem>();
        gunLineRenderer = GetComponent<LineRenderer>();
        gunAudioSource = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    private void Shoot()
    {
        time = 0f;
        gunAudioSource.Play();
        gunLight.enabled = true;
        gunParticle.Stop();
        gunParticle.Play();

        gunLineRenderer.enabled = true;
        gunLineRenderer.SetPosition(0, transform.position);
        ShootRay.origin = transform.position;
        ShootRay.direction = transform.forward;
        if (Physics.Raycast(ShootRay, out shootHit, range, enemyMask))
        {           
            EnemyHealth enemyHealth =
                shootHit.collider.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage,shootHit.point);
            gunLineRenderer.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLineRenderer.SetPosition(1, ShootRay.origin + ShootRay.direction * range);
        }
    }

    void DisableEffects()
    {
        gunLight.enabled = false;
        gunLineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && time >= timeBetweenBullets)
        {
            Shoot();
        }
        if (time >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }
}
