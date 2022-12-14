
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidSpawnerScript : MonoBehaviour
{
    private struct WaypointInfo
    {
        public Vector3 position;
        public bool occupied;

    }
    [SerializeField] private GameManager manager;
    //[SerializeField] float distanceFromPlayer = 15f;
    public static int spawnerCount = 0;
    public int spawnerID = 0;
    public int maxKidCount;
    public float kidSpawnInterval;
    public Transform startPoint;
    public Transform destination;
    public GameObject debugSphere;
    public Transform objectContainer;

    private ObjectPoolerScript poolerScript;
    private WaypointInfo[,] wayPointMatrix = new WaypointInfo[4, 4];
    private Ray startToTargetRay;
    private float lastKidSpawn = 0;
    private float spawnedKidCount = 0;
    private List<KidMovementScript> kidsList = new List<KidMovementScript>();

    

    private void OnEnable()
    {
        WayPointScript._onWaypointEnter += SetKidTarget;
        KidMovementScript.TargetNotFound += SetKidTarget;
    }
    private void OnDisable()
    {
        WayPointScript._onWaypointEnter -= SetKidTarget;
        KidMovementScript.TargetNotFound -= SetKidTarget;
    }
    void Start()
    {
        
        spawnerID = spawnerCount++;
        GameObject sphere = Instantiate(debugSphere, startPoint.position, Quaternion.identity);
        sphere.transform.parent = objectContainer;
        sphere.GetComponent<WayPointScript>().wayPointSpawner = this;
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

                GameObject debug = Instantiate(debugSphere, wayPointPosition, Quaternion.identity);

                debug.GetComponent<WayPointScript>().wayPointSpawner = this;
                wayPointMatrix[i, j].position = wayPointPosition;
                wayPointMatrix[i, j].occupied = false;
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
        

        GameObject kidSpawned = poolerScript.SpawnFromPool(OPTag.KID, transform.position, Quaternion.identity);
        KidMovementScript kidScript = kidSpawned.GetComponentInChildren<KidMovementScript>();

        if (kidScript == null) 
        {
            Debug.Log("KidScript not found.");
            
            return; 
        }
        kidScript.spawner = this;
        
        
        kidsList.Add(kidScript);
        kidScript.SetTarget(startPoint.position);
        kidScript.SetFinalTarget(destination.position);
        spawnedKidCount++;

        manager.AddToList(kidSpawned.transform.GetChild(0).gameObject);
    }
    public void SetKidTarget(int ID)
    {
        
        KidMovementScript kidScript = null;
        foreach (KidMovementScript kid in kidsList)
        {
            if (kid.GetID() == ID) { 
                kidScript = kid;
                break;
            }
        }

        if (kidScript == null) return;


        
        kidScript.ReachedTarget(ID);
        if (kidScript.WPIndex() == 4)
        {
            kidScript.SetTarget(destination.position);
            return;
        }
        else if (kidScript.WPIndex() > 4)
        {
            return;
        }

        int targetOffsetIndex = 0;
        List<int> availableSpots = new();
        for(int i = 0; i <= 3; ++i)
        {
            if (!wayPointMatrix[kidScript.WPIndex(), i].occupied)
            {
                availableSpots.Add(i);
            }
        }
        if(availableSpots.Count == 0)
        {
            for(int i = 0; i <= 3; ++i)
            {
                wayPointMatrix[kidScript.WPIndex(), i].occupied = false;
            }
            targetOffsetIndex = Random.Range(0, 3);
        } else
        {
            targetOffsetIndex = availableSpots[Random.Range(0, availableSpots.Count)];
        }
        
        Vector3 targetWayPoint = wayPointMatrix[kidScript.WPIndex(), targetOffsetIndex].position;
        wayPointMatrix[kidScript.WPIndex(), targetOffsetIndex].occupied = true;
        kidScript.SetTarget(targetWayPoint);
        
    }
}
