using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    [SerializeField] GameObject[] spheres;
    [SerializeField] private Puzzle[] puzzles; 
    public int ammountSloved;
    Material sphereOn;




    public static GameProgress _instance; 
    private void Start()
    {
        if (_instance != null)
            Destroy(this);
        else
            _instance = this; 

        sphereOn = (Material)Resources.Load("Materials/SphereOn");

        for (int i = 0; i < puzzles.Length; i++)
        {
            puzzles[i].puzzleID = i + 1;
            i++; 
        }
    }

    public void addSolved(int id)
    {
        ammountSloved++;
        spheres[id- 1].GetComponent<MeshRenderer>().material = sphereOn; 
    }
}
