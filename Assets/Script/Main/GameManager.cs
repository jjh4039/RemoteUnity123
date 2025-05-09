using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    public FruitManager fruitManager;
    public Player player;
    public IntroduceTextManager introduceTextManager;
    public Bar bar;
    public MainCamera mainCamera;
    public CutScene cutScene;
    public GameOver gameOver;
    public SceneStep sceneStep;

    void Awake()
    {
        Instance = this;
        AudioManager.instance.PlayBgm(true);
    }
}
