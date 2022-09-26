using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    public GameObject projectile;
    public Transform firePoint;

    private float reload = 0.3f;
    public float reloadTime;

    public Transform target;
    public GameObject deathEffect;

    void Update()
    {
        Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        transform.up = direction;
        float distance = Vector2.Distance(transform.position, target.position);
        Ray2D ray = new Ray2D(transform.position, target.position);

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.up, distance);

        if (hit.collider != null && hit.collider.name == "Player")
        {
            if (reload <= 0)
            {
                
                Instantiate(projectile, firePoint.position, firePoint.rotation);
               reload = reloadTime;
                }
                else reload -= Time.deltaTime;
            }
        Debug.DrawRay(transform.position, target.transform.position);
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
        Destroy(gameObject);
        Destroy(effect, 0.5f);
        UIScore score = new UIScore();
        Player player = new Player();
        score.RewriteData(0, 1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Main");
    }
}
