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

    public int attackType;
    // 0 = basic
    // 1 = one

    public bool top;
    Vector2 step;

    GameObject player;
    void OnEnable()
    {
        yInicial = transform.position.y;

        if (attackType == 0)
        {
            SetStep();
        }
    }

    private void Start()
    {
        var cam = Camera.main;
        camHeight = cam.orthographicSize;
        camWidth = cam.orthographicSize * cam.aspect;
    }

    void Update()
    {
        if (player != null)
        {
            if (attackType == 0)
            {
                MovementAttack();
            }
            else
            {
                MovementAttackOne();
            }

            DesactiveInstance();
        }
    }

    void MovementAttack()
    {
        Vector2 newPos = new Vector2(transform.position.x + step.x, transform.position.y + step.y);
        
        transform.position = newPos;
    }

    void SetStep()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector2 actualPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 dir = new Vector2(playerPos.x - actualPos.x, playerPos.y - actualPos.y).normalized;

            step = speed * dir * Time.deltaTime;
        }
    }

    void MovementAttackOne()
    {
        float x = transform.position.x + (-speed * Time.deltaTime);
        float y;

        if (top)
        {
            y = yInicial - Mathf.Cos(transform.position.x / Camera.main.orthographicSize) * mult;
        }
        else
        {
            y = yInicial + Mathf.Cos(transform.position.x / Camera.main.orthographicSize) * mult;
        }

        transform.position = new Vector2(x, y);
    }
    void DesactiveInstance()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;

        if (posX > (camWidth + 10) ||
            posX < (-camWidth - 10) ||
            posY < (-camHeight - 10) ||
            posY > (camHeight + 10))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && attackType == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
