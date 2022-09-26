using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int health = 100;
    public float speed = 5f;

    public Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;

    public GameObject projectile;
    public Transform firePoint;

    private float reload;
    public float reloadTIme = 1f;

    public GameObject deathEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Moving();

        Rotating();

        if (reload <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(projectile, firePoint.position, firePoint.rotation);
                reload = reloadTIme;

            }
        }
        else reload -= Time.deltaTime;

        if (movement.x == 0 && movement.y == 0) anim.SetBool("IsMoving", false);
        else anim.SetBool("IsMoving", true);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        gameObject.active = false;
        //Destroy(gameObject);
        Destroy(effect, 0.5f);
        UIScore score = new UIScore();
        Enemy enemy = new Enemy();
        score.RewriteData(1, 0);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Main");
        //Application.LoadLevel(1);
    }

    void Moving()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
    }

    void Rotating()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }
}
