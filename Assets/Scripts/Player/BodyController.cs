using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BodyController : MonoBehaviour
{
    public static BodyController instance;

    [SerializeField]
    private Transform spine;
    [SerializeField]
    private Transform neck;
    [SerializeField]
    private float turnTime = 50f;

    private Rigidbody[] rigidbodies;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = true;
            }
            //Rigidbody thisObjRB = GetComponent<Rigidbody>();
            //thisObjRB.isKinematic = false;
        }
        else
        {
            Destroy(this);
        }
    }
 
     //values for internal use
     private Quaternion _lookRotation;
     private Vector3 _direction;

    public void TurnSpine(Vector3 targetRot)
    {
        //find the vector pointing from our position to the target
        _direction = (targetRot - spine.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        spine.rotation = Quaternion.Slerp(spine.rotation, _lookRotation, Time.deltaTime * turnTime);
    }

    public void TurnNeck(Vector3 targetRot)
    {
        //find the vector pointing from our position to the target
        _direction = (targetRot - neck.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        neck.rotation = Quaternion.Slerp(neck.rotation, _lookRotation, Time.deltaTime * turnTime);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Kids Win!!!");
    //    if (collision.gameObject.CompareTag("Kid"))
    //    {
            
    //        foreach (Rigidbody rb in rigidbodies)
    //        {
    //            rb.isKinematic = false;
    //        }
    //        Rigidbody thisObjRB = GetComponent<Rigidbody>();
    //        thisObjRB.isKinematic = true;
    //    }
    //}
}
