using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KoreanTyper;

public class KoreanTyperDemo_Auto : MonoBehaviour {
    public Text[] TestTexts;
    public CanvasGroup TextCG;

    public bool isSkip = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isSkip == true)
        {
            TextCG.alpha = 0f;
            GameManager.Instance.player.MoveStart();
            Dialogues.isDialogue = false;
            isSkip = false;
        }

        if (isSkip == true)
        {
            GameManager.Instance.player.isFruitStop = true;
        }
    }

    public IEnumerator TypingText(int textIndex) {

        TextCG.alpha = 1f;
        GameManager.Instance.player.MoveStop();

        isSkip = false;
        string[] strings = new string[3]{ "신선한 과일들이 가득 쌓여있다.",
                                          "평범한 과일이다.",
                                          "이 데모는 자동으로 작성되고 있습니다." };
        TestTexts[0].text = "";

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Typing, 0);
        int strTypingLength = strings[textIndex].GetTypingLength();

        for (int i = 0; i <= strTypingLength; i++) {
            TestTexts[0].text = strings[textIndex].Typing(i);
            yield return new WaitForSeconds(0.02f);
        }

        AudioManager.instance.StopSfx(AudioManager.Sfx.Typing);

        isSkip = true;
    }
}

