using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private GameObject[] tiles; 

   [SerializeField] private GameObject[,] pieces;
   [SerializeField] private int width;
   [SerializeField] private int height;


    [SerializeField] private Transform initTile;

    [SerializeField] private Vector3 coord;
    [SerializeField] private int distance; 
    //public  bool[,] fields;

    private void Awake()
    {
      //  fields = new bool[height ,width];
        pieces = new GameObject[height, width];


        foreach(GameObject tile in tiles)
        {
           Vector3 noramlizedCords =  ConvertToGridspace(tile.transform.position);
            tile.GetComponent<Piece>().puzzleManager = this;
            tile.GetComponent<Piece>().gridCoords = noramlizedCords;
            print(Mathf.RoundToInt(noramlizedCords.x).ToString() + Mathf.RoundToInt(noramlizedCords.y).ToString() + tile.name);
            pieces[Mathf.RoundToInt(noramlizedCords.x), Mathf.RoundToInt(noramlizedCords.y)] = tile; 
        }
    }

    private Vector3 ConvertToGridspace(Vector3 coordinates) 
    {
        Vector3 newCords = coordinates - initTile.position;
        Vector3 gridSpace = newCords / distance;

            return gridSpace; 
    }

  public void SwitchStates(Vector3 coords)
    {
        int x = Mathf.RoundToInt(coords.x);
        int y = Mathf.RoundToInt(coords.y);

        if(x > 0)
        {
            if (pieces[x - 1, y] != null)
            {
                print("left Valid");
                pieces[x - 1, y].GetComponent<Piece>().SwitchStates();
            }
               
        }
        if (y> 0)
        {
            if (pieces[x , y -1] != null)
            {
                pieces[x, y - 1].GetComponent<Piece>().SwitchStates();
            }
        }
        if (y < height-1)
        {
            if (pieces[x, y +1] != null)
            {
                pieces[x, y + 1].GetComponent<Piece>().SwitchStates();
            }
        }
        if (x < width-1)
        {
            if (pieces[x + 1, y] != null)
            {
                pieces[x + 1, y].GetComponent<Piece>().SwitchStates();
            }
        }
    }
    
}
