using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    enum PlayerCondition { Move, Stop, Jump }
    PlayerCondition playerCondition = PlayerCondition.Stop;

    [Header("Player Property")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerJumpForce;

    [SerializeField] private GameObject animObject;

    private SpriteRenderer spriteRenderer;

    private float currentPlayerSpeed;
    private Rigidbody2D rb;
    private bool groundCheck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = animObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (playerCondition == PlayerCondition.Move)
        {
            rb.velocity = new Vector2(currentPlayerSpeed * Time.fixedDeltaTime, rb.velocity.y);
            //rb.AddForce(new Vector2(currentPlayerSpeed, 0f), ForceMode2D.Force);
        }


    }


    public void RightMove()
    {
        currentPlayerSpeed = playerSpeed;
        spriteRenderer.flipX = false;
        playerCondition = PlayerCondition.Move;
    }
    public void LeftMove()
    {
        currentPlayerSpeed = -playerSpeed;
        spriteRenderer.flipX = true;
        playerCondition = PlayerCondition.Move;
    }

 
    public void StopMove()
    {
        //currentPlayerSpeed = 0f;
        playerCondition = PlayerCondition.Stop;
    }
    public void Jump()
    {
        if (groundCheck)
        {
            rb.velocity = new Vector2(rb.velocity.x, playerJumpForce);
            groundCheck = false;
            playerCondition = PlayerCondition.Jump;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        groundCheck = true;
    }

}






//if (transform.localScale.x > 0)
//{
//    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.y);
//}