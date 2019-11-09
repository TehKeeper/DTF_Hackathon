using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Activator_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera relatedCamera;
    Camera[] cameras;
    void Start()
    {
        cameras = FindObjectsOfType<Camera>();
        if(relatedCamera==null)
            relatedCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (Camera item in cameras)
            {
                item.gameObject.SetActive(false);
            }

            relatedCamera.gameObject.SetActive(true);



        }
    }
}
