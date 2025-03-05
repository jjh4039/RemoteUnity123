using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class IntroduceTextManager : MonoBehaviour
{
    public Text introduceText;
    public CanvasGroup canvasGroup;
    public string[] texts;

    private void Awake()
    {   
        StartCoroutine(FristStep());

        texts[0] = "안녕하세요!";
        texts[1] = "우선 저 앞으로\n달려볼까요?";
        texts[2] = "방향키를 눌러\n좌우로 이동하기";
    }

    IEnumerator FristStep()
    {
        StartCoroutine(Say(0));
        yield return new WaitForSeconds(2f);
        StartCoroutine(Say(1));
        yield return new WaitForSeconds(3f);
        StartCoroutine(Say(2));
    }

    IEnumerator Say(int TextIndex) // 대화용
    {
        StartCoroutine(AlphaOn());
        introduceText.text = "";
        introduceText.color = Color.black;

        switch (TextIndex)
        {
            case 2:
                introduceText.color = new Color(0.2f, 0.55f, 1f);
                break;
        }
        
        for (int i = 0; i < texts[TextIndex].Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            introduceText.text += texts[TextIndex][i]; 
        }
    }

    IEnumerator AlphaOn()
    {
        canvasGroup.alpha = 0f;
        for (float i = 0; i < 1; i += 0.1f)
        {
            canvasGroup.alpha = i;
            yield return new WaitForSeconds(0.05f);
        }
    }
}


