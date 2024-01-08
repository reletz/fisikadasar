using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] Obstacles;
    public GameObject[] Lanes;
    public float Step;
    [Range(0, 1)] public float SpawnChance;
    public GameObject Target;

    private float CurrentStep;

    void Start()
    {
        CurrentStep = Target.transform.position.x;

        GenerateNextStep();
    }

    void FixedUpdate()
    {
        if (Target.transform.position.x - CurrentStep >= Step)
        {
            CurrentStep = Target.transform.position.x;

            GenerateNextStep();
        }
    }

    void GenerateNextStep()
    {
        for (int i = 0; i < Lanes.Length; i++)
        {
            if (Random.value <= SpawnChance)
            {
                SpawnRandomObstacle(i);
            }
        }
    }

    void SpawnRandomObstacle(int lane)
    {
        int ObstacleType = Random.Range(0, Obstacles.Length);

        SpawnObstacle(lane, ObstacleType);
    }

    void SpawnObstacle(int lane, int index)
    {
        GameObject NewObstacle = Instantiate(Obstacles[index], gameObject.transform, true);
        NewObstacle.transform.position = new Vector3(CurrentStep + Step, Lanes[lane].transform.position.y + 0.5f * NewObstacle.transform.localScale.y);
        NewObstacle.layer = Lanes[lane].layer;
    }
}
