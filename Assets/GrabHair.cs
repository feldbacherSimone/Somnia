using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHair : MonoBehaviour
{
    Vector3 worldPos;
    Vector3 mousePos; 
    public Camera camera;
    public float zDistance;
    GameObject mouseCollider;

    private void OnMouseDown()
    {
        StartCoroutine(DragHair()); 
    }


    private void Start()
    {
      mouseCollider = new GameObject("MouseCollider", typeof(SphereCollider));
   
        
    }

    private void Update()
    {
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistance);
        worldPos = camera.ScreenToWorldPoint(mousePos);

        mouseCollider.transform.position = worldPos; 
    }

    IEnumerator DragHair()
    {
        while (true)
        {
            mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistance);
            worldPos = camera.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.position = worldPos;

            yield return null;

            if (!Input.GetMouseButton(0))
            {
                StopAllCoroutines();
            }
        }
    }
}
