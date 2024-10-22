using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Singleton  : Syaratnya object ini harus tunggal
    public static Player instance;

    //Movement
    private Vector3 offset;
    private bool dragging = false;
    private Rigidbody2D rb;

    //Shoot
    public ObjectPooler projectilePooler;
    public Transform shootPoint;
    public float shootRate = 0.5f;
    private float shootTimer;

    // Spread shooting parameters
    public int spreadCount = 3; // Number of projectiles to shoot
    public float spreadAngle = 15f; // Angle between projectiles

    //Power Up
    [SerializeField] private Image imgPowerUp;
    public float power = 0;
    public float maxPower = 5;

    //Simple Roguelike
    [Header("Runtime")]
    [SerializeField] private bool isSpreadShoot;
    [SerializeField] private bool isPiercingShoot;
    [SerializeField] private bool isSlowBullet;

    //Explosion
    [SerializeField] private bool isExplosive;
    [SerializeField] public float explosionRadius = 2f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        UpdatePowerUpUI();
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
            if (!isSpreadShoot)
            {
                Shoot();
            }
            else
            {
                SpreadShoot();
            }

            shootTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Check for space key to trigger spread shooting
        {
            RapidFire();
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
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.isPiercing = isPiercingShoot;
            projectileScript.isSlowing = isSlowBullet;
            projectileScript.isExplosive = isExplosive;


            projectile.SetActive(true);
        }
    }

    private void SpreadShoot()
    {
        // Spread shooting logic
        float angleStep = spreadAngle / (spreadCount - 1);
        float startAngle = -spreadAngle / 2;

        for (int i = 0; i < spreadCount; i++)
        {
            GameObject projectile = projectilePooler.GetPoolObject();
            if (projectile != null)
            {
                projectile.transform.position = shootPoint.position;

                // Calculate the rotation based on the angle
                float angle = startAngle + (i * angleStep);
                projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up; // Calculate direction from angle

                Projectile projectileScript = projectile.GetComponent<Projectile>();
                projectileScript.Direction = direction;
                projectileScript.isPiercing = isPiercingShoot;
                projectileScript.isSlowing = isSlowBullet;
                projectileScript.isExplosive = isExplosive;


                projectile.SetActive(true);
            }
        }
    }

    private void GaindSpreadShoot()
    {
        isSpreadShoot = true;
    }

    private void RapidFire()
    {
        shootRate -= 0.05f;
    }

    private void PiercingBullet()
    {
        isPiercingShoot = true;
    }

    private void SlowBullet()
    {
        isSlowBullet = true;
    }

    public void UpdatePower(int number)
    {
        power += number;
        if (power > maxPower)
        {
            power = 0;
            //UIManager.Instance.PnlSkillSelection.SetActive(true);
        }
        UpdatePowerUpUI();
    }

    private void UpdatePowerUpUI()
    {
        imgPowerUp.fillAmount = power / maxPower;
    }

    private void ExplosiveBullet()
    {
        isExplosive = true;
    }

    public void PowerUp(int skillIndex)
    {
        int randomNumber = Random.Range(0, 5);
        maxPower++;
        switch (randomNumber)
        {
            case 0:
                GaindSpreadShoot();
                break;
            case 1:
                RapidFire();
                break;
            case 2:
                SpreadShoot();
                break;
            case 3:
                PiercingBullet();
                break;
            case 4:
                SlowBullet();
                break;
            case 5:
                ExplosiveBullet();
                break;
            default:
                break;
        }

        //UIManager.Instance.PnlSkillSelection.SetActive(false);
    }
}
