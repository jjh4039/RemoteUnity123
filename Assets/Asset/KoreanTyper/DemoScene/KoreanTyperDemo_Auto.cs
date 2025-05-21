using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KoreanTyper;

public class KoreanTyperDemo_Auto : MonoBehaviour {
    public Text[] TestTexts;
    public CanvasGroup TextCG;
    public CanvasGroup ArrowCG;
    public RectTransform arrow;
    public CanvasGroup NpcCG;
    public CanvasGroup ChoiceArrowCG;
    public RectTransform choiceArrow;

    public bool isSkip = false;
    public bool isSpace = false;
    public bool isChoice = false;
    public int nowTextIndex;
    public int myChoiceIndex;

    void Start()
    {
        StartCoroutine(FlowArrow());
        // StartCoroutine(TypingNpc(0, true)); // 테스트용 NPC 대화 시작
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isSkip == true) // 대화 종료
        {
            TextCG.alpha = 0f;
            GameManager.Instance.player.MoveStart();
            BackGround.backGroundMoveStop = false;
            Dialogues.isDialogue = false;
            isSkip = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isSpace == true) // NPC 대화 진행
        {
            isSpace = false;
            switch(nowTextIndex)
            {
                case 0:
                    StartCoroutine(TypingNpc(1, true));
                    break;
                case 1:
                    StartCoroutine(TypingNpc(0, false));
                    break;
            }
        }

        if (isChoice == true)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow) && myChoiceIndex > 0) // 선택지 위로 이동
            {
                myChoiceIndex--;
                ChoiceTextColor();
            }

            if(Input.GetKeyDown(KeyCode.DownArrow) && myChoiceIndex < 2) // 선택지 아래로 이동
            {
                myChoiceIndex++;
                ChoiceTextColor();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isChoice == true) // NPC 대화 진행
        {
            isChoice = false;
            switch (nowTextIndex)
            {
                case 0: // 첫 선택
                    switch (myChoiceIndex)
                    {
                        case 0: // 아무것도 모르겠어요 -> 
                            StartCoroutine(TypingNpc(2, true));
                            break;
                        case 1: // 여긴 어디죠 -> 
                            StartCoroutine(TypingNpc(3, true));
                            break;
                        case 2: // 그동안 들렸던 목소리의 정체 -> 
                            StartCoroutine(TypingNpc(4, true));
                            break;
                    }
                    break;
            }
        }

        if (isSkip == true)
        {
            GameManager.Instance.player.isFruitStop = true;
        }
    }

    public IEnumerator TypingText(int textIndex) {

        ArrowCG.alpha = 0f;
        TextCG.alpha = 1f;
        GameManager.Instance.player.MoveStop();
        BackGround.backGroundMoveStop = true;

        isSkip = false;
        string[] strings = new string[5]{ "신선한 과일들이 가득 쌓여있다.",
                                          "방금까지 누군가 앉아 있던 곳 같다...",
                                          "    시련의 나무 >>\n              << 씨앗 마을\n\n    ...흔한 표지판이다.",
                                          "< 모집 공고 >\n마을을 수..&!@.\n\n포스터가 찢어져 있어 읽기 힘들다...",
                                          "정중앙에 화살이 박힌 과녁...\n시간이 멈춘 듯 낡고 해져있다."};

        for (int i = 0; i < TestTexts.Length; i++) // 초기화
        {
            TestTexts[i].text = "";
        }

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Typing, 0);
        int strTypingLength = strings[textIndex].GetTypingLength();

        for (int i = 0; i <= strTypingLength; i++) {
            TestTexts[0].text = strings[textIndex].Typing(i);
            yield return new WaitForSeconds(0.015f);
        }

        AudioManager.instance.StopSfx(AudioManager.Sfx.Typing);
        ArrowCG.alpha = 1f;
        isSkip = true;
    }

    public IEnumerator TypingNpc(int npcIndex, bool isNpc){

        nowTextIndex = npcIndex;
        ArrowCG.alpha = 0f;
        TextCG.alpha = 1f;
        GameManager.Instance.player.MoveStop();
        BackGround.backGroundMoveStop = true;
        
        isSpace = false;
        isSkip = false;
        string[] NpcStrings = new string[5]{ "처음 보는 얼굴인걸.",
                                          "이 마을에는 무슨 일이지?",
                                          "아무것도 모르겠다라..\n어디서 왔는지도 모른다 이건가",
                                          "이 곳은 씨앗마을이다.\n너 같은 이방인",
                                          "무슨 목소리를 말하는거지?"};

        string[] PlayerStrings = new string[5]{ "아무것도 모르겠다고 답한다.",
                                          "이 곳이 어디인지 묻는다.",
                                          "전까지 들렸던 목소리에 대해 묻는다.",
                                          "",
                                          ""};

        for (int i = 0; i < TestTexts.Length; i++) // 초기화
        {
            TestTexts[i].text = "";
        }

        int strTypingLength;

        switch (isNpc)
        {
            case true:
                ChoiceArrowCG.alpha = 0f;
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Npc, 0);
                NpcCG.alpha = 1f;
                strTypingLength = NpcStrings[npcIndex].GetTypingLength();

                for (int i = 0; i <= strTypingLength; i++)
                {
                    TestTexts[1].text = NpcStrings[npcIndex].Typing(i);
                    yield return new WaitForSeconds(0.015f);
                }
                AudioManager.instance.StopSfx(AudioManager.Sfx.Npc);
                isSpace = true;
                break;

            case false:
                ChoiceArrowCG.alpha = 1f;
                NpcCG.alpha = 0f;
                myChoiceIndex = 0;
                ChoiceTextColor();
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Typing, 0);
                // 가장 긴거로 for문 길이 잡기
                strTypingLength = System.Math.Max(System.Math.Max(PlayerStrings[npcIndex].GetTypingLength(), PlayerStrings[npcIndex + 1].GetTypingLength()), PlayerStrings[npcIndex + 2].GetTypingLength());

                for (int i = 0; i <= strTypingLength; i++)
                {
                    TestTexts[2].text = PlayerStrings[npcIndex].Typing(i);
                    TestTexts[3].text = PlayerStrings[npcIndex + 1].Typing(i);
                    TestTexts[4].text = PlayerStrings[npcIndex + 2].Typing(i);
                    yield return new WaitForSeconds(0.015f);
                }
                AudioManager.instance.StopSfx(AudioManager.Sfx.Typing);
                isChoice = true;
                break;
        }
        ArrowCG.alpha = 1f;
        
        // isSkip = true;
    }

    public void ChoiceTextColor()
    {
        switch (myChoiceIndex)
        {
            case 0:
                choiceArrow.anchoredPosition = new Vector2(-1089.4f, 334.1f);
                TestTexts[2].color = new Color(0, 0, 0, 1);
                TestTexts[3].color = new Color(0, 0, 0, 0.5f);
                TestTexts[4].color = new Color(0, 0, 0, 0.5f);
                break;
            case 1:
                choiceArrow.anchoredPosition = new Vector2(-1089.4f, 223.2f);
                TestTexts[2].color = new Color(0, 0, 0, 0.5f);
                TestTexts[3].color = new Color(0, 0, 0, 1);
                TestTexts[4].color = new Color(0, 0, 0, 0.5f);
                break;
            case 2:
                choiceArrow.anchoredPosition = new Vector2(-1089.4f, 112.8f);
                TestTexts[2].color = new Color(0, 0, 0, 0.5f);
                TestTexts[3].color = new Color(0, 0, 0, 0.5f);
                TestTexts[4].color = new Color(0, 0, 0, 1);
                break;
        }
    }

    public IEnumerator FlowArrow()
    {
        while (true)
        {
            for (int i = 0; i < 60; i++)
            {
                arrow.position = new Vector2(arrow.position.x, arrow.position.y + 0.32f);
                yield return new WaitForSeconds(0.01f);
            }
            for (int i = 0; i < 60; i++)
            {
                arrow.position = new Vector2(arrow.position.x, arrow.position.y - 0.32f);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}

