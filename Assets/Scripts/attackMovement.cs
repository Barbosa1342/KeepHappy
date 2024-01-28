using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AttackMovement : MonoBehaviour
{
    float yInicial;
    public float mult = 1;
    public float speed = 1;

    float camHeight;
    float camWidth;

    public int attackType;
    // 0 = basic
    // 1 = one
    // 2 = two

    public bool top;
    Vector2 step;
    public Vector2 dir;

    void OnEnable()
    {
        yInicial = transform.position.y;

        if (attackType == 0 || attackType == 2)
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
        if (attackType == 0 || attackType == 2)
        {
            MovementAttack();
        }
        else
        {
            MovementAttackOne();
        }

        DesactiveInstance();
    }

    void MovementAttack()
    {
        Vector2 newPos = new(transform.position.x + step.x, transform.position.y + step.y);
        
        transform.position = newPos;
    }

    void SetStep()
    {
        step = speed * Time.deltaTime * dir;
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

        if (posX > (camWidth + 5) ||
            posX < (-camWidth - 5) ||
            posY < (-camHeight - 5) ||
            posY > (camHeight + 5))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Player") && (attackType == 0 || attackType == 2))
        {
            gameObject.SetActive(false);
        }
    }
}
