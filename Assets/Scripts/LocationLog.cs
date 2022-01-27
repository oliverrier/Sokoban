using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationLog : MonoBehaviour
{
    private List<Vector3> oldLocations = new List<Vector3>();
    private List<Quaternion> oldRotations = new List<Quaternion>();
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SaveBoxLocation()
    {
        oldLocations.Add(transform.position);
        gameManager.PushToLocationLogs(GetComponent<LocationLog>());
    }
    
    private void SavePosition()
    {
        oldLocations.Add(transform.position);
    }
    private void SaveRotation()
    {
        oldRotations.Add(transform.rotation);
    }
    
    public void SavePlayerLocation()
    {
        SavePosition();
        SaveRotation();
    }
    
    public void UndoPosition()
    {
        int lastIndex = oldLocations.Count - 1;

        if (lastIndex < 0) return;
        transform.position = oldLocations[lastIndex];
        oldLocations.RemoveAt(lastIndex);
    }

    private void UndoRotation()
    {
        int lastIndex = oldLocations.Count - 1;

        if (lastIndex >= 0 )
        {
            transform.rotation = oldRotations[lastIndex];
            oldRotations.RemoveAt(lastIndex);
        }
    }
    
    public void UndoPositionAndRotation()
    {
        UndoPosition();
        UndoRotation();
    }
}
