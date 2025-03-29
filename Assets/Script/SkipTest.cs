using UnityEngine;

public class SkipTest : MonoBehaviour
{
    public GameObject bar;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) { 
            GameManager.Instance.player.isMove = true;
            GameManager.Instance.player.isFruitStop = false;
            GameManager.Instance.player.transform.position = new Vector3(35f, 0f, 0f);
            GameManager.Instance.fruitManager.isEatApple = true;
            GameManager.Instance.introduceTextManager.isQuestClear = true;
            bar.SetActive(true);
        }
    }
}
