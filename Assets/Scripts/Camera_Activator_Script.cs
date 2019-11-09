using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Activator_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera relatedCamera;
    public bool trackPlayer = false;
    Camera[] cameras;

    Transform player;

    void Start()
    {
        cameras = FindObjectsOfType<Camera>();
        if(relatedCamera==null)
            relatedCamera = GetComponent<Camera>();
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

            foreach (Camera item in cameras)
            {
                item.gameObject.SetActive(false);
            }

            relatedCamera.gameObject.SetActive(true);



        }
    }

    void track()
    {
        if(trackPlayer)
            relatedCamera.transform.LookAt(player);
    }


}
