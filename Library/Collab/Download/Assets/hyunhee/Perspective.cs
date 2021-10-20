using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : Sense
{
    private Transform playerTrans;
    private RaycastHit hit;
    public int ViewDistance = 100;
    public GameObject button;
    public GameObject Check;

    protected override void Initialise()
    {
        playerTrans = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    protected override void UpdateSense()
    {
        Vector3 dir = playerTrans.forward;
        dir.y = 0;

        if (Physics.Raycast(playerTrans.position, dir, out hit))
        {
            Debug.Log("hit point:" + hit.point + ",distance:" + hit.distance + ",name:" + hit.collider.name);
            Debug.DrawRay(playerTrans.position, dir * hit.distance, Color.red);
        }

        if(hit.distance <= 15)
        {
            button.gameObject.SetActive(true);
        }
        else
        {
            button.gameObject.SetActive(false);
            Check.gameObject.SetActive(false);
        }
    }

    void OnDrawGizmos()
    {
        if (playerTrans == null)
            return;
        Vector3 frontRayPoint = playerTrans.position + (playerTrans.forward * ViewDistance);

        Debug.DrawLine(playerTrans.position, frontRayPoint, Color.green);
    }
}
