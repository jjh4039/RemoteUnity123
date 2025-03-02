using UnityEngine;
using UnityEngine.UI;

public class FruitUi : MonoBehaviour
{
    public Text text;

    void Update()
    {
        text.text = (GameManager.Instance.player.readyFruits[0].ToString() + " - " + GameManager.Instance.player.readyFruits[1].ToString() + " - " + GameManager.Instance.player.readyFruits[2].ToString());
    }
}
