using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class GameProgress : MonoBehaviour
{
    [SerializeField] GameObject[] spheres;
    [SerializeField] private Puzzle[] puzzles;
    [SerializeField] private Condition[] conditions; 
    public int ammountSloved;
    Material sphereOn;




    public static GameProgress _instance; 
    private void Start()

    {
        SoundManager.LoadMixer();

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
        foreach(Condition condition in conditions)
        {
            ValidateCondition(condition); 
        }
    }

    public bool ValidateCondition(Condition condition)
    {
        foreach(int _int in condition.puzzlesToSolve)
        {
            if (!puzzles[_int].solved)
                return false; 
        }
        condition.conditionEvent.Invoke();
        return true;
    }



}
[System.Serializable]
public class Condition
{
 
    public int[] puzzlesToSolve;
    public bool isTrue;
    public UnityEvent conditionEvent; 
}
