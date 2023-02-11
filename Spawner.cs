/// Spawner
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform[] points;
    public float beat = (60f / 125f) * 2f;
    private float timer;

    Vector3 startPosition = new Vector3(0, 1.6f, 20f);
    Vector3 startPositionLeft = new Vector3(-0.8f, 1.6f, 50f);
    Vector3 startPositionRight = new Vector3(0.8f, 1.6f, 50f);
    Vector3 finalPosition = new Vector3(0, 1.6f, -1);
    Vector3 finalPositionLeft = new Vector3(-0.8f, 1.6f, 20f);
    Vector3 finalPositionRight = new Vector3(0.8f, 1.6f, 20f);
    float actualspeed = (60f / 125f) * 2;
    float speed = 0.1f;
    // was 0.2f

    private List<GameObject> targets = new List<GameObject>();

    public float timeSinceLastSpawn = 0;
    public float timeBetweenSpawns = 0.96f;

    void Start()
    {
        GameObject target = Instantiate(prefab, startPositionLeft, Quaternion.identity);
        targets.Add(target);
        //GameObject target2 = Instantiate(prefab, startPosition, Quaternion.identity);
        //targets.Add(target2);
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= timeBetweenSpawns)
        {
            GameObject target = Instantiate(prefab, startPositionLeft, Quaternion.identity);
            targets.Add(target);
            timeSinceLastSpawn = 0;
        }
        SpawnObject();
    }

    void SpawnObject()
    {
        float elapsedTime = Time.time;
        for (int i = 0; i < targets.Count; i++) // add time condition
        {
            GameObject target = targets[i];
            Vector3 currentPosition;
            if (i % 2 == 0)
            {
                currentPosition = startPositionRight + (finalPositionRight - startPositionRight) * ((elapsedTime-(i* timeBetweenSpawns)) * speed);
                target.transform.position = currentPosition;
                if (currentPosition == finalPositionRight)
                {
                    targets.RemoveAt(i);
                    Destroy(target);
                }
            }
            else if (i % 2 == 1)
            {
                currentPosition = startPositionLeft + (finalPositionLeft - startPositionLeft) * ((elapsedTime- (i * timeBetweenSpawns)) * speed);
                target.transform.position = currentPosition;
                if (currentPosition == finalPositionLeft)
                {
                    targets.RemoveAt(i);
                    Destroy(target);
                }
            }
        }
    }
}

