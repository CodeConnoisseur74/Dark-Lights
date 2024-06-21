using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    public float Speed;

    bool isFacingLeft;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xInput * Speed * Time.deltaTime, rb.velocity.y);

        if (xInput > 0 && isFacingLeft == true)
        {
            isFacingLeft = !isFacingLeft;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            // Flip the player
        }
        else if (xInput < 0 && isFacingLeft == false)
        {
            // Flip the player
            isFacingLeft = !isFacingLeft;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}
