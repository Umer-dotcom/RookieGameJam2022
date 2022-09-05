using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidSpawnerScript : MonoBehaviour
{
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
    private List<KidInfantryScript> kidsList = new List<KidInfantryScript>();
    private void OnEnable()
    {
        WayPointScript._onWaypointEnter += SetKidTarget;
    }
    private void OnDisable()
    {
        WayPointScript._onWaypointEnter -= SetKidTarget;
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
                Vector3 pointOnRay = startToTargetRay.GetPoint(wayPointOffsetLength * i);
                Vector3 wayPointPosition = new(pointOnRay.x + wayPointOffsetWidth * (j - 2f) + wayPointOffsetWidth / 2f, pointOnRay.y, pointOnRay.z);

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
            SpawnKid();
            lastKidSpawn = 0;
        }
        


    }
    void SpawnKid()
    {
        OPTag[] kidObjectTags = new[] { OPTag.KID, OPTag.KID2 };
        

        GameObject kidSpawned = poolerScript.SpawnFromPool(kidObjectTags[Random.Range(0, kidObjectTags.Length)], transform.position, Quaternion.identity);
        KidInfantryScript kidScript = kidSpawned.GetComponentInChildren<KidInfantryScript>();
        if (kidScript == null) return;
        
        kidsList.Add(kidScript);
        kidScript.SetTarget(startPoint.position);
        spawnedKidCount++;
        
    }
    public void SetKidTarget(int ID)
    {
        
        KidInfantryScript kidScript = null;
        foreach (KidInfantryScript kid in kidsList)
        {
            if (kid.GetKidInfantryID() == ID) { 
                kidScript = kid;
                break;
            }
        }

        if (kidScript == null) return;

        kidScript.ReachedTarget(ID);
        if (kidScript.GetWPIndex() == 4)
        {
            kidScript.SetTarget(destination.position);
            return;
        }
        else if (kidScript.GetWPIndex() > 4 )
        {
            return;
        }
        int targetOffsetIndex = Random.Range(0, 3);
        Vector3 targetWayPoint = wayPointMatrix[kidScript.GetWPIndex(), targetOffsetIndex];
        kidScript.SetTarget(targetWayPoint);
    }
}
