using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShiftingBG : MonoBehaviour
{
    public float backgroundSpeed;
    public Renderer backgroundRenderer;
    void FixedUpdate()
    {
        //Shifts BG
        backgroundRenderer.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0f);
    }
}
