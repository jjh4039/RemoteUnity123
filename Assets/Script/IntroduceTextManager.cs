using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class IntroduceTextManager : MonoBehaviour
{
    public Text introduceText;
    public CanvasGroup canvasGroup;
    public string[] texts;
    public bool isQuestClear;
    public GameObject bar;
    public bool isSkip;
    public int skipNum;
    public int TmpCheck1;

    private void Awake()
    {
        GameManager.Instance.player.isMove = false;
        StartCoroutine(FristStep()); // 튜토리얼 시작
        TextSet(); // String Setting
        skipNum = 0;
        TmpCheck1 = -1;
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

        if (Input.GetKeyDown(KeyCode.Space) && isSkip == true || (Input.GetKeyDown(KeyCode.Return)) && isSkip == true)
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
    

    IEnumerator Say(int TextIndex) // 대화용
    {
        StartCoroutine(AlphaOn());
        introduceText.text = "";
        isSkip = false;

        switch (TextIndex) // 퀘스트 관리
        {
            case 2 or 6 or 12 or 14 or 16 or 23: // 퀘스트를 주는 번호
                isQuestClear = false;
                break;
            case 15 or 17: // 퀘스트 클리어 따로 없이 그냥 다음으로 넘어가는 번호
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
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "어.. 근데\n저 <color=#ff696b>사</color>";
                            break;
                        case 10:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "어.. 근데\n저 <color=#ff696b>사과</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(0.06f);
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
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "'<color=#ff696b>점</color>";
                            break;
                        case 2:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "'<color=#ff696b>점프</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(0.06f);
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
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>특</color>";
                            break;
                        case 1:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>특정</color>";
                            break;
                        case 2:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>특정한</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>특정한 키</color>";
                            break;
                        case 17:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>특정한 키</color>를 눌러\n과일 능력을 <color=#ff696b>준</color>";
                            break;
                        case 18:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>특정한 키</color>를 눌러\n과일 능력을 <color=#ff696b>준비</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(0.06f);
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
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#FF1212>S</color>";
                            break;
                        case 1:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#FF1212>Sp</color>";
                            break;
                        case 2:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#FF1212>Spa</color>";
                            break;
                        case 3:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#FF1212>Spac</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#FF1212>Space</color>";
                            break;
                        case 6:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#FF1212>Space B</color>";
                            break;
                        case 7:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#FF1212>Space Ba</color>";
                            break;
                        case 8:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#FF1212>Space Bar</color>";
                            break;
                        case 19:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#FF1212>Space Bar</color>키를 눌러\n능력을 <color=#FF1212>발</color>";
                            break;
                        case 20:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#FF1212>Space Bar</color>키를 눌러\n능력을 <color=#FF1212>발현</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(0.06f);
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
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "바로 갈게요, 사과는\n<color=#ff696b>『</color>";
                            break;
                        case 13:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "바로 갈게요, 사과는\n<color=#ff696b>『Q</color>";
                            break;
                        case 14:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "바로 갈게요, 사과는\n<color=#ff696b>『Q』</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(0.06f);
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
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "잘했어요,\n이번에는 <color=#FF1212>발</color>";
                            break;
                        case 12:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "잘했어요,\n이번에는 <color=#FF1212>발현</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(0.06f);
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
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>준</color>";
                            break;
                        case 1:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>준비</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>준비</color>와 <color=#ff1212>발</color>";
                            break;
                        case 5:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>준비</color>와 <color=#ff1212>발현</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(0.06f);
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
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "1. 항상 <color=#ff1212>모</color>";
                            break;
                        case 7:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "1. 항상 <color=#ff1212>모든</color>";
                            break;
                        case 9:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "1. 항상 <color=#ff1212>모든 칸</color>";
                            break;
                        case 10:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "1. 항상 <color=#ff1212>모든 칸을</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(0.06f);
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
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "2. <color=#ff1212>같</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "2. <color=#ff1212>같은</color>";
                            break;
                        case 6:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "2. <color=#ff1212>같은 과</color>";
                            break;
                        case 7:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "2. <color=#ff1212>같은 과일</color>";
                            break;
                        case 21:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "2. <color=#ff1212>같은 과일</color> 연속 발현에는\n1초의 <color=#ff1212>쿨</color>";
                            break;
                        case 22:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "2. <color=#ff1212>같은 과일</color> 연속 발현에는\n1초의 <color=#ff1212>쿨타</color>";
                            break;
                        case 23:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "2. <color=#ff1212>같은 과일</color> 연속 발현에는\n1초의 <color=#ff1212>쿨타임</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text += texts[TextIndex][i];
                            break;
                    }
                }
                break;
            default:
                for (int i = 0; i < texts[TextIndex].Length; i++)
                {
                    yield return new WaitForSeconds(0.06f);
                    introduceText.text += texts[TextIndex][i];
                }
                break;
        } // 텍스트 색상 여부

        switch (TextIndex) // 스킵 여부 or 끝나고 이동 가능
        {
            case 2 or 6 or 12: // 초반부분 이동시작
                GameManager.Instance.player.isMove = true;
                break;
            case 23: // 중후반부분 이동시작 and 연습 가능
                GameManager.Instance.player.isMove = true;
                GameManager.Instance.player.isFruitStop = false;
                break;
            case 14:
                GameManager.Instance.player.isFruitStop = false;
                TmpCheck1 = 3;
                break;
            case 16:
                GameManager.Instance.player.isFruitStop = false;
                skipNum = TextIndex + 1;
                isSkip = true;
                break;
            default:
                skipNum = TextIndex + 1;
                isSkip = true;
                break;
        }
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

    public void TextSet()
    {
        introduceText.color = Color.black;
        texts[0] = "안녕하세요!";
        texts[1] = "우선 저 앞으로\n달려볼까요?";
        texts[2] = "방향키를 눌러\n좌우로 이동하기";
        texts[3] = "잘했어요!";
        texts[4] = "어.. 근데\n저 사과는 뭐죠? ";
        texts[5] = "'점프를 이용할 수 있다'\n라고 적혀있는데...";
        texts[6] = "사과를 향해\n이동하기";
        texts[7] = "맞아요.. 간단하게\n설명해 드릴게요";
        texts[8] = "보통 기본적으로\n쓸 수 있는 점프조차";
        texts[9] = "여기에서는\n다른 방법으로 사용해요";
        texts[10] = "특정한 키를 눌러\n과일 능력을 준비하고";
        texts[11] = "Space Bar키를 눌러\n능력을 발현시킬 수 있어요";
        texts[12] = "이해하지 못했어도 좋아요,\n저 앞에서 한번 해보죠";
        texts[13] = "바로 갈게요, 사과는\n『Q』키로 준비할 수 있어요";
        texts[14] = "『Q』키를 3번 눌러\n발현 준비 단계로 돌입하기";
        texts[15] = "잘했어요,\n이번에는 발현이에요";
        texts[16] = "『Space Bar』를 눌러\n능력 발현하기";
        texts[17] = "정확해요,\n이게 전부에요.";
        texts[18] = "준비와 발현의 반복.";
        texts[19] = "하지만 2가지\n주의사항이 있어요";
        texts[20] = "1. 항상 모든 칸을\n준비/발현해야 상태가 전환된다";
        texts[21] = "2. 같은 과일 연속 발현에는\n1초의 쿨타임이 존재한다";
        texts[22] = "슬슬 끝나가네요,\n이젠 여러 과일을 다뤄보죠";
        texts[23] = "마음껏 연습하고\n오른쪽으로 이동하기";
    }
}


