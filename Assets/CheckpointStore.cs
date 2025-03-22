using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointStore : MonoBehaviour
{
    public static CheckpointStore instance;

    Checkpoint _checkpoint;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }


    public void AddCheckpoint(Checkpoint checkpoint)
    {
        _checkpoint = new Checkpoint(checkpoint.location + new Vector3(0,0.5f,0));
    }

    public void ClearCheckpoint()
    {
        _checkpoint = null;
    }

    public Vector3 GetActiveCheckpoint()
    {
        if (_checkpoint != null)
        {
            return _checkpoint.location;
        }
        else
        {
            return Vector3.zero;
        }
       
    }
}

public class Checkpoint
{
    public Vector3 location;
    
    public Checkpoint(Vector3 location)
    {
        this.location = location;
    }
}
