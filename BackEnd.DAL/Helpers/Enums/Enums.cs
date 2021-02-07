using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Helpers.Enums
{
    public enum UserType
    {
        Admin = 1,
        ServiceIntroduction = 2,
        ServiceRecipient = 3 
    }
    public enum ServiceCondition
    {
        Free=1,
        Paid=2    
    }
 
    public enum TimeType
    {    
        day=1,
        Month = 2,
        Year = 3
    }
    public enum AccountType
    {
        Personal=1,
        Commercial=2 
    }
}
