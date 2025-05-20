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
        string[] strings = new string[4]{ "신선한 과일들이 가득 쌓여있다.",
                                          "방금까지 누군가 앉아 있던 곳 같다...",
                                          "    시련의 나무 >>\n              << 씨앗 마을\n\n    ...흔한 표지판이다.",
                                          "< 모집 공고 >\n마을을 수..&!@.\n\n포스터가 찢어져 있어 읽기 힘들어 보인다..."};
        TestTexts[0].text = "";

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Typing, 0);
        int strTypingLength = strings[textIndex].GetTypingLength();

        for (int i = 0; i <= strTypingLength; i++) {
            TestTexts[0].text = strings[textIndex].Typing(i);
            yield return new WaitForSeconds(0.015f);
        }

        AudioManager.instance.StopSfx(AudioManager.Sfx.Typing);
        isSkip = true;
    }
}

