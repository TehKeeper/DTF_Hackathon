using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Activator_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera relatedCamera;
    public bool trackPlayer = false;
    public bool callTask=false;
    public bool killingTrigger;
    public int killId = 0;
    Camera[] cameras;

    Canvas_Script c_s;


    Transform player;

    void Start()
    {
        cameras = FindObjectsOfType<Camera>();
        if(relatedCamera==null)
            relatedCamera = GetComponent<Camera>();

        c_s = FindObjectOfType<Canvas_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        track();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (trackPlayer)
            {
                player = other.transform;
            }

            other.transform.GetComponent<Character_Controller_Script>().canKill = killingTrigger;
            other.transform.GetComponent<Character_Controller_Script>().killId = killId;

            c_s.activateHpBar(killingTrigger,killId);

            foreach (Camera item in cameras)
            {
                item.gameObject.SetActive(false);
            }

            relatedCamera.gameObject.SetActive(true);



        }

        if (callTask)
        {
            FindObjectOfType<Canvas_Script>().changeTask();
        }
    }

    void track()
    {
        if(trackPlayer)
            relatedCamera.transform.LookAt(player);
    }


}
