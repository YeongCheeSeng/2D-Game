using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using Unity.Collections;

public class Player : MonoBehaviour
{
    public int maxHealth;
    public float speed = 5f;
    public float jump = 6f;

    private Rigidbody2D myBody;
    private Animator anim;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    private bool isGrounded;
    private bool jumped;

    public int curHealth;
    public int TotalScore;

    public TMP_Text UIScore;
    public AudioSource CoinPickUp;
    public AudioSource Jump;
    public AudioSource Die;    
    public string SceneToLoadAfterDead;

    public float DieDelay;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        curHealth = maxHealth;
        TotalScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        PlayerJump();
        HealthCheck();
        UIShowScore();

        //Debug.Log("Current Health: " + curHealth);
        Debug.Log("Current Score: " + TotalScore);
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
            ChangeDirection(1);
        }
        else if (h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
        }
        else if (h == 0)
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
        {
            CoinPickUp.Play();
        }
    }

    void CheckIfGrounded()
    {
        //isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);
        //Debug.DrawRay(groundCheckPosition.position, Vector3.down, Color.red, 1);

        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, 0.2f, groundLayer);

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
                Jump.Play();
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
            Die.Play();
            anim.Play("Player Die Animation");
            StartCoroutine(DelaySceneLoad());
            //SceneManager.LoadScene(SceneToLoadAfterDead);

            //Destroy(this.gameObject);
        }
    }

    IEnumerator DelaySceneLoad()
    {
        yield return new WaitForSeconds(DieDelay);
        SceneManager.LoadScene(SceneToLoadAfterDead);
    }

    void UIShowScore()
    {
        if (UIScore != null)
        {
            UIScore.text = "X" + TotalScore;
        }
    }
}
