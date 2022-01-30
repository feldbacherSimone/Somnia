using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece: MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Material onMat;
    public Puzzle puzzleManager; 
    private Material offMat;
   [SerializeField] bool isOn;
    public Vector3 gridCoords;


    private void Awake()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
 
        onMat = (Material)Resources.Load("OnMat");
        offMat = (Material)Resources.Load("OffMat");
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
        isOn = isOn ? false : true;
        SwitchColors();
        puzzleManager.SwitchStates(gridCoords);
    }
    public void SwitchStates()
    {
        isOn = isOn ? false : true;
        SwitchColors();
    }
}
