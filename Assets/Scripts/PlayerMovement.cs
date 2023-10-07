using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D RB2D;
    public float dashForce, speed, friction;    
    public KeyCode dashKey, leftKey;
    float aXH;
    Vector2 direction;
    SpriteRenderer spriteRenderer;
    private bool isDashing = false;

    private void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        aXH = Input.GetAxisRaw("Horizontal");

        direction = new Vector2(aXH, 0).normalized;

        //Dash
        if (Input.GetKeyDown(dashKey) && direction != Vector2.zero)
        {
            RB2D.AddForce(direction * dashForce, ForceMode2D.Impulse);
        }


        if (aXH > 0)
        {
            spriteRenderer.flipX = false;

        }
        else if(aXH < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        //Movement
        transform.position += (Vector3.right * aXH) * speed * Time.fixedDeltaTime;
    }
}
