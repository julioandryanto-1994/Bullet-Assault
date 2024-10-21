using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movement
    private Vector3 offset;
    private bool dragging = false;
    private Rigidbody2D rb;

    //Shoot
    public ObjectPooler projectilePooler;
    public Transform shootPoint;
    public float shootRate = 0.5f;
    private float shootTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //PC or Editor
        if (Input.GetMouseButton(0))
        { 
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

            if (Input.GetMouseButtonDown(0) && IsTouchingObject(mousePos))
            {
                dragging = true;
                offset = transform.position - mousePos;
            }

            if (dragging)
            {
                MovePlayer(mousePos + offset);
            }

            if (Input.GetMouseButtonUp(0))
            { 
                dragging = false;
            }
        }

        shootTimer += Time.deltaTime;
        if (shootTimer >= shootRate)
        {
            Shoot();
            shootTimer = 0;
        }
    }

    private void MovePlayer(Vector3 targetPosition)
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        float minX = -screenBounds.x + 0.5f;
        float maxX = screenBounds.x - 0.5f;
        float minY = -screenBounds.y + 0.5f;
        float maxY = screenBounds.y - 0.5f;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        rb.MovePosition(targetPosition);
    }

    private bool IsTouchingObject(Vector3 position)
    {
        Vector2 touchPosition2D = new Vector2(position.x, position.y);
        RaycastHit2D hit = Physics2D.Raycast(touchPosition2D, Vector2.zero);
        return hit.collider != null && hit.collider.transform == this.transform;
    }

    private void Shoot()
    {
        GameObject projectile = projectilePooler.GetPoolObject();
        if (projectile != null)
        {
            projectile.transform.position = shootPoint.position;
        }
    }
}
