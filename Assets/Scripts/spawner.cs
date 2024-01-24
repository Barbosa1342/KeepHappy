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

    public static bool isTop;
    void Start()
    {
        pooledObjects = objectPool.SetPool(objectToPool, amountToPool);

        var cam = Camera.main;
        camHeight = cam.orthographicSize;
        camWidth = cam.orthographicSize * cam.aspect;

    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            StartCoroutine(Attack());
        }
    }

    GameObject SpawnObject(Vector2 position)
    {
        GameObject objectToSpawn = objectPool.GetPooledObject(pooledObjects);

        if (objectToSpawn != null)
        {
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = Quaternion.identity;
            objectToSpawn.SetActive(true);
        }

        return objectToSpawn;
    }

    IEnumerator Attack()
    {
        int choice = Random.Range(0, 2); // 0 or 1
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

        GameObject lastObjectSpawned;
        for (int numEnemy = 0; numEnemy < 20; numEnemy++)
        {
            lastObjectSpawned = SpawnObject(pos);

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
