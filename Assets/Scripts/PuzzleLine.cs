using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLine : MonoBehaviour
{
    [SerializeField] private GameObject[] tiles;
    [SerializeField] private int puzzleID;
    [SerializeField] private GameObject[,,] pieces; 
    [SerializeField] private int width = 1;
    [SerializeField] private int height = 1;
    [SerializeField] private int depth = 1;

    [SerializeField] private bool solved;

    private void Start()
    {
        width = tiles.Length;
        pieces = new GameObject[width, height, depth];
        foreach (GameObject piece in tiles)
        {
            int i = System.Array.IndexOf(tiles, piece);
            pieces[i, 0, 0] = piece;
            piece.GetComponent<Piece>().puzzleManagerLine= this;
            piece.GetComponent<Piece>().gridCoords = new Vector3(i, 0, 0);
        }
    }


    public void SwitchStates(Vector3 coords)
    {
        int x = Mathf.RoundToInt(coords.x);
        int y = Mathf.RoundToInt(coords.y);
        int z = Mathf.RoundToInt(coords.z);
        if (x > 0)
        {
            if (pieces[x - 1, y, z] != null)
                pieces[x - 1, y, z].GetComponent<Piece>().SwitchStates();
        }
        if (y > 0)
        {
            if (pieces[x, y - 1, z] != null)
                pieces[x, y - 1, z].GetComponent<Piece>().SwitchStates();
        }
        if (y < height - 1)
        {
            if (pieces[x, y + 1, z] != null)
                pieces[x, y + 1, z].GetComponent<Piece>().SwitchStates();
        }
        if (x < width - 1)
        {
            if (pieces[x + 1, y, z] != null)
                pieces[x + 1, y, z].GetComponent<Piece>().SwitchStates();
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
            foreach (GameObject tile in tiles)
            {
                tile.GetComponent<Piece>().enabled = false;
                tile.GetComponent<Piece>().puzzleSolved = true;
            }
            GameProgress._instance.addSolved(puzzleID);
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


