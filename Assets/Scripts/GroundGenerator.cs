using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GameObject GroundTile;
    public float GroundHeight;
    public float Step;
    public GameObject Target;
    public GameObject[] TileInstances;
    
    private int LastTileInstance;
    private float LastPosition;

    void Start()
    {
        LastPosition = Target.transform.position.x;
    }

    void Update()
    {
        if (Target.transform.position.x - LastPosition >= Step)
        {
            LastPosition += Step;
            SwapTiles();
        }
    }

    void SwapTiles()
    {
        TileInstances[LastTileInstance].transform.position += Vector3.right * Step * TileInstances.Length;

        LastTileInstance = (LastTileInstance + 1) % TileInstances.Length;
    }
}