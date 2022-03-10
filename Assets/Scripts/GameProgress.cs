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
        int i = 0; 
        foreach(Puzzle puzzle in puzzles)
        {
            puzzle.puzzleID = i + 1;
            i++;
        }
    }

    public void addSolved(int id)
    {
        ammountSloved++;
        spheres[id- 1].GetComponent<MeshRenderer>().material = sphereOn; 
    }
}
