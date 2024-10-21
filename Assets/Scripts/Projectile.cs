using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 10f;
    private Rigidbody2D rb;
    private ObjectPooler pooler;

    private void OnEnable()
    {
        pooler = FindObjectOfType<ObjectPooler>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.up * Speed;
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
            pooler.ReturnPooledObject(gameObject);
        }
    }
}
