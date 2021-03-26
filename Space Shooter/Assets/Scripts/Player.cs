using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // config
    [Header("Player")]
    [SerializeField] float move_speed = 10f;
    [SerializeField] float padding = 0f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectile_vel = 10f;
    [SerializeField] float projectile_firing_period = 0.1f;

    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.1f;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0.1f;
    [SerializeField] AudioClip takeHitSFX;
    [SerializeField] [Range(0, 1)] float takeHitSFXVolume = 0.1f;

    Coroutine firingCoroutine;

    float x_min, x_max;
    float y_min, y_max;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectile_vel);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
            yield return new WaitForSeconds(projectile_firing_period);
        }    
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * move_speed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * move_speed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, x_min, x_max);       
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, y_min, y_max);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        x_min = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        x_max = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        y_min = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        y_max = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
        else
        {
            AudioSource.PlayClipAtPoint(takeHitSFX, Camera.main.transform.position, takeHitSFXVolume);
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);       
    }

    public int GetHealth()
    {
        return health;
    }
}
