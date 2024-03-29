using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;
    public GameObject Player;
    public float FixedHeight;
    public float XOffset;

    void Awake()
    {
        Instance=this;
    }
    void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(Player.transform.position.x + XOffset, FixedHeight, gameObject.transform.position.z);
    }
}
