using UnityEngine;
using UnityEngine.InputSystem.Android;

public class FruitManager : MonoBehaviour
{
    public bool isEatApple;
    public bool isEatBanana;
    public ParticleSystem[] FruitParticle;

    void Awake()
    {
        isEatApple = false;
        isEatBanana = false;
    }

    public void FruitUseParticle(int FruitIndex)
    {
        FruitParticle[FruitIndex].transform.position = GameManager.Instance.player.transform.position;
        FruitParticle[FruitIndex].Play();
    }
}
