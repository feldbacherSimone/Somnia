using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform tragetPoint;

    private void Start()
    {
        if (startPoint == null)
            startPoint = gameObject.transform; 
    }

    public void Activate()
    {
        Debug.Log("TEST LMAO"); 
    }

   
}
