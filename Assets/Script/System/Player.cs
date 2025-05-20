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
    public bool isUseFruit; // 과일 발현이 가능한가?
    public bool isReadyFruit; // 과일 준비 가능 상태인가?
    public bool isFruitStop; // 과일과 관련된 모든 기능 잠금
    public bool isFocus;
    public bool isLive; // 플레이어 생존여부

    void Start()
    {
        isFruitStop = false;
        isUseFruit = false;
        isReadyFruit = true;
        isDash = false;
        isLive = true;
        isCut = false;
        readyFruits = new int[3]; // 과일 슬롯 개수 (현재 3)
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
        if (isDash == false && isCut == false) // 대쉬 중 기본 이동 로직 및 스프라이트 방향 전환 X  
        {
            rigid.linearVelocity = new Vector2(leftRight * speed, rigid.linearVelocityY); // 기본 좌우이동

            if (leftRight < 0) spriteRen.flipX = true;
            else if (leftRight > 0) spriteRen.flipX = false; // 이동하는 방향 바라보기
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


        if ((Input.GetKeyDown(KeyCode.Space)) && isFruitStop == false && isUseFruit == true) // 과일 사용
        {
            StartCoroutine("UseFruit");
        }

        if ((Input.GetKeyDown(KeyCode.Q)) && GameManager.Instance.fruitManager.isEatApple == true && isReadyFruit == true && isFruitStop == false) // 사과(1) 준비
        {
            ReadyFruit(1);

        }

        if ((Input.GetKeyDown(KeyCode.W)) && GameManager.Instance.fruitManager.isEatBanana == true && isReadyFruit == true && isFruitStop == false) // 바나나(2) 준비
        {
            ReadyFruit(2);
        }

        if (Physics2D.Linecast(new Vector2(transform.position.x - 0.4f, transform.position.y), new Vector2(transform.position.x + 0.4f, transform.position.y - 1f), groundLayer) ||
            Physics2D.Linecast(new Vector2(transform.position.x + 0.4f, transform.position.y), new Vector2(transform.position.x - 0.4f, transform.position.y - 1f), groundLayer)) { isGround = true; anim.SetBool("Jump", false); } // isGround 관리
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

    public void Focus(bool condition) // 포커스 모드 관리
    {
        if (condition == true) // 포커스 시작
        {
            isFocus = true;
            isUseFruit = true;
            GameManager.Instance.barUi.StartCoroutine("FoucsEffect"); // 바 UI 포커스 효과
            GameManager.Instance.barUi.SetMainText(BarUi.SettingText.ready); // 바 UI 텍스트 변경
            Time.timeScale = 0.25f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else // 포커스 해제 = 발현
        {
            isFocus = false;
            isUseFruit = false;
            GameManager.Instance.barUi.StopCoroutine("FoucsEffect");
            GameManager.Instance.barUi.StartCoroutine("FoucsEffectOff");
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            recentReadyFruit = 0; // 연속방지변수 초기화
            GameManager.Instance.barUi.SetMainText(BarUi.SettingText.none); // 발현 UI 텍스트 변경
        }
    }

    void ReadyFruit(int fruitId) // 과일 준비 (과일 번호)
    {
        for (int i = 0; i < readyFruits.Length; i++)
        {
            if (fruitId == recentReadyFruit && readyFruits[readyFruits.Length - 1] == 0) // 같은 과일 준비 불가
            {
                GameManager.Instance.barUi.StartCoroutine("Error"); // 바 UI 에러 텍스트 출력
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Error, 0);
            }
            else
            {
                if (readyFruits[i] == 0)
                {
                    if (i == 0) { Focus(true); } // 첫번째 과일 준비 시 자동 포커스 모드 시작

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
                    if (i == readyFruits.Length - 1) // 전부 채웠을 때 색바꾸기 & 발현하세요!
                    {
                        GameManager.Instance.barUi.SetMainText(BarUi.SettingText.full); // 바 UI 텍스트 변경
                    }
                    break;
                }
            }
        }
    }

    IEnumerator UseFruit() // 과일 사용
    {
        Focus(false); // 발현 시 포커스 해제
        isReadyFruit = false;

        for (int i = 0; i <= readyFruits.Length - 1; i++)
        {
            if (readyFruits[i] != 0)
            {
                {
                    switch (readyFruits[i]) // 과일 번호
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
                    GameManager.Instance.bar.UseColorChange(i, readyFruits[i]); // 발현 색 변경
                    GameManager.Instance.fruitManager.FruitUseParticle(readyFruits[i]);

                    if (i == readyFruits.Length - 1) // 발현 종료 검증A : 마지막 칸 사용했으면 
                    {
                        yield return new WaitForSeconds(0.3f);
                        GameManager.Instance.bar.StartCoroutine("BarReadying");
                    }
                    else if (readyFruits[i + 1] == 0) // 발현 종료 검증B : 다음이 마지막일때
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
        rigid.gravityScale = 0f; // 대쉬 중 중력 제거
        rigid.linearVelocityY = 0f; // 대쉬 시작시 수직 위치 고정

        anim.SetBool("Dash", true);
        if (leftRight == 0) // 방향키 안잡고 대쉬 -> 바라보는 방향
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
        isReadyFruit = true; // 과일 준비 가능 상태
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

    // 과일 슬롯 전체 초기화
    public void ResetFruit()
    {
        for (int i = 0; i < readyFruits.Length; i++)
        {
            readyFruits[i] = 0;
            GameManager.Instance.bar.BarsColor[i].color = Color.white;
        }
    }

    // 과일 슬롯 색만 흰색으로 초기화
    public void WhiteResetFruit() 
    {
        for (int i = 0; i < readyFruits.Length; i++)
        {
            GameManager.Instance.bar.BarsColor[i].color = Color.white; // 발현 바 색 조정
        }
    }
}
