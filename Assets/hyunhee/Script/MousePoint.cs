using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MousePoint : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("�߾ӾƾƷֿ����δ�"+hit.transform.gameObject.name);
                //hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1);
            }
        }
    }
}