using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 2f;

    public int hp;
    public int maxHp = 1;

    void OnEnable()
    {
        hp = maxHp;
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
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

            //TODO : Harus diganti dengan object pooler
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage()
    {
        hp--;
        if (hp <= 0)
        {
            //TODO : Muncul destroy vfx
            gameObject.SetActive(false);
        }
    }
}
