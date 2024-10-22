using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject destroyedVFX;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public float Speed = 2f;

    public int hp;
    public int maxHp = 1;

    void OnEnable()
    {
        hp = maxHp;
    }

    private void OnDisable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;

        originalColor = spriteRenderer.color; // Store the original color
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                //Player take damge
                Debug.Log("Player Kena Damage");
            }

            //TODO : Harus diganti dengan object pooler
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Projectile"))
        {
            TakeDamage();
            if (!collision.GetComponent<Projectile>().isPiercing)
            {
                //TODO : Harus diganti dengan object pooler
                Destroy(collision.gameObject);
            }
        }
    }

    private void TakeDamage()
    {
        hp--;
        if (hp <= 0)
        {
            //TODO : VFX harus dimasukin ke pooler juga
            Instantiate(destroyedVFX, gameObject.transform.position, Quaternion.identity);
            Player.instance.UpdatePower(1);
            EnemyPooler.instance.ReturnPooledObject(gameObject);
        }
        else
        {
            StartCoroutine(ChangeColor());
        }
    }

    private IEnumerator ChangeColor()
    {
        // Change to white
        spriteRenderer.color = Color.white;

        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.05f);

        // Change back to the original color
        spriteRenderer.color = originalColor;
    }

    private void OnBecameInvisible()
    {
        //TODO : Pakai pooler
        EnemyPooler.instance.ReturnPooledObject(gameObject);
    }
}
