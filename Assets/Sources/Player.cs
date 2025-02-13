using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator animator;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isMoving;

    private float xInput;

    private int facingDirection;
    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        facingDirection = 1;
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        InputPlayer();

        UpdatePlayerStatus();

        AnimatorControllers();
    }

    private void InputPlayer()
    {
        xInput = Input.GetAxisRaw("Horizontal");
    }

    private void UpdatePlayerStatus()
    {
        Move();
        Jump();
        Flip();
    }

    private void Move()
    {
       rb2d.velocity = new Vector2(xInput * moveSpeed, rb2d.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
    }

    private void Flip()
    {
        if ((rb2d.velocity.x > 0 && !facingRight) || 
            (rb2d.velocity.x < 0 && facingRight))
        {
            facingDirection *= -1;
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    private void AnimatorControllers()
    {
        isMoving = rb2d.velocity.x != 0;

        animator.SetBool("isMoving", isMoving);
    }
}
