using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ContryClick : MonoBehaviour
{
    private Vector3 earthStartPos = new Vector3(0, 4.04f, -8.67f);
    public Transform cam;
    private List<GameObject> levelList = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            levelList.Add(transform.GetChild(i).gameObject);
        }
    }

    void Start()
    {
        cam.DOLocalMove(earthStartPos, 1);
    }

    void LateUpdate()
    {
        for (int i = 0; i < levelList.Count; i++)
        {
            Transform trans = levelList[i].transform;

            Vector3 v1 = new Vector3(cam.position.x, transform.position.y, cam.position.z) - transform.position;
            Vector3 v2 = new Vector3(trans.position.x, transform.position.y, trans.position.z) - transform.position;
            
            if (Vector3.Angle(v1, v2) < 15f)
            {
                if (!trans.gameObject.activeSelf)
                {
                    trans.gameObject.SetActive(true);
                }
            }
            else
            {
                trans.gameObject.SetActive(false);
            }
        }
    }
}
