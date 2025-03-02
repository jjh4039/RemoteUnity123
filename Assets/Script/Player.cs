using JetBrains.Annotations;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rigid;
    [HideInInspector] public CapsuleCollider2D colider;
    [HideInInspector] public SpriteRenderer spriteRen;
    [HideInInspector] public Animator anim;
    [HideInInspector] public float leftRight;
    [HideInInspector] public bool isGround;
    public LayerMask groundLayer;
    public int speed;   
    public float jumpPower;
    public int[] readyFruits;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        colider = GetComponent<CapsuleCollider2D>();
        spriteRen = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        leftRight = Input.GetAxisRaw("Horizontal");
        rigid.linearVelocity = new Vector2(leftRight * speed, rigid.linearVelocityY); // 기본 좌우이동

        if (leftRight < 0) spriteRen.flipX = true;
        else if (leftRight > 0) spriteRen.flipX = false; // 이동하는 방향 바라보기

        if (leftRight != 0) anim.SetBool("Move", true);
        else anim.SetBool("Move", false);

        if ((Input.GetKeyDown(KeyCode.Space)) && isGround == true) // 테스트 용 점프
        {
            rigid.linearVelocityY = jumpPower;
        }

        if (Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - 1f), groundLayer)) {isGround = true; anim.SetBool("Jump", false); } // isGround 관리
        else { isGround = false; anim.SetBool("Jump", true); }
    }
}
