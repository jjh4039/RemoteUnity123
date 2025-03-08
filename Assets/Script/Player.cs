using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using static Fruits;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Collections;

public class Player : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rigid;
    [HideInInspector] public CapsuleCollider2D colider;
    [HideInInspector] public SpriteRenderer spriteRen;
    [HideInInspector] public Animator anim;
    [HideInInspector] public float leftRight;
    [HideInInspector] public bool isGround;
    [HideInInspector] public Fruits fruits;
    [HideInInspector] public IntroducePosition iPos;
    public LayerMask groundLayer;
    public int speed;
    public float jumpPower;
    public int[] readyFruits;
    public int recentUseFruit;
    public bool isDash;
    public bool isMove;
    public bool isUseFruit;
    public bool isReadyFruit;
    public bool isFruitStop;

    void Start()
    {
        isFruitStop = false;
        isUseFruit = false;
        isReadyFruit = true;
        isDash = false;
        readyFruits = new int[3]; // ���� ���� ���� (���� 3)
        for (int k = 0; k < readyFruits.Length; k++) { readyFruits[k] = 0; }
        rigid = GetComponent<Rigidbody2D>();
        colider = GetComponent<CapsuleCollider2D>();
        spriteRen = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMove == true) leftRight = Input.GetAxisRaw("Horizontal");


        if (isDash == false) // �뽬 �� �⺻ �̵� ���� �� ��������Ʈ ���� ��ȯ X  
        { 
            rigid.linearVelocity = new Vector2(leftRight * speed, rigid.linearVelocityY); // �⺻ �¿��̵�

            if (leftRight < 0) spriteRen.flipX = true;
            else if (leftRight > 0) spriteRen.flipX = false; // �̵��ϴ� ���� �ٶ󺸱�
        }

        if (leftRight != 0) anim.SetBool("Move", true);
        else anim.SetBool("Move", false);

        if ((Input.GetKeyDown(KeyCode.Space)) & isUseFruit == true && isFruitStop == false) // ���� ���
        {
            UseFruit();
        }

        if ((Input.GetKeyDown(KeyCode.Q)) && GameManager.Instance.fruitManager.isEatApple == true && isReadyFruit == true && isFruitStop == false) // ���(1) �غ�
        {
            ReadyFruit(1);
        }

        if ((Input.GetKeyDown(KeyCode.W)) && GameManager.Instance.fruitManager.isEatBanana == true && isReadyFruit == true && isFruitStop == false) // �ٳ���(2) �غ�
        {
            ReadyFruit(2);
        }

        if (Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - 1f), groundLayer)) { isGround = true; anim.SetBool("Jump", false); } // isGround ����
        else { isGround = false; anim.SetBool("Jump", true); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fruits")
        {
            fruits = collision.gameObject.GetComponent<Fruits>();
            fruits.EatFruit();
        }

        if(collision.gameObject.tag == "Introduce")
        {
            iPos = collision.gameObject.GetComponent<IntroducePosition>();
            iPos.introduceStart();
        }
    }

    void ReadyFruit(int fruitId) // ���� �غ� (���� ��ȣ)
    {
        for (int i = 0; i < readyFruits.Length; i++)
        {
            if (readyFruits[i] == 0)
            {
                readyFruits[i] = fruitId;
                GameManager.Instance.bar.BarsColor[i].color = new Color(1f, 0.65f, 0.62f, 1f);
                if (i == readyFruits.Length - 1)
                {
                    GameManager.Instance.bar.StartCoroutine("BarReadying");
                }
                break;
            }
        }
    }

    void UseFruit() // ���� ���
    {

        for (int i = 0; i <= readyFruits.Length; i++)
        {
            if (readyFruits[i] != 0)
            {
                if (readyFruits[i] == recentUseFruit)
                {
                    Debug.Log("���� ���� ��� ����");
                    break;
                }
                else 
                { 
                    switch (readyFruits[i])
                    {
                        case 1:
                            rigid.linearVelocityY = jumpPower;
                            recentUseFruit = 1;
                            break;
                        case 2:
                            StartCoroutine(Dash());
                            recentUseFruit = 2;
                            break;
                        default:
                            Debug.Log("���� ��� ����");
                            break;
                    }
                    GameManager.Instance.bar.BarsColor[i].color = Color.white;
                    GameManager.Instance.fruitManager.FruitUseParticle(recentUseFruit);
                    StopCoroutine("RecentFruitReset"); // �ߺ� �ʱ�ȭ ���� �뵵
                    StartCoroutine("RecentFruitReset"); // ���� ��� ���� �ڷ�ƾ ����
                    if (i == readyFruits.Length - 1) // �������� ���������
                    {
                        isUseFruit = false;
                        isReadyFruit = true;
                    }
                    readyFruits[i] = 0;
                    break;
                }
            }
        }
    }

    IEnumerator RecentFruitReset() // ���� ��� ���� �ڷ�ƾ
    {
        yield return new WaitForSeconds(2f);
        recentUseFruit = 0;
        Debug.Log("�ֱ� ���� ��� ��� �ʱ�ȭ");
    }

    IEnumerator Dash()
    {
        isDash = true;
        anim.SetBool("Dash", true);
        rigid.linearVelocity = new Vector2(leftRight * speed * 2.5f, rigid.linearVelocityY);
        yield return new WaitForSeconds(0.2f);
        isDash = false;
        anim.SetBool("Dash", false);
    }

    public void MoveStop()
    {
        isMove = false;
        rigid.linearVelocityX = 0;
        leftRight = 0;
        isFruitStop = true;
    }
}
