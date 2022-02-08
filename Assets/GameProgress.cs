using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    [SerializeField] GameObject[] spheres;
    [SerializeField] int ammountSloved;
    Material sphereOn;


    public static GameProgress _instance; 
    private void Start()
    {
        if (_instance != null)
            Destroy(this);
        else
            _instance = this; 

        sphereOn = (Material)Resources.Load("Materials/SphereOn");
    }

    public void addSolved()
    {
        ammountSloved++;
        spheres[ammountSloved - 1].GetComponent<MeshRenderer>().material = sphereOn; 
    }
}
