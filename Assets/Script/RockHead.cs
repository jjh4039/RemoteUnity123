using UnityEngine;
using System.Collections;

public class RockHead : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider2D;

    private GameObject player;
    private float distance;
    private float playerPos;
    private float rockHeadPos;
    private Vector3 originalPos;
    private float returnSpeed = 1;
    private bool isAttacking = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            distance = Mathf.Abs(transform.position.x - player.transform.position.x);
            if (distance < 1 && !isAttacking)
            {
                Move();
            }

        }
        if(transform.position != originalPos && !isAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, originalPos, returnSpeed * Time.deltaTime);
        }
    }
    void Move()
    {
        rb.gravityScale = 1;
        isAttacking = true;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            SpawnManager.instance.Kill(collision.collider);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            rb.gravityScale = 0;
            isAttacking = false;
        }
    }


    
}
