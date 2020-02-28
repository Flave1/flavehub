using System;
using System.Collections.Generic; 

namespace flavehub.Contracts.ApiCore
{

    public enum UserLevel
    {
        Beginner,
        Master,
        Skilled,
        Elite,
        Ultimate
    }
    public enum PostStatus
    {
        Available = 1,
        Unavailable,
        Deleted,
        Conformed,
        UnConfirmed,
        Pending,
        Cancelled
    }

   
}
