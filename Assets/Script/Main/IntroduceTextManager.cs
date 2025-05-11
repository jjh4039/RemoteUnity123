using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class IntroduceTextManager : MonoBehaviour
{
    [HideInInspector] public Text introduceText;
    [HideInInspector] public CanvasGroup canvasGroup;
    public string[] texts; // 인스펙터에서 텍스트 수 관리
    public Animator boxAnim;
    public GameObject skipGuide;
    public float textSpeed; // 텍스트 속도
    [HideInInspector] public bool isQuestClear;
    [HideInInspector] public GameObject bar;
    [HideInInspector] public bool isSkip;
    [HideInInspector] public int skipNum;
    [HideInInspector] public int TmpCheck1;

    private void Start()
    {
        GameManager.Instance.player.isMove = false;
        TextSet(); // String Setting
        skipNum = 0;
        TmpCheck1 = -1;
        StartCoroutine(FristStep()); // 튜토리얼 시작
    }

    public void Update()
    {
        if (isQuestClear == false)
        {
            introduceText.color = new Color(0.2f, 0.55f, 1f);
        }
        else
        {
            introduceText.color = introduceText.color = Color.black;
        } 

        if (Input.GetKeyDown(KeyCode.Space) && isSkip == true)
        {
            StartCoroutine(Say(skipNum));
        }

        if (TmpCheck1 > 0) // 튜토리얼용 준비 연습
        {
            if(Input.GetKeyDown(KeyCode.Q)){
                TmpCheck1--;
                if (TmpCheck1 == 0)
                {
                    StartCoroutine(Say(15));
                }
            }
        }
    }

    IEnumerator FristStep()
    {
        GameManager.Instance.cutScene.StartCoroutine("PadeIn");
        introduceText.rectTransform.anchoredPosition = new Vector2(-1281.47f, -720.26f);
        isQuestClear = true; // 처음만 기본값 설정
        StartCoroutine(Say(0));
        yield return new WaitForSeconds(0f);
    }

    IEnumerator SecondStep()
    {
        introduceText.rectTransform.anchoredPosition = new Vector2(-1271.8f, -720.26f);
        StartCoroutine(Say(3));
        yield return new WaitForSeconds(0f);
    }

    IEnumerator ThirdStep()
    {
        introduceText.rectTransform.anchoredPosition = new Vector2(-1264.92f, -720.26f);
        bar.SetActive(true); // 최초 바 Ui 활성화
        StartCoroutine(Say(7));
        yield return new WaitForSeconds(0f);
    }

    IEnumerator FourthStep()
    {
        introduceText.rectTransform.anchoredPosition = new Vector2(-1255.554f, -720.26f);
        StartCoroutine(Say(13));
        yield return new WaitForSeconds(0f);
    }

    IEnumerator FifthStep()
    {
        introduceText.rectTransform.anchoredPosition = new Vector2(-1242.355f, -720.26f);
        StartCoroutine(Say(24));
        yield return new WaitForSeconds(0f);
    }

    IEnumerator FirstDie()
    {
        introduceText.rectTransform.anchoredPosition = new Vector2(-1235.173f, -718.35f);
        StartCoroutine(Say(30));
        yield return new WaitForSeconds(0f);
        GameManager.Instance.player.MoveStop();
    }

    IEnumerator OneMore()
    {
        introduceText.rectTransform.anchoredPosition = new Vector2(-1225.45f, -718.35f);
        StartCoroutine(Say(35));
        SpawnManager.instance.deathCount = 3;
        yield return new WaitForSeconds(0f);
    }

    IEnumerator HighJump()
    {
        introduceText.rectTransform.anchoredPosition = new Vector2(-1204.3f, -718.35f);
        StartCoroutine(Say(37));
        yield return new WaitForSeconds(0f);
    }

    IEnumerator FinalCut()
    {
        GameManager.Instance.player.isCut = true;
        GameManager.Instance.player.spriteRen.flipX = false;
        bar.SetActive(false);
        GameManager.Instance.player.rigid.linearVelocity = new Vector2(1f * GameManager.Instance.player.speed, 0f);
        GameManager.Instance.sceneStep.readyScene("Loading");
        yield return new WaitForSeconds(1f);
        GameManager.Instance.player.rigid.linearVelocity = new Vector2(0f * GameManager.Instance.player.speed, 0f);
        yield return new WaitForSeconds(1f);
        

        GameManager.Instance.player.spriteRen.flipX = true;
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.player.spriteRen.flipX = false;

        // 물음표 이모티콘 생성
        yield return new WaitForSeconds(0.2f);
        GameManager.Instance.player.EmotionQuestion();

        yield return new WaitForSeconds(2.5f);
        GameManager.Instance.mainCamera.StartCoroutine("SizeThreeZoom");
        GameManager.Instance.mainCamera.cameraLevel = 3;

        yield return new WaitForSeconds(1.4f);
        GameManager.Instance.player.EmotionThink();

        yield return new WaitForSeconds(3.1f);
        GameManager.Instance.player.rigid.linearVelocity = new Vector2(1f * GameManager.Instance.player.speed, 0f);

        yield return new WaitForSeconds(1f);
        Time.timeScale = 0.5f;
        GameManager.Instance.cutScene.StartCoroutine("PadeOut");
    }


    IEnumerator Say(int TextIndex) // 대화용
    {
        skipGuide.SetActive(false);
        StartCoroutine(AlphaOn());
        introduceText.text = "";
        isSkip = false;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Typing);

        if (TextIndex == 15) { Time.timeScale = 1f; Time.fixedDeltaTime = 0.02f * Time.timeScale;} // 튜토리얼 슬로우모션 제거

        switch (TextIndex) // 텍스트 박스 관리
        {
            // 시작부분
            case 0 or 3 or 7 or 13 or 15 or 17 or 24 or 30 or 35 or 37:
                StartCoroutine(BoxOn());
                break;
            // 끝부분
            case 2 or 6 or 12 or 14 or 16 or 23 or 29 or 34 or 36 or 40:
                StartCoroutine(BoxOff());
                break;
        }

        switch (TextIndex) // 퀘스트 관리
        {
            case 2 or 6 or 12 or 14 or 16 or 23 or 29 or 34 or 36 or 40: // 퀘스트를 주는 번호
                isQuestClear = false;
                break;
            case 15 or 17 or 24 or 30 or 35 or 37: // 퀘스트 클리어 따로 없이 그냥 다음으로 넘어가는 번호
                isQuestClear = true;
                GameManager.Instance.player.isFruitStop = true;
                break;
            default:
                break;
        }

        switch (TextIndex)
        {
            case 4:
                for (int i = 0; i < texts[TextIndex].Length; i++)
                {
                    switch (i)
                    {
                        case 9:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "어.. 근데\n저 <color=#ff696b>사</color>";
                            break;
                        case 10:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "어.. 근데\n저 <color=#ff696b>사과</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text += texts[TextIndex][i];
                            break;
                    }
                }
                break;
            case 5:
                for (int i = 0; i < texts[TextIndex].Length; i++)
                {
                    switch (i)
                    {
                        case 1:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "'<color=#ff696b>점</color>";
                            break;
                        case 2:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "'<color=#ff696b>점프</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text += texts[TextIndex][i];
                            break;
                    }
                }
                break;
            case 10:
                for (int i = 0; i < texts[TextIndex].Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>특</color>";
                            break;
                        case 1:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>특정</color>";
                            break;
                        case 2:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>특정한</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>특정한 키</color>";
                            break;
                        case 17:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>특정한 키</color>를 눌러\n과일 능력을 <color=#ff696b>준</color>";
                            break;
                        case 18:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>특정한 키</color>를 눌러\n과일 능력을 <color=#ff696b>준비</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text += texts[TextIndex][i];
                            break;
                    }
                }
                break;
            case 11:
                for (int i = 0; i < texts[TextIndex].Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>S</color>";
                            break;
                        case 1:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>Sp</color>";
                            break;
                        case 2:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>Spa</color>";
                            break;
                        case 3:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>Spac</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>Space</color>";
                            break;
                        case 6:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>Space B</color>";
                            break;
                        case 7:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>Space Ba</color>";
                            break;
                        case 8:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>Space Bar</color>";
                            break;
                        case 18:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>Space Bar</color>를 눌러\n능력을 <color=#FF1212>발</color>";
                            break;
                        case 19:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>Space Bar</color>를 눌러\n능력을 <color=#FF1212>발현</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text += texts[TextIndex][i];
                            break;
                    }
                }
                break;
            case 13:
                for (int i = 0; i < texts[TextIndex].Length; i++)
                {
                    switch (i)
                    {
                        case 12:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "바로 갈게요, 사과는\n<color=#ff696b>『</color>";
                            break;
                        case 13:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "바로 갈게요, 사과는\n<color=#ff696b>『Q</color>";
                            break;
                        case 14:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "바로 갈게요, 사과는\n<color=#ff696b>『Q』</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text += texts[TextIndex][i];
                            break;
                    }
                }
                break;
            case 15:
                for (int i = 0; i < texts[TextIndex].Length; i++)
                {
                    switch (i)
                    {
                        case 11:    
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "잘했어요,\n이번에는 <color=#FF1212>발</color>";
                            break;
                        case 12:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "잘했어요,\n이번에는 <color=#FF1212>발현</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text += texts[TextIndex][i];
                            break;
                    }
                }
                break;
            case 18:
                for (int i = 0; i < texts[TextIndex].Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>준</color>";
                            break;
                        case 1:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>준비</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>준비</color>와 <color=#ff1212>발</color>";
                            break;
                        case 5:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>준비</color>와 <color=#ff1212>발현</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text += texts[TextIndex][i];
                            break;
                    }
                }
                break;
            case 20:
                for (int i = 0; i < texts[TextIndex].Length; i++)
                {
                    switch (i)
                    {
                        case 6:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "1. 항상 <color=#ff1212>모</color>";
                            break;
                        case 7: 
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "1. 항상 <color=#ff1212>모든</color>";
                            break;
                        case 9:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "1. 항상 <color=#ff1212>모든 칸</color>";
                            break;
                        case 10:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "1. 항상 <color=#ff1212>모든 칸을</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text += texts[TextIndex][i];
                            break;
                    }
                }
                break;
            case 21:
                for (int i = 0; i < texts[TextIndex].Length; i++)
                {
                    switch (i)
                    {
                        case 3:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "2. <color=#ff1212>같</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "2. <color=#ff1212>같은</color>";
                            break;
                        case 6:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "2. <color=#ff1212>같은 과</color>";
                            break;
                        case 7:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "2. <color=#ff1212>같은 과일</color>";
                            break;
                        case 9:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "2. <color=#ff1212>같은 과일 연</color>";
                            break;
                        case 10:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "2. <color=#ff1212>같은 과일 연속</color>";
                            break;
                        case 11:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "2. <color=#ff1212>같은 과일 연속발</color>";
                            break;
                        case 12:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "2. <color=#ff1212>같은 과일 연속발현</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text += texts[TextIndex][i];
                            break;
                    }
                }
                break;
            case 24:
                for (int i = 0; i < texts[TextIndex].Length; i++)
                {
                    switch (i)
                    {
                        case 15:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "설명을 줄일게요,\n바나나는 <color=#FFD700>『</color>";
                            break;
                        case 16:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "설명을 줄일게요,\n바나나는 <color=#FFD700>『W</color>";
                            break;
                        case 17:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "설명을 줄일게요,\n바나나는 <color=#FFD700>『W』</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text += texts[TextIndex][i];
                            break;
                    }
                }
                break;
            case 32:
                for (int i = 0; i < texts[TextIndex].Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>섞</color>";
                            break;
                        case 2:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>섞</color> <color=#FFD700>어</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>섞</color> <color=#FFD700>어</color> <color=#FF1212>서</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text += texts[TextIndex][i];
                            break;
                    }
                    
                }
                break;
            default:
                for (int i = 0; i < texts[TextIndex].Length; i++)
                {
                    yield return new WaitForSeconds(textSpeed);
                    introduceText.text += texts[TextIndex][i];
                }
                break;
        }

        switch (TextIndex) // 스킵 여부 or 끝나고 이동 가능
        {
            case 2 or 6 or 12: // 초반부분 이동시작
                GameManager.Instance.player.isMove = true;
                break;
            case 23 or 29 or 34 or 36 or 40: // 중후반부분 이동시작 and 연습 가능
                GameManager.Instance.player.isMove = true;
                GameManager.Instance.player.isFruitStop = false;
                break;
            case 14: // Q 준비 테스트
                GameManager.Instance.player.isFruitStop = false;
                TmpCheck1 = 1; 
                break;
            case 16:
                GameManager.Instance.player.isFruitStop = false;
                skipNum = TextIndex + 1;
                isSkip = true;
                break;
            default:
                skipNum = TextIndex + 1;
                skipGuide.SetActive(true);
                isSkip = true;
                break;
        }

        AudioManager.instance.StopSfx(AudioManager.Sfx.Typing);
    }

    IEnumerator AlphaOn()
    {
        canvasGroup.alpha = 0f;
        for (float i = 0; i < 1; i += 0.1f)
        {
            canvasGroup.alpha = i;
            yield return new WaitForSeconds(0.025f);
        }
    }

    IEnumerator BoxOn()
    {
        boxAnim.SetBool("Live", true);
        for (float i = 0; i < 1; i += 0.1f) // ??
        {
            yield return new WaitForSeconds(0.025f);
        }
    }

    IEnumerator BoxOff()
    {
        boxAnim.SetBool("Live", false);
        for (float i = 0; i < 1; i -= 0.1f)
        {
            yield return new WaitForSeconds(0.025f);
        }
    }

    public void TextSet()
    {
        introduceText.color = Color.black;
        texts[0] = "안녕하세요!";
        texts[1] = "우선 저 앞으로\n달려볼까요?";
        texts[2] = "방향키를 눌러\n좌우로 이동하기";
        texts[3] = "잘했어요!";
        texts[4] = "어.. 근데\n저 사과는 뭐죠?";
        texts[5] = "'점프를 이용할 수 있다'\n라고 적혀있는데...";
        texts[6] = "사과를 향해\n이동하기";
        texts[7] = "맞아요.. 간단하게\n설명해 드릴게요";
        texts[8] = "보통 기본적으로\n쓸 수 있는 점프조차";
        texts[9] = "여기에서는\n다른 방법으로 사용해요";
        texts[10] = "특정한 키를 눌러\n과일 능력을 준비하고";
        texts[11] = "Space Bar를 눌러\n능력을 발현할 수 있어요";
        texts[12] = "이해하지 못했어도 좋아요,\n저 앞에서 한번 해보죠";
        texts[13] = "바로 갈게요, 사과는\n『Q』로 준비할 수 있어요";
        texts[14] = "『Q』를 눌러\n집중 모드로 돌입하기";
        texts[15] = "잘했어요,\n이번에는 발현이에요";
        texts[16] = "『Space Bar』를 눌러\n능력 발현하기";
        texts[17] = "정확해요,\n이게 전부에요";
        texts[18] = "준비와 발현의 반복.";
        texts[19] = "하지만 2가지\n주의사항이 있어요";
        texts[20] = "1. 항상 모든 칸을\n준비/발현해야만 전환된다";
        texts[21] = "2. 같은 과일 연속발현에는\n약간의 쿨타임이 존재한다";
        texts[22] = "슬슬 끝나가네요,\n이젠 여러 과일을 다뤄보죠";
        texts[23] = "마음껏 연습하고\n오른쪽으로 이동하기";
        texts[24] = "설명을 줄일게요,\n바나나는 『W』로 준비하고";
        texts[25] = "발현 시 앞으로\n빠르게 돌진해요";
        texts[26] = "사과와 섞어서\n준비 및 발현시키면...";
        texts[27] = "더욱 빠르게 또는\n더욱 높이 뛸 수 있겠죠?";
        texts[28] = "준비해둔 코스 끝에서\n기다리고 있을게요.";
        texts[29] = "Q, W를 활용해\n코스 극복하기";
        texts[30] = "아직은 좀\n어려운가요?";
        texts[31] = "이정도 거리면\n마구잡이로 뛰기보단...";
        texts[32] = "섞 어 서 준비해야\n지나갈 수 있겠네요.";
        texts[33] = "어느정도 힌트가\n되었을까요?";
        texts[34] = "다시 한 번\n도전하기";
        texts[35] = "잘했어요.\n이제 두 번 남았어요";
        texts[36] = "장거리 도약\n코스 클리어하기";
        texts[37] = "드디어\n마지막이네요";
        texts[38] = "높이 뛰는 것도\n할 수 있겠죠?";
        texts[39] = "코스 끝에서 봐요.";
        texts[40] = "마지막 코스\n클리어하기";
    }

}


