using UnityEngine;

public class BackGround : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public static bool backGroundMoveStop;
    public float offset;

    void Start()
    {
        backGroundMoveStop = false;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.SetColor("_Color", new Color(0.61f, 0f, 0.75f, 1f));
        meshRenderer.sharedMaterial.SetColor("_Color", Color.red);
    }

    void FixedUpdate()
    {
        {
            if ((Input.GetKey(KeyCode.RightArrow) && backGroundMoveStop == false))
            {
                meshRenderer.material.mainTextureOffset += new Vector2(0.0005f, 0);
            }
            if ((Input.GetKey(KeyCode.LeftArrow) && backGroundMoveStop == false))
            {
                meshRenderer.material.mainTextureOffset -= new Vector2(0.0005f, 0);
            }
        }
    }
}
