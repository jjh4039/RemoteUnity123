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
        // �ð� �ӵ� ���� & ���̵� ��
        Time.timeScale = 1;
        StartCoroutine(PadeIn());
        RandomTipset(); // ���� �� ����

        // �̵� ���� ���� 
        switch (loadIndex)
        {
            case 0:
                sceneStep.readyScene("SeedVillage");
                destination = "���� ����";
                destinationEN = "Seed Village";
                break;
        }

        destinationText[0].text = destination;
        destinationText[1].text = destinationEN;
    }

    void Update()
    {
        loadingSlider.value = loadingPer;

        // �ε� ��
        if (loadingPer <= 0.91f + (sceneStep.asyncOp.progress / 10f))
        {
            loadingPer += Time.deltaTime * 0.33f;
        }

        // �ε� �Ϸ�
        else if (isLoading == false)
        {
            
            StartCoroutine(PadeOut());
            isLoading = true; //�ݺ� ���� ����
        }

        // �ε� �ؽ�Ʈ ����
        loadingText.text = "Loading... " + (loadingSlider.value * 100).ToString("0.00") + "%";
    }

    void RandomTipset()
    {
        int randomTip = Random.Range(0, 3);
        switch (randomTip)
        {
            case 0:
                tipText.text = "TIP : " + "���� ���̿��� 2.5���� ��Ÿ���� �����մϴ�.";
                break;
            case 1:
                tipText.text = "TIP : " + "������ �� 4������ �ֽ��ϴ�.";
                break;
            case 2:
                tipText.text = "TIP : " + "���� �غ� �ܰ迡���� �ð��� õõ�� �帨�ϴ�.";
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

        // �� ����
        sceneStep.loadScene("SeedVillage");
    }
}
