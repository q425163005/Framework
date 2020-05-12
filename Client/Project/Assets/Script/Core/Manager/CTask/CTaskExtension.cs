using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.Tasks
{
    public static class CTaskExtension
    {
        public static CTaskHandle Run(this CTask task)
        {
            return Mgr.Task.Manager.Run(task);
        }
    }
}
