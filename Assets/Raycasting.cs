using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
   
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject sphere;
    [SerializeField] private Material otherMat;




    private RaycastHit _hit;
    private Vector3 _worldPos;


   
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectSquare();
        }
        RaycastTest();
        
    }

    void RaycastTest()
    {
        Vector3 pos = Input.mousePosition;
        Vector3 newPos = new Vector3(pos.x, pos.y, camera.transform.position.z +10 );
        _worldPos = camera.ScreenToWorldPoint(newPos);
        //print(_worldPos);
        sphere.transform.position = _worldPos; 

        if (Physics.Raycast(transform.position, _worldPos, out _hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, _worldPos * _hit.distance, Color.yellow);
           
        }
        else
        {
            Debug.DrawRay(transform.position, _worldPos * 1000, Color.white);
            
            
        }     
    }

    private void SelectSquare()
    {
        Debug.Log("Click!");
        if (Physics.Raycast(transform.position, _worldPos, out _hit, Mathf.Infinity))
        {
            _hit.collider.gameObject.GetComponent<Piece>().Onhit();
            Debug.Log("Did Hit");
        }
       
            //Debug.Log("Did not Hit"); 
    }



    }

