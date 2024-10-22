using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 10f;
    public Vector2 Direction;
    public bool isPiercing;
    public bool isSlowing;
    public bool isExplosive;
    public SpriteRenderer spriteRenderer;
    public GameObject spriteRendererSlow;
    public TrailRenderer trailRenderer;

    private Rigidbody2D rb;

    private void OnEnable()
    {

        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        /*
        if (Direction != Vector2.zero)
        {
            rb.velocity = Direction * Speed;
        }
        else
        {
            rb.velocity = transform.up * Speed;
        }
        */
        rb.velocity = Direction != Vector2.zero ? Direction * Speed : transform.up * Speed;

        trailRenderer.enabled = isPiercing;

        spriteRendererSlow.SetActive(isSlowing);

    }

    private void OnDisable()
    {
        trailRenderer.enabled = false;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > Camera.main.orthographicSize)
        {
            ProjectilePooler.instance.ReturnPooledObject(gameObject);
        }
    }


    private void Explode(Vector3 explosionPosition)
    {
        // detect enemy on radius
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(explosionPosition, Player.instance.explosionRadius);

        foreach (Collider2D hit in hitEnemies)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(); // Apply damage to each enemy hit by the explosion
            }
        }
        // Spawn explosive vfx
        Instantiate(explosionVFX, explosionPosition, Quaternion.identity);
    }
}
