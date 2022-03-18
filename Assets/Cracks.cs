using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cracks : MonoBehaviour
{
    public Sprite[] cracks;
    public Camera cam;
    public Vector3 target;
    public GameObject parent;

    public float minSize = 1;
    public float maxSize = 3; 
   
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("click");


            Vector3 pos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, target.z));
            GameObject newCrack = GameObject.Instantiate(parent);
            newCrack.transform.position = pos;
            float scale = Random.RandomRange(minSize, maxSize);
            newCrack.transform.localScale = new Vector3(scale, scale, scale);
            newCrack.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.RandomRange(0, 360)));
            //cracks[Random.Range(0, cracks.Length - 1)]
        }
    }
}
