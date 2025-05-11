using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutScene : MonoBehaviour
{
    public GameObject letterBox;
    public Image[] CutSceneBox;
    public CanvasGroup pade;
    public GameObject skipText;

    [Header("Trigger")]
    // 다음 신 로딩 완료여부
    public bool isSkipCutScene = false;
    // 컷신 레터박스 완료여부
    public bool isCutSceneBoxEnd = false;

    // 컷신 레터박스
    public IEnumerator CutSceneStart()
    {
        letterBox.SetActive(true);

        for (int i = 0; i < 100; i++) 
        {
            CutSceneBox[0].rectTransform.localPosition = new Vector3(0, CutSceneBox[0].rectTransform.localPosition.y - 1, 0);
            CutSceneBox[1].rectTransform.localPosition = new Vector3(0, CutSceneBox[1].rectTransform.localPosition.y + 1, 0);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.3f);
        isCutSceneBoxEnd = true;
    }

    public void Awake()
    {
        pade.alpha = 1f;
    }

    public void Update()
    {
        // 스킵 점검
        if (isSkipCutScene == true && isCutSceneBoxEnd == true)
        {
            skipText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(PadeOut());
                isSkipCutScene = false;
            }
        }
    }

    public IEnumerator PadeIn()
    {
        for (int i = 0; i < 100; i++)
        {
            pade.alpha -= 0.01f;
            yield return new WaitForSeconds(0.005f);
        }
    }

    // 페이드 아웃
    public IEnumerator PadeOut()
    {
        for(int i = 0; i < 100; i++)
        {
            pade.alpha += 0.01f;
            yield return new WaitForSeconds(0.005f);
        }

        GameManager.Instance.sceneStep.loadScene("Loading");
    }
}
