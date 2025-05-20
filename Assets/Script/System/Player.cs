using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using static Fruits;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Collections;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Component")]
    [HideInInspector] public Rigidbody2D rigid;
    [HideInInspector] public BoxCollider2D colider;
    [HideInInspector] public SpriteRenderer spriteRen;
    [HideInInspector] public Animator anim;
    [HideInInspector] public float leftRight;
    [HideInInspector] public bool isGround;
    [HideInInspector] public Fruits fruits;
    [HideInInspector] public IntroducePosition iPos;
    public GameObject[] emotionBox;
    public LayerMask groundLayer;

    [Header("Field")]
    public float speed;
    public float jumpPower;
    public int[] readyFruits;
    public int recentReadyFruit;

    [Header("Trigger")]
    public bool isDash;
    public bool isMove;
    public bool isCut;
    public bool isUseFruit; // ���� ������ �����Ѱ�?
    public bool isReadyFruit; // ���� �غ� ���� �����ΰ�?
    public bool isFruitStop; // ���ϰ� ���õ� ��� ��� ���
    public bool isFocus;
    public bool isLive; // �÷��̾� ��������

    void Start()
    {
        isFruitStop = false;
        isUseFruit = false;
        isReadyFruit = true;
        isDash = false;
        isLive = true;
        isCut = false;
        readyFruits = new int[3]; // ���� ���� ���� (���� 3)
        for (int k = 0; k < readyFruits.Length; k++) { readyFruits[k] = 0; }
        rigid = GetComponent<Rigidbody2D>();
        colider = GetComponent<BoxCollider2D>();
        spriteRen = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        isLive = true;
    }

    void Update()
    {
        if (isMove == true) leftRight = Input.GetAxisRaw("Horizontal");
        if (isDash == false && isCut == false) // �뽬 �� �⺻ �̵� ���� �� ��������Ʈ ���� ��ȯ X  
        {
            rigid.linearVelocity = new Vector2(leftRight * speed, rigid.linearVelocityY); // �⺻ �¿��̵�

            if (leftRight < 0) spriteRen.flipX = true;
            else if (leftRight > 0) spriteRen.flipX = false; // �̵��ϴ� ���� �ٶ󺸱�
        }

        if (isCut == false) {
            if (leftRight != 0) anim.SetBool("Move", true);
            else anim.SetBool("Move", false);
        }
        else
        {
            if (rigid.linearVelocityX > 0)
            {
                anim.SetBool("Move", true);
            }
            else anim.SetBool("Move", false);
        }


        if ((Input.GetKeyDown(KeyCode.Space)) && isFruitStop == false && isUseFruit == true) // ���� ���
        {
            StartCoroutine("UseFruit");
        }

        if ((Input.GetKeyDown(KeyCode.Q)) && GameManager.Instance.fruitManager.isEatApple == true && isReadyFruit == true && isFruitStop == false) // ���(1) �غ�
        {
            ReadyFruit(1);

        }

        if ((Input.GetKeyDown(KeyCode.W)) && GameManager.Instance.fruitManager.isEatBanana == true && isReadyFruit == true && isFruitStop == false) // �ٳ���(2) �غ�
        {
            ReadyFruit(2);
        }

        if (Physics2D.Linecast(new Vector2(transform.position.x - 0.4f, transform.position.y), new Vector2(transform.position.x + 0.4f, transform.position.y - 1f), groundLayer) ||
            Physics2D.Linecast(new Vector2(transform.position.x + 0.4f, transform.position.y), new Vector2(transform.position.x - 0.4f, transform.position.y - 1f), groundLayer)) { isGround = true; anim.SetBool("Jump", false); } // isGround ����
        else { isGround = false; anim.SetBool("Jump", true); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fruits")
        {
            fruits = collision.gameObject.GetComponent<Fruits>();
            fruits.EatFruit();
        }

        if (collision.gameObject.tag == "Introduce")
        {
            iPos = collision.gameObject.GetComponent<IntroducePosition>();
            iPos.introduceStart();
        }
    }

    public void Focus(bool condition) // ��Ŀ�� ��� ����
    {
        if (condition == true) // ��Ŀ�� ����
        {
            isFocus = true;
            isUseFruit = true;
            GameManager.Instance.barUi.StartCoroutine("FoucsEffect"); // �� UI ��Ŀ�� ȿ��
            GameManager.Instance.barUi.SetMainText(BarUi.SettingText.ready); // �� UI �ؽ�Ʈ ����
            Time.timeScale = 0.25f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else // ��Ŀ�� ���� = ����
        {
            isFocus = false;
            isUseFruit = false;
            GameManager.Instance.barUi.StopCoroutine("FoucsEffect");
            GameManager.Instance.barUi.StartCoroutine("FoucsEffectOff");
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            recentReadyFruit = 0; // ���ӹ������� �ʱ�ȭ
            GameManager.Instance.barUi.SetMainText(BarUi.SettingText.none); // ���� UI �ؽ�Ʈ ����
        }
    }

    void ReadyFruit(int fruitId) // ���� �غ� (���� ��ȣ)
    {
        for (int i = 0; i < readyFruits.Length; i++)
        {
            if (fruitId == recentReadyFruit && readyFruits[readyFruits.Length - 1] == 0) // ���� ���� �غ� �Ұ�
            {
                GameManager.Instance.barUi.StartCoroutine("Error"); // �� UI ���� �ؽ�Ʈ ���
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Error, 0);
            }
            else
            {
                if (readyFruits[i] == 0)
                {
                    if (i == 0) { Focus(true); } // ù��° ���� �غ� �� �ڵ� ��Ŀ�� ��� ����

                    readyFruits[i] = fruitId;
                    switch (fruitId)
                    {
                        case 1:
                            GameManager.Instance.bar.BarsColor[i].color = new Color(1f, 0.65f, 0.62f, 0.85f);
                            AudioManager.instance.PlaySfx(AudioManager.Sfx.Ready, 1);
                            recentReadyFruit = 1;
                            break;
                        case 2:
                            GameManager.Instance.bar.BarsColor[i].color = new Color(1f, 1f, 0.7f, 0.85f);
                            AudioManager.instance.PlaySfx(AudioManager.Sfx.Ready, 2);
                            recentReadyFruit = 2;
                            break;
                    }
                    if (i == readyFruits.Length - 1) // ���� ä���� �� ���ٲٱ� & �����ϼ���!
                    {
                        GameManager.Instance.barUi.SetMainText(BarUi.SettingText.full); // �� UI �ؽ�Ʈ ����
                    }
                    break;
                }
            }
        }
    }

    IEnumerator UseFruit() // ���� ���
    {
        Focus(false); // ���� �� ��Ŀ�� ����
        isReadyFruit = false;

        for (int i = 0; i <= readyFruits.Length - 1; i++)
        {
            if (readyFruits[i] != 0)
            {
                {
                    switch (readyFruits[i]) // ���� ��ȣ
                    {
                        case 1:
                            rigid.linearVelocityY = jumpPower;
                            AudioManager.instance.PlaySfx(AudioManager.Sfx.Use, 1);
                            break;
                        case 2:
                            StartCoroutine(Dash());
                            AudioManager.instance.PlaySfx(AudioManager.Sfx.Use, 2);
                            break;
                    }
                    GameManager.Instance.bar.UseColorChange(i, readyFruits[i]); // ���� �� ����
                    GameManager.Instance.fruitManager.FruitUseParticle(readyFruits[i]);

                    if (i == readyFruits.Length - 1) // ���� ���� ����A : ������ ĭ ��������� 
                    {
                        yield return new WaitForSeconds(0.3f);
                        GameManager.Instance.bar.StartCoroutine("BarReadying");
                    }
                    else if (readyFruits[i + 1] == 0) // ���� ���� ����B : ������ �������϶�
                    {
                        yield return new WaitForSeconds(0.3f);
                        GameManager.Instance.bar.StartCoroutine("BarReadying");
                    }

                    readyFruits[i] = 0;
                    yield return new WaitForSeconds(0.35f);
                    // break;
                }
            }
        }
    }

    IEnumerator Dash()
    {
        isDash = true;
        rigid.gravityScale = 0f; // �뽬 �� �߷� ����
        rigid.linearVelocityY = 0f; // �뽬 ���۽� ���� ��ġ ����

        anim.SetBool("Dash", true);
        if (leftRight == 0) // ����Ű ����� �뽬 -> �ٶ󺸴� ����
        {
            if (spriteRen.flipX == true) leftRight = -1;
            else leftRight = 1;
        }
        rigid.linearVelocity = new Vector2(leftRight * speed * 1.7f, rigid.linearVelocityY);
        yield return new WaitForSeconds(0.2f);
        rigid.gravityScale = 1.8f;
        isDash = false;
        anim.SetBool("Dash", false);
    }

    public void MoveStop()
    {
        ResetFruit();
        isMove = false;
        rigid.linearVelocityX = 0;
        leftRight = 0;
        isFruitStop = true;
    }

    public void MoveStart()
    {
        isMove = true;
        isFruitStop = false;
        isReadyFruit = true; // ���� �غ� ���� ����
    }

    public void Die()
    {
        ResetFruit();
        isLive = false;
        SpawnManager.instance.deathCount++;
        gameObject.SetActive(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Die, 0);
        GameManager.Instance.gameOver.gameOverUi.SetActive(true);
    }

    public void EmotionQuestion()
    {
        GameObject.Instantiate(emotionBox[0], new Vector3(transform.position.x, transform.position.y + 0.9f, 0), Quaternion.identity);
    }

    public void EmotionThink()
    {
        GameObject.Instantiate(emotionBox[1], new Vector3(transform.position.x, transform.position.y + 0.9f, 0), Quaternion.identity);
    }

    // ���� ���� ��ü �ʱ�ȭ
    public void ResetFruit()
    {
        for (int i = 0; i < readyFruits.Length; i++)
        {
            readyFruits[i] = 0;
            GameManager.Instance.bar.BarsColor[i].color = Color.white;
        }
    }

    // ���� ���� ���� ������� �ʱ�ȭ
    public void WhiteResetFruit() 
    {
        for (int i = 0; i < readyFruits.Length; i++)
        {
            GameManager.Instance.bar.BarsColor[i].color = Color.white; // ���� �� �� ����
        }
    }
}
