using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem.Android;

public class Bar : MonoBehaviour
{
    public Image MainBar;
    public CanvasGroup MainBarAlp;
    public Image[] BarsColor;
    public Sprite[] TrunBars;

    public float BarCoolDownTime = 2.2f;

    void OnEnable()
    {
        StartCoroutine(BarOnAni());
        StartCoroutine(BarOnAlp());
    }

    void Update()
    {
        if (BarCoolDownTime >= 0)
        {
            BarCoolDownTime -= Time.deltaTime;
        }
    }

    // �� Ȱ��ȭ �ִϸ��̼�
    IEnumerator BarOnAni()
    {
        MainBar.sprite = TrunBars[5];
        yield return new WaitForSeconds(0.1f);
        MainBar.sprite = TrunBars[4];
        yield return new WaitForSeconds(0.1f);
        MainBar.sprite = TrunBars[3];
        yield return new WaitForSeconds(0.1f);
        MainBar.sprite = TrunBars[6];
        yield return new WaitForSeconds(0.1f);
        MainBar.sprite = TrunBars[0];
        BarsColor[0].gameObject.SetActive(true);
        BarsColor[1].gameObject.SetActive(true);
        BarsColor[2].gameObject.SetActive(true);
    }

    // �� Ȱ��ȭ ���̵���
    IEnumerator BarOnAlp()
    {
        MainBarAlp.alpha = 0;
        for (int i = 0; i <= 10; i++)
        {
            MainBarAlp.alpha += 0.1f;
            yield return new WaitForSeconds(0.04f);
        }
    }

    public IEnumerator BarReadying()
    {
        BarCoolDownTime = 2.2f;
        GameManager.Instance.barUi.SetMainText(BarUi.SettingText.cooldown); // �� UI �ؽ�Ʈ ����

        GameManager.Instance.player.WhiteResetFruit(); // �� �ʱ�ȭ
        MainBar.color = Color.white;

        yield return new WaitForSeconds(0.02f);
        BarsColor[0].gameObject.SetActive(false);
        BarsColor[1].gameObject.SetActive(false);
        BarsColor[2].gameObject.SetActive(false);
        MainBar.sprite = TrunBars[6];
        yield return new WaitForSeconds(0.04f);
        MainBar.sprite = TrunBars[3];
        yield return new WaitForSeconds(0.04f);
        MainBar.sprite = TrunBars[4];
        yield return new WaitForSeconds(0.05f);
        MainBar.sprite = TrunBars[5];
        yield return new WaitForSeconds(0.05f);
        MainBar.sprite = TrunBars[7];
        yield return new WaitForSeconds(0.4f);
        MainBar.sprite = TrunBars[5];
        yield return new WaitForSeconds(0.4f);
        MainBar.sprite = TrunBars[4];
        yield return new WaitForSeconds(0.4f);
        MainBar.sprite = TrunBars[3];
        yield return new WaitForSeconds(0.4f);
        MainBar.sprite = TrunBars[6];
        yield return new WaitForSeconds(0.4f);
        MainBar.sprite = TrunBars[0];
        GameManager.Instance.barUi.SetMainText(BarUi.SettingText.none);
        GameManager.Instance.player.isReadyFruit = true;
        BarsColor[0].gameObject.SetActive(true);
        BarsColor[1].gameObject.SetActive(true);
        BarsColor[2].gameObject.SetActive(true);
    }

    // ��밡�� ������ ���ϱ� 
    public void UseColorChange(int index, int colorId)
    {
        switch (colorId)
        {
            case 1:
                BarsColor[index].color = new Color(1f, 0.3f, 0.24f, 1f);
                break;
            case 2:
                BarsColor[index].color = new Color(1f, 0.93f, 0f, 1f);
                break;
        }
    }
}
