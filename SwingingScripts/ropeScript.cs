using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeScript : MonoBehaviour
{
    public Vector3 destiny;
    public Transform handPos;

    public float speed = 1;

    public float distance = 10;

    public GameObject nodePrefab;

    public GameObject player;

    public float forcePower;

    public GameObject lastNode;

    bool done=false;

    int vertexCount=2;
    public List<GameObject> Nodes = new List<GameObject>();

    public LineRenderer lr;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        lastNode = transform.gameObject;

        Nodes.Add(transform.gameObject);

        lr = GetComponent<LineRenderer>();

        destiny += new Vector3(UnityEngine.Random.Range(-.5f, .5f) ,0 ,0);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position , destiny , speed);
        if (transform.position != destiny)
        {
            if (Vector3.Distance(player.transform.position,lastNode.transform.position) > distance)
            {
                CreateNode();
            }
        }
        else if(!done)
        {
            done = true;
            lastNode.GetComponent<Rigidbody>().isKinematic = false;
            lastNode.GetComponent<HingeJoint>().connectedBody = player.GetComponent<Rigidbody>();
            //player.GetComponent<Rigidbody>().AddForce((Vector3.forward) * forcePower / 2); //player.transform.TransformDirection(Vector3.forward) 
        }
        if (done)
        {
            player.GetComponent<Rigidbody>().velocity *= 1.01f;
            
        }

       
    }

    private void LateUpdate()
    {
        RenderLine();
    }
    private void CreateNode()
    {
       
        Vector3 pos2Create = player.transform.position - lastNode.transform.position;
        pos2Create.Normalize();
        pos2Create *= distance;
        pos2Create += (Vector3)lastNode.transform.position;

        GameObject go =(GameObject)Instantiate(nodePrefab, pos2Create, Quaternion.identity);

        go.transform.SetParent(transform);

        lastNode.GetComponent<HingeJoint>().connectedBody = go.GetComponent<Rigidbody>();

        lastNode = go;

        Nodes.Add(lastNode); vertexCount++;
    }

    void RenderLine()
    {
        lr.SetVertexCount(vertexCount);
        int i;
        for (i = 0; i < Nodes.Count; i++)
        {
            lr.SetPosition(i, Nodes[i].transform.position);
        }

        lr.SetPosition(i, handPos.position);
    }
}
