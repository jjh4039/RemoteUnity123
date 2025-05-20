using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageManager : MonoBehaviour
{
    public enum StageName
    {
        SeedVillage,
        Stage1
    }

    [Header("Components")]
    public CanvasGroup StageTitle;
    public GameObject bar;


    [Header("Field")]
    public StageName stageID;

    void Start()
    {
        GameManager.Instance.cutScene.StartCoroutine("PadeIn");
        StageTitle.alpha = 0f;
        switch (stageID)
        {
            case StageName.SeedVillage:
                StartCoroutine(TitleOn(StageName.SeedVillage));
                break;
            case StageName.Stage1:

                break;
            default:
                break;
        }
    }

    public IEnumerator TitleOn(StageName ST)
    {
        GameManager.Instance.player.isFruitStop = true;
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < 100; i++)
        {
            StageTitle.alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(2f);


        for (int i = 0; i < 100; i++)
        {
            StageTitle.alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        GameManager.Instance.player.isFruitStop = false;
        bar.SetActive(true);
    } 
}
