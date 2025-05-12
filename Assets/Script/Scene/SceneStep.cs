using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneStep : MonoBehaviour
{
    [Header("Component")]
    public AsyncOperation asyncOp;

    void Update()
    {
        // Debug.Log(asyncOp.progress);
    }

    public void readyScene(string sceneName)
    {
        Debug.Log("�غ�");
        asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false;
        GameManager.Instance.cutScene.isSkipCutScene = true;
    }

    public void loadScene(string sceneName)
    {
        Debug.Log("�ε�");
        asyncOp.allowSceneActivation = true;
        GameManager.Instance.cutScene.isSkipCutScene = false;
    }
}
