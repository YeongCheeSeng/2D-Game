using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int damage;
    public Transform left_Collision;
    public Transform right_Collision;
    public Transform down_Collision;
    public Transform up_Collision;
    public LayerMask PlayerLayerMask;

    public bool moveLeft;

    private Rigidbody2D rb;
    private Animator anim;
    private Player player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        moveLeft = true;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        CheckCollision();
        CheckPlayer();

        if (moveLeft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }

    void CheckCollision()
    {
        if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
    }

    void CheckPlayer()
    {
        if (Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, PlayerLayerMask))
        {
            DamagePlayer();
        }

        if (Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, PlayerLayerMask))
        {
            DamagePlayer();
        }

        if (Physics2D.Raycast(up_Collision.position, Vector2.up, 0.1f, PlayerLayerMask))
        {
            Die();
        }
    }

    void ChangeDirection()
    {
        moveLeft = !moveLeft;
        Vector3 tempScale = transform.localScale;

        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else 
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
        }

        transform.localScale = tempScale;
    }

    void DamagePlayer()
    {
        Debug.Log("Damage Player");

        if (player != null)
        {
            player.curHealth -= damage;
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
