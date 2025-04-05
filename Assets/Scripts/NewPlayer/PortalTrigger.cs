using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{

    ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem[] allParticles = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem ps in allParticles)
        {
            if (ps.gameObject.name == "WinEffect")  
            {
                particleSystem = ps;
                break;
            }
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (particleSystem != null)
            {
                particleSystem.Play();
                
            }

           
        }
    }
}
