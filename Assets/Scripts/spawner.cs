using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject objectToPool;
    [SerializeField] int amountToPool;

    List<GameObject> pooledObjects = new();
    float camHeight;
    float camWidth;

    [SerializeField] GameObject player;
    void Start()
    {
        pooledObjects = ObjectPool.SetPool(objectToPool, amountToPool);

        var cam = Camera.main;
        camHeight = cam.orthographicSize;
        camWidth = cam.orthographicSize * cam.aspect;

        StartCoroutine(Attack());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(AttackOne());
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(AttackTwo());
        }
    }

    GameObject SpawnObject(Vector2 position, int attackType, Vector2 dir)
    {
        GameObject objectToSpawn = ObjectPool.GetPooledObject(pooledObjects);

        if (objectToSpawn != null)
        {
            objectToSpawn.transform.SetPositionAndRotation(position, Quaternion.identity);

            AttackMovement attackScript = objectToSpawn.GetComponent<AttackMovement>();
            attackScript.dir = dir;
            attackScript.attackType = attackType;

            objectToSpawn.SetActive(true);
        }

        return objectToSpawn;
    }

    GameObject SpawnObject(Vector2 position, int attackType = 1, bool isTop = true)
    {
        GameObject objectToSpawn = ObjectPool.GetPooledObject(pooledObjects);

        if (objectToSpawn != null)
        {
            objectToSpawn.transform.SetPositionAndRotation(position, Quaternion.identity);

            AttackMovement attackScript = objectToSpawn.GetComponent<AttackMovement>();
            attackScript.top = isTop;
            attackScript.attackType = attackType;

            objectToSpawn.SetActive(true);
        }

        return objectToSpawn;
    }

    IEnumerator Attack()
    {
        while (true)
        {
            // There are 4 blocks to spawn the attack
            // Top, Bottom, Right and Left
            // The Spawn Point sound complex to do at one range
            // This way i'm breaking the problem down
            int choice = Random.Range(1, 5); // 1 to 4

            float x, y;
            if (choice == 1)
            {
                // Top
                x = Random.Range(-camWidth, camWidth);
                y = Random.Range(camHeight, camHeight + 5);
            }
            else if (choice == 2)
            {
                // Bottom
                x = Random.Range(-camWidth, camWidth);
                y = Random.Range(-camHeight - 5, -camHeight);
            }
            else if (choice == 3)
            {
                // Right
                x = Random.Range(camWidth, camWidth + 5);
                y = Random.Range(-camHeight, camHeight);
            }
            else
            {
                // Left
                x = Random.Range(-camWidth - 5, -camWidth);
                y = Random.Range(-camHeight, camHeight);
            }
            Vector2 pos = new (x, y);

            Vector2 playerPos = new (player.transform.position.x, player.transform.position.y);
            Vector2 dir = new Vector2(playerPos.x - pos.x, playerPos.y - pos.y).normalized;

            _ = SpawnObject(pos, 0, dir);

            float waitTime = Random.Range(1.5f, 2.5f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator AttackOne()
    {
        int choice = Random.Range(0, 2); // 0 or 1
        bool isTop;
        Vector2 pos;

        if (choice == 0)
        {
            isTop = true;
            pos = new Vector2(camWidth, camHeight);
        }
        else
        {
            isTop = false;
            pos = new Vector2(camWidth, -camHeight);
        }

        for (int numEnemy = 0; numEnemy < 20; numEnemy++)
        {
            GameObject lastObjectSpawned = SpawnObject(pos, 1, isTop);

            BoxCollider2D box = lastObjectSpawned.GetComponent<BoxCollider2D>();
            float dis = camWidth - lastObjectSpawned.transform.position.x;

            bool distanceToBorder = (dis > box.size.x * (1.1));
            while (!distanceToBorder)
            {
                dis = camWidth - lastObjectSpawned.transform.position.x;
                distanceToBorder = (dis > box.size.x * (1.1));
                yield return null;
            }
        }
    }

    IEnumerator AttackTwo()
    {
        int choice = Random.Range(1, 5); // 1 to 4

        float x, y;
        Vector2 spawnPos;

        if (choice == 1)
        {
            // Top
            x = 0;
            y = camHeight;
        }
        else if (choice == 2)
        {
            // Bottom
            x = 0;
            y = -camHeight;
        }
        else if (choice == 3)
        {
            // Right
            x = camWidth;
            y = 0;
        }
        else
        {
            // Left
            x = -camWidth;
            y = 0;
        }
        spawnPos = new Vector2(x, y);
        

        for (int i = 0; i <= 10; i++)
        {
            float a = Random.Range(-1f, 1f);
            float b = Random.Range(0f, 1f);
            Vector2 moveDir;

            if (choice == 1)
            {
                // Top
                moveDir = new Vector2(a, -b);
            }
            else if (choice == 2)
            {
                // Bottom
                moveDir = new Vector2(a, b);
            }
            else if (choice == 3)
            {
                // Right
                moveDir = new Vector2(-b, a);
            }
            else
            {
                // Left
                moveDir = new Vector2(b, a);
            }
            moveDir = moveDir.normalized;

            _ = SpawnObject(spawnPos, 2, moveDir);

            yield return null;
        }
        
    }
}
