using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelScrol : MonoBehaviour
{

    public float speed = 1;                 //旋转速度

    private bool inmod = false;

    public bool Horizontal;
    public bool Vertical;


    void OnMouseDown()
    {
        inmod = true;
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && inmod)
        {

            if (Horizontal)
                transform.Rotate(Vector3.down * Input.GetAxis("Mouse X") * speed, Space.World);
            if (Vertical)
                transform.Rotate(Vector3.right * Input.GetAxis("Mouse Y") * speed, Space.World);
        }

        if (Input.GetMouseButtonUp(0))
        {
            inmod = false;
        }
#else
//没有触摸
        if (Input.touchCount <= 0)
	    {
            inmod = false;
	        return;
	    }
	    //单点触摸， 水平上下旋转
	    if (1 == Input.touchCount && inmod)
	    {
	        Touch touch = Input.GetTouch(0);
	        Vector2 deltaPos = touch.deltaPosition;
	        if (Horizontal)
	            transform.Rotate(Vector3.down * deltaPos.x* 0.15f, Space.World);
	        if (Vertical)
                transform.Rotate(Vector3.right * deltaPos.y* 0.15f, Space.World);
	    }
#endif
    }
}
