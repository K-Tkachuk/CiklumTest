using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CiklumTest.Enums
{
    public enum Errors
    {
        [Display(Description = "UserNotAuthorized" )]
        UserNotAuthorized = 0,

        [Display(Description = "UserNotExist" )]
        UserNotExist = 1,

        [Display(Description = "EmptyData" )]
        EmptyData = 2,

        [Display(Description = "IncorrectEmailOrPassword" )]
        IncorrectEmailOrPassword = 3,

        [Display(Description = "DataNotFound" )]
        DataNotFound = 4,

        [Display(Description = "InternalServerError" )]
        InternalServerError = 5,

        [Display(Description = "ServerIgnor" )]
        ServerIgnor = 6,

        [Display(Description = "InvalidToken" )]
        InvalidToken = 7,

        [Display(Description = "UserExist")]
        UserExist = 8,

        [Display(Description = "SomethingWentWrong" )]
        SomethingWentWrong = 9,
       
    }
}
