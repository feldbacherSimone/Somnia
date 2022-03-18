using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [Range(0, 10)]
    public float strength = 10;
    public float yOffset = 0;
    public float xOffset = 0;
    public float zDepth = -2;
    Vector3 initPos;
    public bool camera;
    [SerializeField]
    float scrollMin, scrollMax; 
    private void Start()
    {
        zDepth = gameObject.transform.position.z; 
        initPos = gameObject.transform.position;
        scrollMin = gameObject.transform.position.z; 
        //mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); 
    }
    void moveCameraTarget()
    {
        // print(Input.mousePosition);
        Vector3 pos = Input.mousePosition;
        float realitveMouseX = pos.x / Screen.width  - 0.5f + xOffset;
        float realitveMouseY = pos.y / Screen.height - 0.5f + yOffset;

        if (camera)
        {
            if (zDepth >= scrollMin && zDepth <= scrollMax)
            {
                zDepth += Input.mouseScrollDelta.y;
                //print("mouse Scroll: " + Input.mouseScrollDelta.y);
            }
            else if (zDepth < scrollMin)
            {
                zDepth = scrollMin;
            }
            else if (zDepth > scrollMax)
            {
                zDepth = scrollMax;
            }
        }
        

        Vector3 newPos = new Vector3(initPos.x + realitveMouseX * strength, initPos.y + realitveMouseY * strength, zDepth);



        gameObject.transform.position = newPos;
        
        // gameObject.transform.position = new Vector3(pos.x, pos.y, -2);

    }

    private void Update()
    {
        moveCameraTarget();
    }


    
}
