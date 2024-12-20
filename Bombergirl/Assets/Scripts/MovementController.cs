
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public  Rigidbody2D rb {  get; private set; }
    public Vector2 direction = Vector2.down;
    public float speed = 5f;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if(Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up);
        }
        else if(Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down);

        }
        else if(Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left);

        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right);

        }
        else
        {
            SetDirection(Vector2.zero);

        }
      
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rb.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;


    }






}
