using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class GameProgress : MonoBehaviour
{
    [SerializeField] PuzzleLine linePuzzle;
    [SerializeField] GameObject lineSphere;

    [SerializeField] GameObject endShader; 

    [SerializeField] GameObject[] spheres;
    [SerializeField] private Puzzle[] puzzles;
    [SerializeField] private Condition[] conditions; 
    public int ammountSloved;
    Material sphereOn;



    [SerializeField] GameObject[] tutorialSpheres;


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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ammountSloved = puzzles.Length + 1; 
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            for (int i = 0; i < 4; i++)
            {
                puzzles[i].solved = true;
            }
          
            foreach (Condition condition in conditions)
            {
                if (!condition.isTrue)
                    ValidateCondition(condition);

            }
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            endShader.SetActive(true);
        }
    }

    public void addSolved(int id)
    {
        ammountSloved++;
        spheres[id- 1].GetComponent<MeshRenderer>().material = sphereOn;

        if(id < 5)
        {
            tutorialSpheres[id-1].GetComponent<MeshRenderer>().material = sphereOn;
        }

        activateEnd();
        foreach (Condition condition in conditions)
        {
            if(!condition.isTrue)
                ValidateCondition(condition); 

        }
    }

    public void addSolved(bool isLine)
    {
        ammountSloved++;
        lineSphere.GetComponent<MeshRenderer>().material = sphereOn;
        activateEnd();


    }

    public bool ValidateCondition(Condition condition)
    {
        foreach(int _int in condition.puzzlesToSolve)
        {
            if (!puzzles[_int].solved)
                return false;

        }
        condition.isTrue = true; 
        condition.conditionEvent.Invoke(); 

        return true;
    }

    void activateEnd()
    {
        if(ammountSloved >= puzzles.Length + 1)
        {
            SoundManager.PlaySound(SoundManager.Sound.DisableBarrier, SoundManager.Mixer.SFX);
            endShader.SetActive(true);
        }
    
    }

}
[System.Serializable]
public class Condition
{
 
    public int[] puzzlesToSolve;
    public bool isTrue;
    public UnityEvent conditionEvent; 
}
