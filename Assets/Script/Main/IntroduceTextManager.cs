using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class IntroduceTextManager : MonoBehaviour
{
    [HideInInspector] public Text introduceText;
    [HideInInspector] public CanvasGroup canvasGroup;
    public string[] texts; // �ν����Ϳ��� �ؽ�Ʈ �� ����
    public Animator boxAnim;
    public GameObject skipGuide;
    public float textSpeed; // �ؽ�Ʈ �ӵ�
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
        StartCoroutine(FristStep()); // Ʃ�丮�� ����
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
        GameManager.Instance.cutScene.StartCoroutine("PadeIn");
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

        // ����ǥ �̸�Ƽ�� ����
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


    IEnumerator Say(int TextIndex) // ��ȭ��
    {
        skipGuide.SetActive(false);
        StartCoroutine(AlphaOn());
        introduceText.text = "";
        isSkip = false;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Typing);

        if (TextIndex == 15) { Time.timeScale = 1f; Time.fixedDeltaTime = 0.02f * Time.timeScale;} // Ʃ�丮�� ���ο��� ����

        switch (TextIndex) // �ؽ�Ʈ �ڽ� ����
        {
            // ���ۺκ�
            case 0 or 3 or 7 or 13 or 15 or 17 or 24 or 30 or 35 or 37:
                StartCoroutine(BoxOn());
                break;
            // ���κ�
            case 2 or 6 or 12 or 14 or 16 or 23 or 29 or 34 or 36 or 40:
                StartCoroutine(BoxOff());
                break;
        }

        switch (TextIndex) // ����Ʈ ����
        {
            case 2 or 6 or 12 or 14 or 16 or 23 or 29 or 34 or 36 or 40: // ����Ʈ�� �ִ� ��ȣ
                isQuestClear = false;
                break;
            case 15 or 17 or 24 or 30 or 35 or 37: // ����Ʈ Ŭ���� ���� ���� �׳� �������� �Ѿ�� ��ȣ
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
                            introduceText.text = "��.. �ٵ�\n�� <color=#ff696b>��</color>";
                            break;
                        case 10:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "��.. �ٵ�\n�� <color=#ff696b>���</color>";
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
                            introduceText.text = "'<color=#ff696b>��</color>";
                            break;
                        case 2:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "'<color=#ff696b>����</color>";
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
                            introduceText.text = "<color=#ff696b>Ư</color>";
                            break;
                        case 1:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>Ư��</color>";
                            break;
                        case 2:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>Ư����</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>Ư���� Ű</color>";
                            break;
                        case 17:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>Ư���� Ű</color>�� ����\n���� �ɷ��� <color=#ff696b>��</color>";
                            break;
                        case 18:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>Ư���� Ű</color>�� ����\n���� �ɷ��� <color=#ff696b>�غ�</color>";
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
                            introduceText.text = "<color=#FF1212>Space Bar</color>�� ����\n�ɷ��� <color=#FF1212>��</color>";
                            break;
                        case 19:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>Space Bar</color>�� ����\n�ɷ��� <color=#FF1212>����</color>";
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
                            introduceText.text = "�ٷ� ���Կ�, �����\n<color=#ff696b>��</color>";
                            break;
                        case 13:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "�ٷ� ���Կ�, �����\n<color=#ff696b>��Q</color>";
                            break;
                        case 14:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "�ٷ� ���Կ�, �����\n<color=#ff696b>��Q��</color>";
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
                            introduceText.text = "���߾��,\n�̹����� <color=#FF1212>��</color>";
                            break;
                        case 12:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "���߾��,\n�̹����� <color=#FF1212>����</color>";
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
                            introduceText.text = "<color=#ff696b>��</color>";
                            break;
                        case 1:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>�غ�</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>�غ�</color>�� <color=#ff1212>��</color>";
                            break;
                        case 5:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#ff696b>�غ�</color>�� <color=#ff1212>����</color>";
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
                            introduceText.text = "1. �׻� <color=#ff1212>��</color>";
                            break;
                        case 7: 
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "1. �׻� <color=#ff1212>���</color>";
                            break;
                        case 9:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "1. �׻� <color=#ff1212>��� ĭ</color>";
                            break;
                        case 10:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "1. �׻� <color=#ff1212>��� ĭ��</color>";
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
                            introduceText.text = "2. <color=#ff1212>��</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "2. <color=#ff1212>����</color>";
                            break;
                        case 6:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "2. <color=#ff1212>���� ��</color>";
                            break;
                        case 7:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "2. <color=#ff1212>���� ����</color>";
                            break;
                        case 9:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "2. <color=#ff1212>���� ���� ��</color>";
                            break;
                        case 10:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "2. <color=#ff1212>���� ���� ����</color>";
                            break;
                        case 11:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "2. <color=#ff1212>���� ���� ���ӹ�</color>";
                            break;
                        case 12:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "2. <color=#ff1212>���� ���� ���ӹ���</color>";
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
                            introduceText.text = "������ ���ϰԿ�,\n�ٳ����� <color=#FFD700>��</color>";
                            break;
                        case 16:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "������ ���ϰԿ�,\n�ٳ����� <color=#FFD700>��W</color>";
                            break;
                        case 17:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "������ ���ϰԿ�,\n�ٳ����� <color=#FFD700>��W��</color>";
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
                            introduceText.text = "<color=#FF1212>��</color>";
                            break;
                        case 2:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>��</color> <color=#FFD700>��</color>";
                            break;
                        case 4:
                            yield return new WaitForSeconds(textSpeed);
                            introduceText.text = "<color=#FF1212>��</color> <color=#FFD700>��</color> <color=#FF1212>��</color>";
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

        switch (TextIndex) // ��ŵ ���� or ������ �̵� ����
        {
            case 2 or 6 or 12: // �ʹݺκ� �̵�����
                GameManager.Instance.player.isMove = true;
                break;
            case 23 or 29 or 34 or 36 or 40: // ���Ĺݺκ� �̵����� and ���� ����
                GameManager.Instance.player.isMove = true;
                GameManager.Instance.player.isFruitStop = false;
                break;
            case 14: // Q �غ� �׽�Ʈ
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
        texts[0] = "�ȳ��ϼ���!";
        texts[1] = "�켱 �� ������\n�޷������?";
        texts[2] = "����Ű�� ����\n�¿�� �̵��ϱ�";
        texts[3] = "���߾��!";
        texts[4] = "��.. �ٵ�\n�� ����� ����?";
        texts[5] = "'������ �̿��� �� �ִ�'\n��� �����ִµ�...";
        texts[6] = "����� ����\n�̵��ϱ�";
        texts[7] = "�¾ƿ�.. �����ϰ�\n������ �帱�Կ�";
        texts[8] = "���� �⺻������\n�� �� �ִ� ��������";
        texts[9] = "���⿡����\n�ٸ� ������� ����ؿ�";
        texts[10] = "Ư���� Ű�� ����\n���� �ɷ��� �غ��ϰ�";
        texts[11] = "Space Bar�� ����\n�ɷ��� ������ �� �־��";
        texts[12] = "�������� ���߾ ���ƿ�,\n�� �տ��� �ѹ� �غ���";
        texts[13] = "�ٷ� ���Կ�, �����\n��Q���� �غ��� �� �־��";
        texts[14] = "��Q���� ����\n���� ���� �����ϱ�";
        texts[15] = "���߾��,\n�̹����� �����̿���";
        texts[16] = "��Space Bar���� ����\n�ɷ� �����ϱ�";
        texts[17] = "��Ȯ�ؿ�,\n�̰� ���ο���";
        texts[18] = "�غ�� ������ �ݺ�.";
        texts[19] = "������ 2����\n���ǻ����� �־��";
        texts[20] = "1. �׻� ��� ĭ��\n�غ�/�����ؾ߸� ��ȯ�ȴ�";
        texts[21] = "2. ���� ���� ���ӹ�������\n�ణ�� ��Ÿ���� �����Ѵ�";
        texts[22] = "���� �������׿�,\n���� ���� ������ �ٷﺸ��";
        texts[23] = "������ �����ϰ�\n���������� �̵��ϱ�";
        texts[24] = "������ ���ϰԿ�,\n�ٳ����� ��W���� �غ��ϰ�";
        texts[25] = "���� �� ������\n������ �����ؿ�";
        texts[26] = "����� ���\n�غ� �� ������Ű��...";
        texts[27] = "���� ������ �Ǵ�\n���� ���� �� �� �ְ���?";
        texts[28] = "�غ��ص� �ڽ� ������\n��ٸ��� �����Կ�.";
        texts[29] = "Q, W�� Ȱ����\n�ڽ� �غ��ϱ�";
        texts[30] = "������ ��\n������?";
        texts[31] = "������ �Ÿ���\n�������̷� �ٱ⺸��...";
        texts[32] = "�� �� �� �غ��ؾ�\n������ �� �ְڳ׿�.";
        texts[33] = "������� ��Ʈ��\n�Ǿ������?";
        texts[34] = "�ٽ� �� ��\n�����ϱ�";
        texts[35] = "���߾��.\n���� �� �� ���Ҿ��";
        texts[36] = "��Ÿ� ����\n�ڽ� Ŭ�����ϱ�";
        texts[37] = "����\n�������̳׿�";
        texts[38] = "���� �ٴ� �͵�\n�� �� �ְ���?";
        texts[39] = "�ڽ� ������ ����.";
        texts[40] = "������ �ڽ�\nŬ�����ϱ�";
    }

}


