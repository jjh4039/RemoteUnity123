using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
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
    public bool isMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        colider = GetComponent<CapsuleCollider2D>();
        spriteRen = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove == true) leftRight = Input.GetAxisRaw("Horizontal");


            rigid.linearVelocity = new Vector2(leftRight * speed, rigid.linearVelocityY); // �⺻ �¿��̵�

            if (leftRight < 0) spriteRen.flipX = true;
            else if (leftRight > 0) spriteRen.flipX = false; // �̵��ϴ� ���� �ٶ󺸱�
        if ((Input.GetKeyDown(KeyCode.Space)) && isGround == true) 
        {
            rigid.linearVelocityY = jumpPower;
        }

        if (leftRight != 0) anim.SetBool("Move", true);
        else anim.SetBool("Move", false);
        if (Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - 1f), groundLayer)) { isGround = true; anim.SetBool("Jump", false); } // isGround ����
        else { isGround = false; anim.SetBool("Jump", true); }
    }
}
