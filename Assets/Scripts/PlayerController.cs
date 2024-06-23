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

    bool canDash, isDashing;
    float dashDir;
    public float DashForce;

    public float JumpForce;
    public LayerMask WhatIsGround;
    public float JumpRadius;
    bool isGrounded;

    public Transform GroundCheckPos;
    public int JumpAmount;
    int JumpCounter;
    public float waitTimeDash;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canDash = true;
        // tr.emitting = false;
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
        isGrounded = Physics2D.OverlapCircle(GroundCheckPos.position, JumpRadius, WhatIsGround);

        if (isGrounded)
        {
            JumpCounter = JumpAmount;
        }

        if (Input.GetKeyDown(KeyCode.Space) && JumpCounter > 0)
        {
            rb.velocity = Vector2.up * JumpForce;
            if (!isGrounded)
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

        anim.SetBool("hasJumped", !isGrounded);

        #endregion

        #region dash
        if (Input.GetKeyDown(KeyCode.Q) && canDash)
        {
            // dash
            isDashing = true;
            canDash = false;
            StartCoroutine(StopDash());
        }

        if (isDashing)
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
            {
                dashDir = transform.localScale.x;
                // dashDir = Mathf.Clamp(transform.localScale.x, -1, 1);
            }
            rb.velocity = new Vector2(dashDir * DashForce, rb.velocity.y) * Time.deltaTime;
            // tr.emitting = true;
        }

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

    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(waitTimeDash);
        canDash = true;
        isDashing = false;
        // tr.emitting = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(GroundCheckPos.position, JumpRadius);
    }



}
