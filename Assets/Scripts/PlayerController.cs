using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    public float Speed;
    bool isFacingLeft;
    public float JumpForce;
    public LayerMask WhatIsGround;
    public float JumpRadius;
    bool IsGrounded;

    public Transform groundCheckPos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(groundCheckPos.position, JumpRadius, WhatIsGround);
        float xInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xInput * Speed * Time.deltaTime, rb.velocity.y);

        if (xInput > 0 && isFacingLeft == true)
        {
            Flip();
        }
        else if (xInput < 0 && isFacingLeft == false)
        {
            Flip();
        }

        if (IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * JumpForce;
        }
    }

    void Flip()
    {
        isFacingLeft = !isFacingLeft;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
