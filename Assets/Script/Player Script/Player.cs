using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth;
    public float speed = 5f;
    public float jump = 6f;

    private Rigidbody2D myBody;
    private Animator anim;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;
    private bool isGrounded;
    private bool jumped;
    public int curHealth;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        PlayerJump();
        HealthCheck();

        Debug.Log("Current Health: " + curHealth);
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        //player move left and right
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            ChangeDirection(3);
        }
        else if (h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-3);
        }
        else
        {
            myBody.velocity = new Vector2(0, myBody.velocity.y);
        }

        anim.SetInteger("Speed",Mathf.Abs((int)myBody.velocity.x));

        ////player jump
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    myBody.velocity = new Vector2(myBody.velocity.x, jump);
        //    Debug.Log("Jump");
        //}
    }

    private void ChangeDirection(int direction)
    {
        Vector2 currentScale = transform.localScale;
        currentScale.x = direction;
        transform.localScale = currentScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Enemy")
        //    Debug.Log("Enemy Detacted");

        //if (collision.gameObject.tag == "Ground")
        //{
        //    Debug.Log("Ground Detacted");
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
            Debug.Log("Coin Detacted");
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        Debug.DrawRay(groundCheckPosition.position, Vector3.down, Color.red, 1);

        //Debug.Log("Ground Detacted");

        if (isGrounded && jumped)
        {
            jumped = false;
        }
    }

    void PlayerJump()
    {
        if (isGrounded) 
        {
            anim.SetBool("Jump",false);

            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jump);
                //Debug.Log("Jump");
                anim.SetBool("Jump",true);
            }
        }
    }

    void HealthCheck()
    {
        if (curHealth <= 0)
        {
            curHealth = 0;
            Destroy(this.gameObject);
            return;
        }
    }
}
