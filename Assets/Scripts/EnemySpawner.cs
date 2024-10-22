using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefab; 
    public float spawnerInterval = 2f;

    [SerializeField] private int currentWave = 0;
    [SerializeField] private int enemyToSpawn = 0;
    [SerializeField] private int baseEnemyHp = 1;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            currentWave++;
            enemyToSpawn++;

            int enemyHp = baseEnemyHp;
            if (currentWave % 20 == 0)
            {
                baseEnemyHp++;
                enemyToSpawn = 1;
            }

            for (int i = 0; i < enemyToSpawn; i++)
            {
                yield return new WaitForSeconds(0.2f);
                //TODO : harus ganti ke object pooler
                GameObject enemy = Instantiate(prefab);

                if (enemy != null)
                {
                    float xPosition = Random.Range(Camera.main.ViewportToWorldPoint(new Vector3(0.2f, 0f, 0f)).x, Camera.main.ViewportToWorldPoint(new Vector3(0.8f, 0f, 0f)).x);

                    float yPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 1f;

                    //Set Postiion
                    enemy.transform.position = new Vector3(xPosition, yPosition, 0f);
                    //Set Hp
                    //TODO : Harus cari cara untuk tidak terlalu menggunakan GetComponent (Bisa explore pakai Interface)
                    enemy.GetComponent<Enemy>().maxHp = enemyHp;
                    enemy.GetComponent<Enemy>().hp = enemyHp;

                }
            }

            yield return new WaitForSeconds(spawnerInterval);
        }
    }
}
