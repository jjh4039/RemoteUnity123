using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.UI;
using System.Collections;

public class FruitManager : MonoBehaviour
{
    [Header("Component")]
    public ParticleSystem[] FruitParticle;
    public CanvasGroup FruitGuide;
    public Text FruitGuideMain;
    public Text FruitGuideSub;
    public Image FruitGuideImage;
    public Sprite[] FruitGuideImageSprite;

    [Header("Field")]
    public bool isEatApple;
    public bool isEatBanana;
    public int EatIndex;

    public void FruitUseParticle(int FruitIndex)
    {
        FruitParticle[FruitIndex].transform.position = GameManager.Instance.player.transform.position;
        FruitParticle[FruitIndex].Play();
    }

    public IEnumerator StartGuide() // 과일 사용
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Eat, 0);
        switch (EatIndex)
        {
            case 1:
                FruitGuideMain.text = "[ 사과 ]";
                FruitGuideMain.color = new Color(1, 0f, 0f, 1);
                FruitGuideSub.text = "[ Q ] : 발현 시 낮게 점프합니다";
                FruitGuideImage.sprite = FruitGuideImageSprite[0];
                break;
            case 2:
                FruitGuideMain.text = "[ 바나나 ]";
                FruitGuideMain.color = new Color(1, 0.9f, 0f, 1);
                FruitGuideSub.text = "[ W ] : 발현 시 짧게 돌진합니다";
                FruitGuideImage.sprite = FruitGuideImageSprite[1];
                break;
        }

        for (int i = 0; i < 100; i++)
        {
            FruitGuide.alpha += 0.03f;
            yield return new WaitForSeconds(0.01f);
        }
        FruitGuide.alpha = 1;

        yield return new WaitForSeconds(4f);

        for (int i = 0; i < 100; i++)
        {
            FruitGuide.alpha -= 0.03f;
            yield return new WaitForSeconds(0.01f);
        }
        FruitGuide.alpha = 0;
    }
}
