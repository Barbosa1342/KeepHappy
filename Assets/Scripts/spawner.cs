using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class spawner : MonoBehaviour
{
    [SerializeField] GameObject objectToPool;
    [SerializeField] int amountToPool;

    List<GameObject> pooledObjects = new List<GameObject>();
    float camHeight;
    float camWidth;
    void Start()
    {
        pooledObjects = objectPool.SetPool(objectToPool, amountToPool);

        var cam = Camera.main;
        camHeight = cam.orthographicSize;
        camWidth = cam.orthographicSize * cam.aspect;

        StartCoroutine(Attack());
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            StartCoroutine(AttackOne());
        }
    }

    GameObject SpawnObject(Vector2 position, int attackType)
    {
        GameObject objectToSpawn = objectPool.GetPooledObject(pooledObjects);

        if (objectToSpawn != null)
        {
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = Quaternion.identity;
            objectToSpawn.GetComponent<attackMovement>().attackType = attackType;
            objectToSpawn.SetActive(true);
        }

        return objectToSpawn;
    }
    GameObject SpawnObject(Vector2 position, bool isTop, int attackType = 1)
    {
        GameObject objectToSpawn = objectPool.GetPooledObject(pooledObjects);

        if (objectToSpawn != null)
        {
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = Quaternion.identity;
            objectToSpawn.GetComponent<attackMovement>().attackType = attackType;
            objectToSpawn.GetComponent<attackMovement>().top = isTop;
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
            Vector2 pos = new Vector2(x, y);

            SpawnObject(pos, 0);

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
            GameObject lastObjectSpawned = SpawnObject(pos, isTop, 1);

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
}
