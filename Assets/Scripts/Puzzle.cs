using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private GameObject[] tiles; 

    [SerializeField] private GameObject[,,] pieces; //this is causing so much less problems than expected, it's getting suspicious
    [SerializeField] private int width = 1;
    [SerializeField] private int height = 1;
    [SerializeField] private int depth = 1;

    [SerializeField] private Transform initTile;

    [SerializeField] private Vector3 coord;
    [SerializeField] private float distance;
    [SerializeField] private Vector3 vecDistance; 
    //public  bool[,] fields;
    [SerializeField] private bool solved; 

    private void Start()
    {
        if (distance != 0)
            vecDistance = new Vector3(distance, distance, distance);

      //  fields = new bool[height ,width];
        pieces = new GameObject[width, height, depth];


        foreach(GameObject tile in tiles)
        {
           Vector3 noramlizedCords =  ConvertToGridspace(tile.transform.position, tile.transform.parent);
            tile.GetComponent<Piece>().puzzleManager = this;
            tile.GetComponent<Piece>().gridCoords = noramlizedCords;
            print(Mathf.RoundToInt(noramlizedCords.x).ToString() + Mathf.RoundToInt(noramlizedCords.z).ToString() + Mathf.RoundToInt(noramlizedCords.y) + tile.name);
            pieces[Mathf.RoundToInt(noramlizedCords.x),Mathf.RoundToInt(noramlizedCords.y), Mathf.RoundToInt(noramlizedCords.z)] = tile; 
        }
    
    }

    private Vector3 ConvertToGridspace(Vector3 coordinates, Transform parent) 
    {
        Vector3 newCords = coordinates - initTile.position;
        newCords = new Vector3(newCords.x / parent.localScale.x, newCords.y / parent.localScale.y, newCords.z / parent.localScale.z);
        Vector3 gridSpace = new Vector3(newCords.x / vecDistance.x, newCords.y / vecDistance.y, newCords.z / vecDistance.z);
      
            return gridSpace; 
    }

  public void SwitchStates(Vector3 coords)
    {
        int x = Mathf.RoundToInt(coords.x);
        int y = Mathf.RoundToInt(coords.y);
        int z = Mathf.RoundToInt(coords.z);
        if(x > 0)
        {
            if (pieces[x - 1, y, z] != null)
                pieces[x - 1, y, z].GetComponent<Piece>().SwitchStates();    
        }
        if (y> 0)
        {
            if (pieces[x , y -1, z] != null)
                pieces[x, y - 1, z].GetComponent<Piece>().SwitchStates();
        }
        if (y < height-1)
        {
            if (pieces[x, y +1 , z] != null)
                pieces[x, y + 1, z].GetComponent<Piece>().SwitchStates();
        }
        if (x < width-1)
        {
            if (pieces[x + 1, y, z] != null)
                pieces[x + 1, y ,z].GetComponent<Piece>().SwitchStates();
        }
        if (z > 0)
        {
            if (pieces[x, y, z - 1] != null)
                pieces[x, y, z - 1].GetComponent<Piece>().SwitchStates();
        }
        if (z < depth - 1)
        {
            if (pieces[x, y, z + 1] != null)
                pieces[x, y, z + 1].GetComponent<Piece>().SwitchStates();
        }
        solved = CheckForSolved();
        if (solved)
        {
            Debug.Log(gameObject.name + " is solved");
            SoundManager.PlaySound(SoundManager.Sound.PuzzleSolve, transform.position);
            foreach(GameObject tile in tiles)
            {
                tile.GetComponent<Piece>().enabled = false;
                tile.GetComponent<Piece>().puzzleSolved = true;
            }
            GameProgress._instance.addSolved(); 
        }
    }
    

    private bool CheckForSolved()
    {
        for (int i = 0; i < tiles.Length; i++)
        {

            if (!tiles[i].GetComponent<Piece>().isOn)
                return false; 
        }
       return true; 
        
    }
}
