using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public enum Status
    {
        isActive = 0,
        isDeleted = 1,
        isUpdated = 2,
        isBlackListed = 3,
        isDefault = 4,
        isInactive = 5,
        isExpired = 6,
    }
}
