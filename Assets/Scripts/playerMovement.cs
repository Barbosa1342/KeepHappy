using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rig;

    [SerializeField] float speed = 5;
    float dirX, dirY;
    readonly float diagonalLimit = 0.7f;
    void Awake()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        transform.position = mousePosition;
        //dirX = Input.GetAxisRaw("Horizontal");
        //dirY = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        /*
        if (dirX > 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else if (dirX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (dirX != 0 && dirY != 0)
        {
            dirX *= diagonalLimit;
            dirY *= diagonalLimit;
        }

        Vector2 newVelocity = new Vector2(dirX, dirY) * speed;
        rig.velocity = Vector2.Lerp(rig.velocity, newVelocity, 0.75f);*/

        
    }
}
