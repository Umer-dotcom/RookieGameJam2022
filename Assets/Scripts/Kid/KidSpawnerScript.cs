using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidSpawnerScript : MonoBehaviour
{
    public class Kid
    {
        GameObject kidPrefab;
        int iceCreamNeeded;
        string tag;
    }

    //public List<Kid> kidPrefabList;
    //public GameObject kidPrefab;
    public int maxKidCount;
    public float kidSpawnInterval;
    public Transform startPoint;
    public Transform destination;
    public GameObject debugSphere;
    

    private ObjectPoolerScript poolerScript;
    private Vector3[,] wayPointMatrix = new Vector3[4, 4];
    private Ray startToTargetRay;
    private float lastKidSpawn = 0;
    private float spawnedKidCount = 0;
    private List<KidScript> kidsList = new List<KidScript>();
    private void Awake()
    {
        
    }
    void Start()
    {
        Instantiate(debugSphere, startPoint.position, Quaternion.identity);
        poolerScript = ObjectPoolerScript.Instance;
        Vector3 directionVector = destination.position - startPoint.position;
        float distanceBWVectors = Vector3.Distance(destination.position, startPoint.position);
        float wayPointOffsetLength = distanceBWVectors / 6f;
        float wayPointOffsetWidth = wayPointOffsetLength;
        startToTargetRay = new Ray(new (startPoint.position.x, startPoint.position.y, startPoint.position.z - 3.5f), directionVector);
        for (int i  = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                //if (j - 2 == 0) continue;
                Vector3 pointOnRay = startToTargetRay.GetPoint(wayPointOffsetLength * i);
                Vector3 wayPointPosition = new(pointOnRay.x + wayPointOffsetWidth * (j - 2f) + wayPointOffsetWidth / 2f, pointOnRay.y, pointOnRay.z);
                //Debug.Log( i + ", " + j +  " = "  +  pointOnRay.x );
                Instantiate(debugSphere, wayPointPosition, Quaternion.identity);
                wayPointMatrix[i, j] = wayPointPosition;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(kidToTargetRay.origin, kidToTargetRay.direction * 20f, Color.blue);
        lastKidSpawn += Time.deltaTime;

        if (spawnedKidCount < maxKidCount && lastKidSpawn >= kidSpawnInterval)
        {
            Debug.Log("Kid Created");
            SpawnKid();
            lastKidSpawn = 0;
        }
        foreach (KidScript kid in kidsList)
        {
            if (kid.IsTargetReached())
            {
                
                SetKidTarget(kid);
            }
        }


    }
    void SpawnKid()
    {
        OPTag[] kidObjectTags = new[] { OPTag.KID, OPTag.KID2 };
        ;

        GameObject kidSpawned = poolerScript.SpawnFromPool(kidObjectTags[Random.Range(0, kidObjectTags.Length)], transform.position, Quaternion.identity);
        KidScript kidScript = kidSpawned.GetComponentInChildren<KidScript>();
        if (kidScript == null)
        {
            Debug.Log("Kid Script is null");
        }
        
        kidsList.Add(kidScript);
        SetKidTarget(kidScript);
        spawnedKidCount++;
        
    }
    void SetKidTarget(KidScript kidScript)
    {
        if (kidScript.GetWPIndex() == 4)
        {
            //Debug.Log("Reached final target.");
            kidScript.SetTarget(destination.position);
            return;
        }
        else if (kidScript.GetWPIndex() > 4 )
        {
            //Debug.Log("Given wayPointIndex exceeds index size.");
            return;
        } else if (kidScript.GetWPIndex() == 0)
        {
            Debug.Log("Start Point assigned.");
            kidScript.SetTarget(startPoint.position);
        }
        int targetOffsetIndex = Random.Range(0, 3);
        Vector3 targetWayPoint = wayPointMatrix[kidScript.GetWPIndex(), targetOffsetIndex];
        kidScript.SetTarget(targetWayPoint);
    }
    public void ModifyTarget()
    {

    }
}
