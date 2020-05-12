using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix_Project
{
    /// <summary>
    /// 单例基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDataMgr<T> where T :IDisposable, new()
    {
        protected static T instance;
        /// <summary>
        /// 实例
        /// </summary>
        public static T I
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                    Mgr.__dataMgrList.Add(instance);
                }
                return instance;
            }
        }

        public virtual void Dispose()
        {
        }
    }
}
