using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{

    public class Lang
    {        
        public string key { get; set; }
      
        /// <summary>
        /// 系统黓认语言
        /// </summary>
        public string Value
        {
            get
            {
                return MapEditor.I.Lang.Get(key);
            }
        }
    }
}
