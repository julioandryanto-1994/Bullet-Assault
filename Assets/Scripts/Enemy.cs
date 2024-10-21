using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int hp;
    public int maxhp = 1;

    private void OnEnable()
    {
        hp = maxhp;
    }
    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                //Player take damage
                Debug.Log("Player Kena Damage");
            }

            //TODO : Harus diganti dengan object pooler
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Projectile"))
        {
            TakeDamage();

            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage()
    {
        hp--;
        if(hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
