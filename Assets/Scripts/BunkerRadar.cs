using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerRadar : MonoBehaviour
{
    private GameObject[] multipleBunker;
    public Transform closestBunker;
    public bool bunkerContact;


    // Start is called before the first frame update
    void Start()
    {
        closestBunker = null;
        bunkerContact = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (closestBunker != null)
        {
            closestBunker.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }


    }
    private void LateUpdate()
    {
        if (Input.GetButton("Yellow"))
        {
            closestBunker = getClosestBunker();
            closestBunker.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        
    }






    public Transform getClosestBunker()
    {
        multipleBunker = GameObject.FindGameObjectsWithTag("Bunker");
        float closestDistance = Mathf.Infinity;
        Transform trans = null;
        foreach(GameObject go in multipleBunker)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if(currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                trans = go.transform;
            }
        }
        return trans;
    }

}
