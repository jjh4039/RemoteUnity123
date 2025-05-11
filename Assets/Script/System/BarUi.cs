using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class BarUi : MonoBehaviour
{
    [Header("Component")]
    public Text mainText;
    public Outline mainTextOutline;
    public Text errorText;

    [Header("Field")]
    public bool isCooldown;

    public void Start()
    {
        SetMainText(SettingText.none);
    }

    public void Update()
    {
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
                mainText.text = "����� ������ �Է��ϼ���!";
                mainTextOutline.effectColor = new Color(0.15f, 1f, 0f, 0.5f);
                break;
            case SettingText.full:
                mainText.text = "��� ĭ�� �غ�Ǿ����ϴ�!";
                mainTextOutline.effectColor = new Color(0.51f, 0.67f, 1f, 0.5f);
                break;
            case SettingText.cooldown:
                isCooldown = true;
                mainTextOutline.effectColor = new Color(1f, 1f, 1f, 0.5f);
                break;
        }
    }
}
