using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Base
{
    public enum Roles
    {
        Admin = 1,
        manager = 2,
        UserLevel1 = 3,
        UserLevel2 = 4,

    }

    public enum TestItemStatus : int
    {
        /// <summary>
        /// در انتظار تایید
        /// </summary>
        WaiteForAccept = 1,
        /// <summary>
        /// تایید شده
        /// </summary>
        Accept = 2,
        /// <summary>
        /// تایید نشده
        /// </summary>
        InAccept = 3,
    }
}
