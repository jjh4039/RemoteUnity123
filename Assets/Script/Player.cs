using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigid;
    public CapsuleCollider2D colider;
    public SpriteRenderer spriteRen;
    public Animator anim;
    public float leftRight;
    public int speed;

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
        rigid.linearVelocity = new Vector2(leftRight * speed, rigid.linearVelocityY); // �⺻ �¿��̵�

        if (leftRight < 0) spriteRen.flipX = true;
        else if (leftRight > 0) spriteRen.flipX = false; // �̵��ϴ� ���� �ٶ󺸱�

        if (leftRight != 0) anim.SetBool("Move", true);
        else anim.SetBool("Move", false);

    }
}
