using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwhook : MonoBehaviour
{
    public GameObject hook; // HOOK PREFAB

    public Transform handPos;

    [SerializeField]float rotateSpeed;

    GameObject curHook;

    public bool ropeThrow = false;

    Rigidbody rb;

    Quaternion firstRot;

    // *********************************************************** //

    void Start()
    {
        firstRot = transform.rotation;

        rb = GetComponent<Rigidbody>();
    }

    public void sendHook(Vector3 target)
    {
        Vector3 destiny = target;

        curHook = (GameObject)Instantiate(hook, handPos.position, Quaternion.identity);

        curHook.GetComponent<ropeScript>().destiny = destiny;
        curHook.GetComponent<ropeScript>().handPos = handPos;
        curHook.GetComponent<ropeScript>().player = this.gameObject;

        ropeThrow = true;
    }


        
    void Update()
    {
        if (!ropeThrow)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, firstRot, rotateSpeed * Time.deltaTime);
        }
    }

    public void dropTheRope()
    {
        Destroy(curHook);

        ropeThrow = false;
    }
}
