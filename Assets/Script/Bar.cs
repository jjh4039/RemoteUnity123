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


    void OnEnable()
    {
        StartCoroutine(BarOnAni());
        StartCoroutine(BarOnAlp());
    }

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
        BarsColor[0].gameObject.SetActive(false);
        BarsColor[1].gameObject.SetActive(false);
        BarsColor[2].gameObject.SetActive(false);
        MainBar.sprite = TrunBars[6];
        yield return new WaitForSeconds(0.04f);
        MainBar.sprite = TrunBars[3];
        yield return new WaitForSeconds(0.04f);
        MainBar.sprite = TrunBars[4];
        yield return new WaitForSeconds(0.04f);
        MainBar.sprite = TrunBars[5];
        yield return new WaitForSeconds(0.04f);
        MainBar.sprite = TrunBars[4];
        yield return new WaitForSeconds(0.04f);
        MainBar.sprite = TrunBars[3];
        yield return new WaitForSeconds(0.04f);
        MainBar.sprite = TrunBars[6];
        yield return new WaitForSeconds(0.04f);
        MainBar.sprite = TrunBars[0];
        GameManager.Instance.player.isUseFruit = true;
        BarsColor[0].gameObject.SetActive(true);
        BarsColor[1].gameObject.SetActive(true);
        BarsColor[2].gameObject.SetActive(true);
    }
}
