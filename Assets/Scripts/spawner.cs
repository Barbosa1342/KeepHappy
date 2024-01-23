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

    GameObject lastObjectToSpawn;
    void Start()
    {
        pooledObjects = objectPool.SetPool(objectToPool, amountToPool);

        var cam = Camera.main;
        camHeight = cam.orthographicSize;
        camWidth = cam.orthographicSize * cam.aspect;
    }

    void Update()
    {
        Attack1(true);
    }


    void Attack1(bool top = false)
    {
        Vector2 pos;

        if (top)
        {
            pos = new Vector2(camWidth, camHeight);
        }
        else
        {
            pos = new Vector2(camWidth, -camHeight);
        }
        
        if (lastObjectToSpawn == null)
        {
            lastObjectToSpawn = SpawnObject(pos, top);
        }
        else
        {
            BoxCollider2D box = lastObjectToSpawn.GetComponent<BoxCollider2D>();
            float dis = camWidth - lastObjectToSpawn.transform.position.x;

            if (dis > box.size.x * (1.1))
            {
                lastObjectToSpawn = SpawnObject(pos, top);
            }
        }
    }

    GameObject SpawnObject(Vector2 position, bool top = false)
    {
        GameObject objectToSpawn = objectPool.GetPooledObject(pooledObjects);

        if (objectToSpawn != null)
        {
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = Quaternion.identity;
            objectToSpawn.GetComponent<attackMovement>().top = top;
            objectToSpawn.SetActive(true);
        }

        return objectToSpawn;
    }
}
