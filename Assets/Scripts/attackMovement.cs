using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class attackMovement : MonoBehaviour
{
    float yInicial;
    public float mult = 1;
    public float speed = 1;

    float camHeight;
    float camWidth;

    public bool top;
    void OnEnable()
    {
        yInicial = transform.position.y;
    }

    private void Start()
    {
        var cam = Camera.main;
        camHeight = cam.orthographicSize;
        camWidth = cam.orthographicSize * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;

        float x = posX + (-speed * Time.deltaTime);
        float y;

        if (top)
        {
            y = yInicial - Mathf.Cos(posX / Camera.main.orthographicSize) * mult;
        }
        else
        {
            y = yInicial + Mathf.Cos(posX / Camera.main.orthographicSize) * mult;
        }

        transform.position = new Vector2(x, y);

        if (posX > (camWidth + 10) || posX < (-camWidth - 10) || posY < (-camHeight-10) || posY > (camHeight+10))
        {
            gameObject.SetActive(false);
        }
    }
}
