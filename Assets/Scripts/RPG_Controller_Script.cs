using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RPG_Controller_Script : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent navAgent;
    Vector3 movePoint;

        
    void Start()
    {
        navAgent=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            navAgent.SetDestination(setMovePoint());
        }
    }

    Vector3 setMovePoint()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 approxPosition = Vector3.zero;

        if (Physics.Raycast(ray, out hit))
        {
            approxPosition = hit.point;
        }

        if (approxPosition == Vector3.zero)
            return transform.position;
        else
            return approxPosition;

    }
}
