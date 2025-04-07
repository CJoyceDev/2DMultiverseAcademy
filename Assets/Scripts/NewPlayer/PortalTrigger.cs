using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PortalTrigger : MonoBehaviour
{

    ParticleSystem particleSystem;
    [SerializeField] AudioClip PortalSound;
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
                SoundHandler.instance.PlaySound(PortalSound, transform, 0.1f, 1.5f);
            }

           
        }
    }
}
