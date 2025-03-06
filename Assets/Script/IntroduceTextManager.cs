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
        StartCoroutine(FristStep()); // Ʃ�丮�� ����
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

    IEnumerator Say(int TextIndex) // ��ȭ��
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
                            introduceText.text = "��.. �ٵ�\n�� <color=#ff696b>��</color>";
                            break;
                        case 10:
                            yield return new WaitForSeconds(0.09f);
                            introduceText.text = "��.. �ٵ�\n�� <color=#ff696b>���</color>";
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
                            introduceText.text = "'<color=#ff696b>��</color>";
                            break;
                        case 2:
                            yield return new WaitForSeconds(0.09f);
                            introduceText.text = "'<color=#ff696b>����</color>";
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
        texts[0] = "�ȳ��ϼ���!";
        texts[1] = "�켱 �� ������\n�޷������?";
        texts[2] = "����Ű�� ����\n�¿�� �̵��ϱ�";
        texts[3] = "���߾��!";
        texts[4] = "��.. �ٵ�\n�� ����� ����? ";
        texts[5] = "'������ �̿��� �� �ִ�'\n��� �����ִµ�...";
        texts[6] = "����� ����\n�̵��ϱ�";
    }
}


