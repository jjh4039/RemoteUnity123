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

    private void Awake()
    {
        GameManager.Instance.player.isMove = false;
        StartCoroutine(FristStep()); // 튜토리얼 시작
        TextSet(); // String Setting
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
    }

    IEnumerator FristStep()
    {
        introduceText.rectTransform.anchoredPosition = new Vector2(-1281.47f, -720.26f);
        isQuestClear = true;
        StartCoroutine(Say(0));
        yield return new WaitForSeconds(1.9f);
        StartCoroutine(Say(1));
        yield return new WaitForSeconds(2.9f);
        isQuestClear = false;
        StartCoroutine(Say(2));
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.player.isMove = true;
    }

    IEnumerator SecondStep()
    {
        introduceText.rectTransform.anchoredPosition = new Vector2(-1271.35f, -720.26f);
        isQuestClear = true;
        StartCoroutine(Say(3));
        yield return new WaitForSeconds(1.9f);
        StartCoroutine(Say(4));
        yield return new WaitForSeconds(2.9f);
        StartCoroutine(Say(5));
        yield return new WaitForSeconds(4f);
        isQuestClear = false;
        StartCoroutine(Say(6));
        yield return new WaitForSeconds(1.1f);
        GameManager.Instance.player.isMove = true;
    }

    IEnumerator ThirdStep()
    {
        bar.SetActive(true);
        yield return new WaitForSeconds(1.9f);
    }

    IEnumerator Say(int TextIndex) // 대화용
    {
        StartCoroutine(AlphaOn());
        introduceText.text = "";

        switch (TextIndex)
        {
            case 4: 
                for (int i = 0; i < texts[TextIndex].Length; i++)
                {
                    switch (i)
                    {
                        case 9:
                            yield return new WaitForSeconds(0.09f);
                            introduceText.text = "어.. 근데\n저 <color=#ff696b>사</color>";
                            break;
                        case 10:
                            yield return new WaitForSeconds(0.09f);
                            introduceText.text = "어.. 근데\n저 <color=#ff696b>사과</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(0.09f);
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
                            yield return new WaitForSeconds(0.09f);
                            introduceText.text = "'<color=#ff696b>점</color>";
                            break;
                        case 2:
                            yield return new WaitForSeconds(0.09f);
                            introduceText.text = "'<color=#ff696b>점프</color>";
                            break;
                        default:
                            yield return new WaitForSeconds(0.09f);
                            introduceText.text += texts[TextIndex][i];
                            break;
                    }
                }
                break;
            default:
                for (int i = 0; i < texts[TextIndex].Length; i++)
                {
                    yield return new WaitForSeconds(0.09f);
                    introduceText.text += texts[TextIndex][i];
                }
                break;
        }

    }

    IEnumerator AlphaOn()
    {
        canvasGroup.alpha = 0f;
        for (float i = 0; i < 1; i += 0.1f)
        {
            canvasGroup.alpha = i;
            yield return new WaitForSeconds(0.05f);
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
    }
}


