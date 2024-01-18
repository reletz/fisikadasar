using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShiftingBG : MonoBehaviour
{
    public static ShiftingBG Instance;
    public float backgroundSpeed;
    public float offset=0f;
    public Renderer backgroundRenderer;

    void Awake()
    {
        Instance=this;
    }
    void FixedUpdate()
    {
        //Shifts BG
        offset = backgroundSpeed * Time.deltaTime;
        backgroundRenderer.material.mainTextureOffset += new Vector2(offset, 0f);
    }
}
