using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public GameObject[] Powerups;
    public GameObject[] Lanes;
    public float Step;
    public float SpawnDistance;
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
        float Generate = Random.Range(0.0f, 1.0f);

        if (Generate <= SpawnChance)
        {
            int Lane = Random.Range(0, Lanes.Length);
            GenerateRandomPowerup(Lane);
        }
    }

    void GenerateRandomPowerup(int Lane)
    {
        int ObjectId = Random.Range(0, Powerups.Length);
        SpawnPowerup(ObjectId, Lane);
    }

    void SpawnPowerup(int ObjectId, int Lane)
    {
        GameObject NewObstacle = Instantiate(Powerups[ObjectId], gameObject.transform, true);
        NewObstacle.transform.position = new Vector3(CurrentStep + SpawnDistance, Lanes[Lane].transform.position.y);
        NewObstacle.layer = Lanes[Lane].layer;
    }
}
