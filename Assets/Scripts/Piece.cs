using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece: MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Material onMat;
    public Puzzle puzzleManager; 
    private Material offMat;
    public bool isOn;
    public Vector3 gridCoords;
    public bool puzzleSolved = false;
    [SerializeField] Piece sisterTile; 

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
            isOn = isOn ? false : true;
            SwitchColors();
            puzzleManager.SwitchStates(gridCoords);
        } 
        if(sisterTile != null)
        {
            sisterTile.isOn = sisterTile.isOn ? false : true; 
            sisterTile.SwitchColors();
            
        }
     
    }
    public void SwitchStates()
    {
        isOn = isOn ? false : true;
        SwitchColors();
    }
}
