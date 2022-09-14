using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    public int maxKidCount;
    public float kidSpawnInterval;
    public Transform startPoint;
    public Transform destination;
    public GameObject debugSphere;
    public Transform objectContainer;

    private ObjectPoolerScript poolerScript;
    private Vector3[,] wayPointMatrix = new Vector3[4, 4];
    private Ray startToTargetRay;
    private float lastKidSpawn = 0;
    private float spawnedKidCount = 0;
    private List<KidMovementScript> kidsList = new List<KidMovementScript>();
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
        GameObject sphere = Instantiate(debugSphere, startPoint.position, Quaternion.identity);
        sphere.transform.parent = objectContainer;
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
        

        GameObject kidSpawned = poolerScript.SpawnFromPool(OPTag.KID, transform.position, Quaternion.identity);
        KidMovementScript kidScript = kidSpawned.GetComponentInChildren<KidMovementScript>();

        if (kidScript == null) 
        {
            Debug.Log("KidScript not found.");
            
            return; 
        }
        
        kidsList.Add(kidScript);
        kidScript.SetTarget(startPoint.position);
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
        else if (kidScript.WPIndex() > 4 )
        {
            return;
        }
        int targetOffsetIndex = Random.Range(0, 3);
        Vector3 targetWayPoint = wayPointMatrix[kidScript.WPIndex(), targetOffsetIndex];
        kidScript.SetTarget(targetWayPoint);
    }
}
