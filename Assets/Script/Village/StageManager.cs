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

    [Header("Field")]
    public StageName stageID;

    void Start()
    {
        GameManager.Instance.cutScene.StartCoroutine("PadeIn");
        switch (stageID)
        {
            case StageName.SeedVillage:
               
                break;
            case StageName.Stage1:

                break;
            default:
                break;
        }
    }

    void Update()
    {
        
    }
}
