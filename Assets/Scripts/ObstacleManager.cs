using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] Obstacles;
    public GameObject[] Lanes;
    public float InitialStep;
    public float Step;
    public float SpawnDistance;
    public float DifficultyIncrease;
    [Range(0, 1)] public float SpawnChance;
    public GameObject Target;

    private float CurrentStep;

    void Start()
    {
        CurrentStep = Target.transform.position.x;
        Step = InitialStep;

        GenerateNextStep();
    }

    void FixedUpdate()
    {
        if (Target.transform.position.x - CurrentStep >= Step)
        {
            CurrentStep = Target.transform.position.x;
            IncreaseDifficulty();
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
        NewObstacle.transform.position = new Vector3(CurrentStep + SpawnDistance, Lanes[lane].transform.position.y);
        //Changing the Layer and Speed (Including the child)
        SetLayerRecursively(NewObstacle, Lanes[lane].layer, index);
        static void SetLayerRecursively(GameObject go, int layerNumber, int index)
        {
            foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
            {
                //Layer
                trans.gameObject.layer = layerNumber;
                if(layerNumber == 8) //Bottom Lane
                {
                    trans.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "BottomLane";
                }
                if(layerNumber == 7) //Middle Lane
                {
                    trans.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "MiddleLane";
                }
                if(layerNumber == 6) //Top Lane
                {
                    trans.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "TopLane";
                }
                //Speed (The rest are still objects, ex. fence)
                if(index==0) //Car
                {
                    trans.gameObject.GetComponent<Rigidbody2D>().velocity=new Vector2(trans.gameObject.GetComponent<Rigidbody2D>().velocity.x-4f, trans.gameObject.GetComponent<Rigidbody2D>().velocity.y);
                }
                if(index==1) //Person1
                {
                    trans.gameObject.GetComponent<Rigidbody2D>().velocity=new Vector2(trans.gameObject.GetComponent<Rigidbody2D>().velocity.x-2f, trans.gameObject.GetComponent<Rigidbody2D>().velocity.y);
                }        
            }
        }
    }

    void IncreaseDifficulty()
    {
        //Currently the value of step follows the function Step(x) = 2 * InitialStep/Difficulty + 4
        float Difficulty = 2 * InitialStep / (Step - 4) + DifficultyIncrease;
        Step = 2 * InitialStep / Difficulty + 4;
        Debug.Log(Difficulty);
    }
}
