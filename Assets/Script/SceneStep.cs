using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneStep : MonoBehaviour
{
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
    }

    public void loadScene(string sceneName)
    {
        Debug.Log("�ε�");
        asyncOp.allowSceneActivation = true;
    }
}
