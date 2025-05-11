using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class BarUi : MonoBehaviour
{
    [Header("Component")]
    public Text mainText;
    public Outline mainTextOutline;
    public CanvasGroup errorText;
    public GameObject focus;

    [Header("Field")]
    public bool isCooldown;

    public void Start()
    {
        SetMainText(SettingText.none);
    }

    public void Update()
    {
        focus.transform.position = GameManager.Instance.player.transform.position;

        if (isCooldown == true)
        {
            mainText.text = GameManager.Instance.bar.BarCoolDownTime.ToString("0.0s");
        }
    }

    public enum SettingText
    {
        none,
        ready,
        full,
        cooldown,
    }

    IEnumerator Error()
    {
        errorText.alpha = 1f;
        for (int i = 0; i < 100; i++)
        {
            errorText.alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            if (errorText.alpha <= 0f) break;
        }
    }

    IEnumerator FoucsEffect()
    {
        for (float i = 0; i < 40f; i += 0.2f)
        {
            focus.transform.position = GameManager.Instance.player.transform.position;
            focus.transform.localScale = new Vector3(i, i, 1);
            yield return new WaitForSeconds(0.0004f);
        }
    }

    IEnumerator FoucsEffectOff()
    {
        for (float i = 40f; i > 0f; i -= 0.3f)
        {
            focus.transform.position = GameManager.Instance.player.transform.position;
            focus.transform.localScale = new Vector3(i, i, 1);
            yield return new WaitForSeconds(0.0004f);
        }
        focus.transform.localScale = new Vector3(0f, 0f, 1);
    }

    public void SetMainText(SettingText setting)
    {
        switch (setting)
        {
            case SettingText.none:
                isCooldown = false;
                mainText.text = "";
                mainTextOutline.effectColor = new Color(0f, 0f, 0f, 0f);
                break;
            case SettingText.ready:
                mainText.text = "사용할 과일을 입력하세요!";
                mainTextOutline.effectColor = new Color(0.15f, 1f, 0f, 0.5f);
                break;
            case SettingText.full:
                mainText.text = "모든 칸이 준비되었습니다!";
                mainTextOutline.effectColor = new Color(0.51f, 0.67f, 1f, 0.5f);
                break;
            case SettingText.cooldown:
                isCooldown = true;
                mainTextOutline.effectColor = new Color(1f, 1f, 1f, 0.5f);
                break;
        }
    }
}
