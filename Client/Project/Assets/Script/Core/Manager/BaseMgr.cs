using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CSF
{
    public class BaseMgr<T>: MonoBehaviour where T : MonoBehaviour
    {              
        public static T Create()
        {
            GameObject _mgrRoot = GameObject.Find("__Manager__");
            if (_mgrRoot == null)
            {
                _mgrRoot = new GameObject("__Manager__");
                DontDestroyOnLoad(_mgrRoot);
            }
            T mgr = _mgrRoot.GetComponent<T>();
            if (mgr == null)
            {
                mgr = _mgrRoot.AddComponent<T>();
            }
            return mgr;
        }  

        public virtual void Dispose()
        {

        }

    }
}
