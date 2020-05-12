using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeoplePoint : MonoBehaviour
{

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        List<Transform> list = new List<Transform>();
        foreach (Transform variable in this.transform)
        {
            Gizmos.DrawWireSphere(variable.position, .5f* GetHandleSize(variable.position));
        }
    }

    public virtual float GetHandleSize(Vector3 pos)
    {
        float handleSize = 1f;
#if UNITY_EDITOR
        handleSize = UnityEditor.HandleUtility.GetHandleSize(pos) * 0.4f;
        handleSize = Mathf.Clamp(handleSize, 0, 1.2f);
#endif
        return handleSize;
    }
}
