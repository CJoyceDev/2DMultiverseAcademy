using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZoneTrigger : MonoBehaviour
{
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;

    private bool triggered = false;

    void Start()
    {
        //wall1 = GameObject.Find("Wall1");
        //wall2 = GameObject.Find("Wall2");
        //wall3 = GameObject.Find("Wall3");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            wall1.SetActive(true);
            wall2.SetActive(true);
            wall3.SetActive(true);

            Debug.Log("Wall active");
        }
    }
}
