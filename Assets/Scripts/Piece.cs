using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece: MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Material onMat;
    public Puzzle puzzleManager;
    public PuzzleLine puzzleManagerLine;
    private Material offMat;
    public bool isOn;
    public Vector3 gridCoords;
    public bool puzzleSolved = false;
    [SerializeField] Piece sisterTile;
    [SerializeField] private bool isLine; 

    private void Awake()
    {

        meshRenderer = gameObject.GetComponent<MeshRenderer>();
 
        onMat = (Material)Resources.Load("Materials/OnMat");
        offMat = (Material)Resources.Load("Materials/OffMat");
        SwitchColors();

      
    }
    public void SwitchColors()
    {

        if (isOn)
        {
            meshRenderer.material = onMat; 
        }
        else
        {
            meshRenderer.material = offMat;  
        }
    }

    public void Onhit()
    {
        if (!puzzleSolved) {
            SoundManager.PlaySound(SoundManager.Sound.PuzzleInteract, transform.position, "SFX");

            isOn = isOn ? false : true;
            SwitchColors();
            if (!isLine)
                puzzleManager.SwitchStates(gridCoords);
            else if (isLine)
                puzzleManagerLine.SwitchStates(gridCoords);
            if (sisterTile != null)
            {
                sisterTile.isOn = sisterTile.isOn ? false : true;
                sisterTile.SwitchColors();
                sisterTile.puzzleManager.SwitchStates(true);
            }
        }    
    }

    public void SwitchStates()
    {
        isOn = isOn ? false : true;
        SwitchColors();
    }
}
