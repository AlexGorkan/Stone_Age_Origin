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
    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private float currentPlayerSpeed;
    private Rigidbody2D rb;
    private bool groundCheck;
    private float direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = animObject.GetComponent<SpriteRenderer>();
        animator = animObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (playerCondition == PlayerCondition.Move)
        {
            rb.velocity = new Vector2(playerSpeed * direction * Time.fixedDeltaTime, rb.velocity.y);
            //rb.AddForce(new Vector2(currentPlayerSpeed, 0f), ForceMode2D.Force);
        }
        animator.SetFloat("Speed", Mathf.Abs(playerSpeed));

    }


    public void RightMove()
    {
        //currentPlayerSpeed = playerSpeed;
        direction = 1f;
        spriteRenderer.flipX = false;
        playerCondition = PlayerCondition.Move;
    }
    public void LeftMove()
    {
        //currentPlayerSpeed = -playerSpeed;
        direction = -1f;
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
            animator.SetTrigger("Jump");
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