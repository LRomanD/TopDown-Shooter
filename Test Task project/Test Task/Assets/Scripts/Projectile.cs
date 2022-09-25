using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage = 100;
    public Rigidbody2D rb;
    public GameObject hitEffect;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        Player player = hitInfo.GetComponent<Player>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            DestroyProjectile();
            
        }
        else if (player != null)
        {
            player.TakeDamage(damage);
            DestroyProjectile();
        }

        if (hitInfo.CompareTag("Horizontal"))
        {
            Vector2 reflectDir = Vector2.Reflect(rb.transform.position.normalized, hitInfo.transform.position.normalized);
            float rot = 30 - (Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg);
            Debug.Log(rot);
            rb.transform.eulerAngles = new Vector3(0, 0, rot);
            speed *= 0.75f;
            rb.velocity = (rb.transform.up * Time.deltaTime * speed);
            return;
        }
        else if (hitInfo.CompareTag("Vertical"))
        {
            Vector2 reflectDir = Vector2.Reflect(rb.transform.position.normalized, hitInfo.transform.position.normalized);
            float rot = 270 - (Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg);
            Debug.Log(rot);
            rb.transform.eulerAngles = new Vector3(0, 0, rot);
            speed *= 0.75f;
            rb.velocity = (rb.transform.up * Time.deltaTime * speed);
            return;
        }
        Debug.Log(hitInfo.name);
    }

    void Start()
    {
        Invoke("DestroyProjectile", lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    void DestroyProjectile()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.2f);
        Destroy(gameObject);
    }
}
