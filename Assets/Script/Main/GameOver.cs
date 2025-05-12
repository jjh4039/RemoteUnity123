using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour
{
    [Header("Component")]
    public GameObject gameOverUi;
    public Text lastSavePointText;
    public string lastSavePoint;

    void Update()
    {
        lastSavePointText.text = "마지막 세이브 : " + lastSavePoint;
    }
}
