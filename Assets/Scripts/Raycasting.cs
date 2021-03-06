using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
   
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject sphere;
    [SerializeField] private Material otherMat;
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject FreeLookObject; 

    [SerializeField]private float weightSmoothTime = 0.4f; 
    private float weightSmoothVel;

    private float currentWeight;

    [SerializeField] private bool debug; 

    private RaycastHit _hit;
    private Vector3 _targetPos;
    private Ray ray;

    Outline _outline; 

    void PrintDebug(string message)
    {
        if (debug)
        {
            Debug.Log(message);
        }
    }

    private void Start()
    {
       Cursor.lockState =  CursorLockMode.Locked;
        FreeLookObject.SetActive(true);
    }

    private void LateUpdate()
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
        Vector3 newPos = new Vector3(pos.x, pos.y, camera.transform.position.z +1 );
        //Vector3 _worldPos = camera.ScreenToWorldPoint(newPos);
        //print(_worldPos);
        //sphere.transform.position = _worldPos;
        //_targetPos = _worldPos - transform.position; //why was this easy to fix, this shouldn't be easy to fix q-q

        ray = Camera.main.ScreenPointToRay (newPos);

        if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
        {

            Debug.DrawRay(ray.origin, ray.direction * _hit.distance, Color.yellow);
            sphere.transform.position = _hit.point;

            Piece piece = _hit.collider.gameObject.GetComponent<Piece>();
            if (piece != null)
            {
                RaiseHand(1f);
                if (_outline != piece.outline && _outline != null)
                {
                    _outline.OutlineColor = new Color(1, 1, 1, 0);
                    _outline = null;
                }
                if (_outline == null)
                {
                    piece.outline.OutlineColor = new Color(1, 1, 1, 1);
                    _outline = piece.outline;
                }
               
                //print("raise" + currentWeight);
            }
            else
            {
                RaiseHand(0f);
                if (_outline != null)
                {
                    _outline.OutlineColor = new Color(1, 1, 1, 0);
                    _outline = null;
                }
                //print("dont raise" + currentWeight);
            }
        }
        else
        {
            Debug.DrawRay(ray.origin,ray.direction * 1000, Color.white);        
        }     
    }

    private void SelectSquare()
    {
        PrintDebug("Click!");
        if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
        {

            Piece piece = _hit.collider.gameObject.GetComponent<Piece>();
            if(piece != null)
            {

                piece.Onhit();
            }

            PrintDebug("Did Hit");
        }       
            //Debug.Log("Did not Hit"); 
    }


  


    public void RaiseHand(float target)
    {
        animator.SetLayerWeight(2, Mathf.SmoothDamp(currentWeight, target, ref weightSmoothVel, weightSmoothTime));
        currentWeight = animator.GetLayerWeight(2);
    }

    }

