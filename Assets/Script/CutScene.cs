using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutScene : MonoBehaviour
{
    public Image[] CutSceneBox;
    public CanvasGroup pade;
    public GameObject skipText;

    // 컷신 레터박스 & 자막
    public IEnumerator CutSceneStart()
    {
        for (int i = 0; i < 100; i++) 
        {
            CutSceneBox[0].rectTransform.localPosition = new Vector3(0, CutSceneBox[0].rectTransform.localPosition.y - 1, 0);
            CutSceneBox[1].rectTransform.localPosition = new Vector3(0, CutSceneBox[1].rectTransform.localPosition.y + 1, 0);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.3f);
        skipText.SetActive(true);
    }

    // 페이드 아웃
    public IEnumerator PadeOut()
    {
        for(int i = 0; i < 100; i++)
        {
            pade.alpha += 0.01f;
            yield return new WaitForSeconds(0.006f);
        }
    }
}
