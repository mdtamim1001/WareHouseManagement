﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.interfaces
{
    public interface ICreate<Type, ID, RET>
    {
        RET Create(Type obj);

    }
}
