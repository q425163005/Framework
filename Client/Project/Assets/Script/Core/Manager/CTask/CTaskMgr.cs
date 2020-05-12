using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UObject = UnityEngine.Object;
using UnityEngine.U2D;
using AssetBundles;
using System.Threading.Tasks;
using CSF.Tasks;

namespace CSF
{
    //https://github.com/tomblind/unity-async-routines
    
    public class CTaskMgr : BaseMgr<CTaskMgr>
    {
        public RoutineManager Manager { get { return routineManager; } }

        private RoutineManager routineManager = new RoutineManager();

        public CTaskMgr()
        {
            CTask.TracingEnabled = false;
        }
        public virtual void Update()
        {
            routineManager.Update();
        }

        public virtual void LateUpdate()
        {
            routineManager.Flush();
        }

        public void OnDestroy()
        {
            routineManager.StopAll();
        }

        /// <summary> Manages and runs a routine. </summary>
        public CTaskHandle Run(CTask routine, Action<Exception> onStop = null)
        {
            return routineManager.Run(routine, onStop);
        }

        /// <summary> Stops all managed routines. </summary>
        public void StopAll()
        {
            routineManager.StopAll();
        }
    }
}
