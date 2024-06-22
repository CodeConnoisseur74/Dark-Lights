using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    public float Speed = 50;
    bool isFacingLeft;
    Animator anim;
    public float JumpForce;
    public LayerMask WhatIsGround;
    public float JumpRadius;
    bool IsGrounded;

    public Transform GroundCheckPos;
    public int JumpAmount;
    int JumpCounter;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
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

        #endregion

        #region Jump
        IsGrounded = Physics2D.OverlapCircle(GroundCheckPos.position, JumpRadius, WhatIsGround);

        if (IsGrounded)
        {
            JumpCounter = JumpAmount;
        }

        if (Input.GetKeyDown(KeyCode.Space) && JumpCounter > 0)
        {
            rb.velocity = Vector2.up * JumpForce;
            if (!IsGrounded)
            {
                JumpCounter--;
            }
        }

        #endregion

        #region Animations

        if (xInput != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        anim.SetBool("hasJumped", !IsGrounded);

        #endregion

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Flip()
    {
        isFacingLeft = !isFacingLeft;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(GroundCheckPos.position, JumpRadius);
    }



}
