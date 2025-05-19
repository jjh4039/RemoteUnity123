using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingSceneManager : MonoBehaviour
{
    [Header("Component")]
    public Text loadingText;
    public Text[] destinationText;
    public Text tipText;
    public Slider loadingSlider;
    public SceneStep sceneStep;
    public CanvasGroup pade;

    [Header("Field")]
    public int loadIndex;
    public string destination;
    public string destinationEN;
    public float loadingPer = 0f;
    public bool isLoading = false;

    void Start()
    {
        // 시간 속도 복구 & 페이드 인
        Time.timeScale = 1;
        StartCoroutine(PadeIn());
        RandomTipset(); // 랜덤 팁 설정

        // 이동 마을 설정 
        switch (loadIndex)
        {
            case 0:
                sceneStep.readyScene("SeedVillage");
                destination = "씨앗 마을";
                destinationEN = "Seed Village";
                break;
        }

        destinationText[0].text = destination;
        destinationText[1].text = destinationEN;
    }

    void Update()
    {
        loadingSlider.value = loadingPer;

        // 로딩 중
        if (loadingPer <= 0.91f + (sceneStep.asyncOp.progress / 10f))
        {
            loadingPer += Time.deltaTime * 0.33f;
        }

        // 로딩 완료
        else if (isLoading == false)
        {
            
            StartCoroutine(PadeOut());
            isLoading = true; //반복 실행 방지
        }

        // 로딩 텍스트 관리
        loadingText.text = "Loading... " + (loadingSlider.value * 100).ToString("0.00") + "%";
    }

    void RandomTipset()
    {
        int randomTip = Random.Range(0, 3);
        switch (randomTip)
        {
            case 0:
                tipText.text = "TIP : " + "발현 사이에는 2.5초의 쿨타임이 존재합니다.";
                break;
            case 1:
                tipText.text = "TIP : " + "과일은 총 4가지가 있습니다.";
                break;
            case 2:
                tipText.text = "TIP : " + "발현 준비 단계에서는 시간이 천천히 흐릅니다.";
                break;
        }
    }

    public IEnumerator PadeIn()
    {
        for (int i = 0; i < 100; i++)
        {
            pade.alpha -= 0.01f;
            yield return new WaitForSeconds(0.006f);
        }
    }

    public IEnumerator PadeOut()
    {
        for (int i = 0; i < 100; i++)
        {
            pade.alpha += 0.01f;
            yield return new WaitForSeconds(0.009f);
        }

        // 씬 변경
        sceneStep.loadScene("SeedVillage");
    }
}
