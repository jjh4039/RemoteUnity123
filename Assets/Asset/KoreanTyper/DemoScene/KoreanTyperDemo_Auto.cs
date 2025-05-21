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
                default:
                    StartCoroutine(TypingNpc(nowTextIndex + 1, true));
                    break;
                case 1:
                    StartCoroutine(TypingNpc(0, false));
                    break;
                case 5 or 8 or 12:
                    StartCoroutine(TypingNpc(3, false));
                    break;
                case 15 or 18 or 21 or 22:
                    TextCG.alpha = 0f;
                    GameManager.Instance.player.MoveStart();
                    BackGround.backGroundMoveStop = false;
                    Dialogues.isDialogue = false;
                    Dialogues.isNpcFirst = false;
                    isSkip = false;// 테스트용 NPC 대화 시작
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

        if (Input.GetKeyDown(KeyCode.Space) && isChoice == true) // 선택
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
                            StartCoroutine(TypingNpc(6, true));
                            break;
                        case 2: // 그동안 들렸던 목소리의 정체 -> 
                            StartCoroutine(TypingNpc(9, true));
                            break;
                    }
                    break;
                case 3:
                    switch (myChoiceIndex)
                    {
                        case 0: // 특이한 일이 뭐야? -> 
                            StartCoroutine(TypingNpc(13, true)); // 수정
                            break;
                        case 1: // 과일이라니? -> 
                            StartCoroutine(TypingNpc(16, true)); // 수정
                            break;
                        case 2: // 마을에 대해 자세히 물어본다 ->
                            StartCoroutine(TypingNpc(19, true)); // 수정
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
        ChoiceArrowCG.alpha = 0f;
        TextCG.alpha = 1f;
        NpcCG.alpha = 0f;
        GameManager.Instance.player.MoveStop();
        BackGround.backGroundMoveStop = true;

        isSkip = false;
        string[] strings = new string[6]{ "신선한 과일들이 가득 쌓여있다.",
                                          "방금까지 누군가 앉아 있던 곳 같다...",
                                          "    시련의 나무 >>\n              << 씨앗 마을\n\n    ...흔한 표지판이다.",
                                          "< 모집 공고 >\n마을을 수..&!@.\n\n포스터가 찢어져 있어 읽기 힘들다...",
                                          "정중앙에 화살이 박힌 과녁..\n시간이 멈춘 듯 낡고 해져있다.",
                                          "두 개의 해바라기가 태양을 향해 고개를 들고 있다.\n나란히 서 있는 모습이 안정감을 주는 듯 하다.."};

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
        string[] NpcStrings = new string[]{ "처음 보는 얼굴인걸.",
                                          "이 마을에는 무슨 일이지?",
                                          "아무 것도 모르겠다라... \n요즘 특이한 일이 많이 생기는군.", 
                                          "이 곳은 씨앗마을이다.\n한때는 사람으로 북적대는 곳이었지만..", 
                                          "지금은 과일... 아니 특이한 일이 생겨서 말이야.", 
                                          "용건없이 온거라면 이만 가봐.", // 5
                                          "찾아온 건 아닌가 보군.. \n이 곳은 씨앗마을이다.", 
                                          "지금은 과일... 아니 마을에 특이한 일이 생겨서",
                                          "외부자가 있기엔 위험하니\n용건이 없다면 이만 가봐.", // 8
                                          "목소리라.. 잘 모르겠는걸.", 
                                          "하지만 해줄 말이 있다.\n지금은 이 마을을 떠나야 해.",
                                          "지금은 과일... 아니 마을에 특이한 일이 생겨서", 
                                          "외부자가 있기엔 위험하다.\n미안하지만 이만 가봐.", // 12
                                          "궁금한 것도 많군.",
                                          "최근에 마을을 지키던 나무에\n큰 이상이 생겨서 말이야.",
                                          "이 이상은 궁금해하지 말고\n지금은 잠시 이 마을을 떠나주겠나.", 
                                          "과일이라.. 내가 말실수를 했군.",
                                          "최근에 마을을 지키던 나무에\n큰 이상이 생겨서 말이야.",
                                          "이 이상은 궁금해하지 말고\n지금은 잠시 이 마을을 떠나주겠나.",
                                          "씨앗 마을이라.. \n예전이었다면 마을을 돌아다니며\n천천히 소개해 줬겠지만...",
                                          "지금은 마을을 지키던 나무에\n큰 이상이 생겨서 말이야.",
                                          "이 이상은 궁금해하지 말고\n지금은 잠시 이 마을을 떠나주겠나.",
                                          "아직도 마을에 있는 건가?\n지금 이 곳은 위험해."
        };

        string[] PlayerStrings = new string[6]{ "아무것도 모르겠다고 답한다.",
                                          "이 곳이 어디인지 묻는다.",
                                          "전까지 들렸던 목소리에 대해 묻는다.",
                                          "'특이한 일'에 대해 물어본다",
                                          "'과일'에 대해 물어본다",
                                          "'씨앗 마을'에 대해 물어본다"
        };

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

