﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XcustGL_KTP
{
    public class Persistent
    {
        public String table = "";
        public String pkField = "";
        public String sited = "";
        Random r = new Random();
        public String getGenID()
        {
            return r.Next().ToString();
        }
    }
}
