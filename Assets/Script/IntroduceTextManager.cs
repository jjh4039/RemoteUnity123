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
        StartCoroutine(FristStep()); // Ʃ�丮�� ����
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

        if (TmpCheck1 > 0) // Ʃ�丮��� �غ� ����
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
        isQuestClear = true; // ó���� �⺻�� ����
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
        bar.SetActive(true); // ���� �� Ui Ȱ��ȭ
        StartCoroutine(Say(7));
        yield return new WaitForSeconds(0f);
    }

    IEnumerator FourthStep()
    {
        introduceText.rectTransform.anchoredPosition = new Vector2(-1255.554f, -720.26f);
        StartCoroutine(Say(13));
        yield return new WaitForSeconds(0f);
    }
    

    IEnumerator Say(int TextIndex) // ��ȭ��
    {
        StartCoroutine(AlphaOn());
        introduceText.text = "";
        isSkip = false;

        switch (TextIndex) // ����Ʈ ����
        {
            case 2 or 6 or 12 or 14 or 16 or 23: // ����Ʈ�� �ִ� ��ȣ
                isQuestClear = false;
                break;
            case 15 or 17: // ����Ʈ Ŭ���� ���� ���� �׳� �������� �Ѿ�� ��ȣ
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
                            introduceText.text = "��.. �ٵ�\n�� <color=#ff696b>��</color>";
                            break;
                        case 10:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "��.. �ٵ�\n�� <color=#ff696b>���</color>";
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
                            introduceText.text = "'<color=#ff696b>��</color>";
                            break;
                        case 2:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "'<color=#ff696b>����</color>";
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
                            introduceText.text = "<color=#ff696b>Ư</color>";
                            break;
                        case 1:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>Ư��</color>";
                            break;
                        case 2:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>Ư����</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>Ư���� Ű</color>";
                            break;
                        case 17:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>Ư���� Ű</color>�� ����\n���� �ɷ��� <color=#ff696b>��</color>";
                            break;
                        case 18:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>Ư���� Ű</color>�� ����\n���� �ɷ��� <color=#ff696b>�غ�</color>";
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
                            introduceText.text = "<color=#FF1212>Space Bar</color>Ű�� ����\n�ɷ��� <color=#FF1212>��</color>";
                            break;
                        case 20:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#FF1212>Space Bar</color>Ű�� ����\n�ɷ��� <color=#FF1212>����</color>";
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
                            introduceText.text = "�ٷ� ���Կ�, �����\n<color=#ff696b>��</color>";
                            break;
                        case 13:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "�ٷ� ���Կ�, �����\n<color=#ff696b>��Q</color>";
                            break;
                        case 14:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "�ٷ� ���Կ�, �����\n<color=#ff696b>��Q��</color>";
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
                            introduceText.text = "���߾��,\n�̹����� <color=#FF1212>��</color>";
                            break;
                        case 12:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "���߾��,\n�̹����� <color=#FF1212>����</color>";
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
                            introduceText.text = "<color=#ff696b>��</color>";
                            break;
                        case 1:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>�غ�</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>�غ�</color>�� <color=#ff1212>��</color>";
                            break;
                        case 5:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "<color=#ff696b>�غ�</color>�� <color=#ff1212>����</color>";
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
                            introduceText.text = "1. �׻� <color=#ff1212>��</color>";
                            break;
                        case 7:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "1. �׻� <color=#ff1212>���</color>";
                            break;
                        case 9:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "1. �׻� <color=#ff1212>��� ĭ</color>";
                            break;
                        case 10:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "1. �׻� <color=#ff1212>��� ĭ��</color>";
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
                            introduceText.text = "2. <color=#ff1212>��</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "2. <color=#ff1212>����</color>";
                            break;
                        case 6:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "2. <color=#ff1212>���� ��</color>";
                            break;
                        case 7:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "2. <color=#ff1212>���� ����</color>";
                            break;
                        case 21:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "2. <color=#ff1212>���� ����</color> ���� ��������\n1���� <color=#ff1212>��</color>";
                            break;
                        case 22:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "2. <color=#ff1212>���� ����</color> ���� ��������\n1���� <color=#ff1212>��Ÿ</color>";
                            break;
                        case 23:
                            yield return new WaitForSeconds(0.06f);
                            introduceText.text = "2. <color=#ff1212>���� ����</color> ���� ��������\n1���� <color=#ff1212>��Ÿ��</color>";
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
        } // �ؽ�Ʈ ���� ����

        switch (TextIndex) // ��ŵ ���� or ������ �̵� ����
        {
            case 2 or 6 or 12: // �ʹݺκ� �̵�����
                GameManager.Instance.player.isMove = true;
                break;
            case 23: // ���Ĺݺκ� �̵����� and ���� ����
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
        texts[0] = "�ȳ��ϼ���!";
        texts[1] = "�켱 �� ������\n�޷������?";
        texts[2] = "����Ű�� ����\n�¿�� �̵��ϱ�";
        texts[3] = "���߾��!";
        texts[4] = "��.. �ٵ�\n�� ����� ����? ";
        texts[5] = "'������ �̿��� �� �ִ�'\n��� �����ִµ�...";
        texts[6] = "����� ����\n�̵��ϱ�";
        texts[7] = "�¾ƿ�.. �����ϰ�\n������ �帱�Կ�";
        texts[8] = "���� �⺻������\n�� �� �ִ� ��������";
        texts[9] = "���⿡����\n�ٸ� ������� ����ؿ�";
        texts[10] = "Ư���� Ű�� ����\n���� �ɷ��� �غ��ϰ�";
        texts[11] = "Space BarŰ�� ����\n�ɷ��� ������ų �� �־��";
        texts[12] = "�������� ���߾ ���ƿ�,\n�� �տ��� �ѹ� �غ���";
        texts[13] = "�ٷ� ���Կ�, �����\n��Q��Ű�� �غ��� �� �־��";
        texts[14] = "��Q��Ű�� 3�� ����\n���� �غ� �ܰ�� �����ϱ�";
        texts[15] = "���߾��,\n�̹����� �����̿���";
        texts[16] = "��Space Bar���� ����\n�ɷ� �����ϱ�";
        texts[17] = "��Ȯ�ؿ�,\n�̰� ���ο���.";
        texts[18] = "�غ�� ������ �ݺ�.";
        texts[19] = "������ 2����\n���ǻ����� �־��";
        texts[20] = "1. �׻� ��� ĭ��\n�غ�/�����ؾ� ���°� ��ȯ�ȴ�";
        texts[21] = "2. ���� ���� ���� ��������\n1���� ��Ÿ���� �����Ѵ�";
        texts[22] = "���� �������׿�,\n���� ���� ������ �ٷﺸ��";
        texts[23] = "������ �����ϰ�\n���������� �̵��ϱ�";
    }
}


