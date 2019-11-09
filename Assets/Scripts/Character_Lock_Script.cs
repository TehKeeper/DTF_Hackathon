using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Lock_Script : MonoBehaviour
{
    // Start is called before the first frame update

    bool lockActive = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if(lockActive)
            StartCoroutine(lockPlayer(collision.transform));
        }
    }

    public void _unlock()
    {
        transform.GetComponentInChildren<Character_Controller_Script>().lockToTransform(transform, false);
        lockActive = false;
    }

    public void _lock()
    {
        lockActive = true;
    }


    IEnumerator lockPlayer(Transform player)
    {
        Rigidbody lrb = player.GetComponent<Rigidbody>();

        bool todo = true;
        float timer = 1;

        while (todo||timer>0)
        {
            timer -= 0.02f;
            
            if (lrb.velocity.magnitude <= 0)
            {
                player.GetComponent<Character_Controller_Script>().lockToTransform(transform, true);
                todo = false;
            }

            yield return null;
        }
    }

}
