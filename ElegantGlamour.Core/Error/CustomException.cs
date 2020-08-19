using System;
using System.Collections.Generic;
using System.Text;

namespace ElegantGlamour.Core.Error
{
    class CustomException
    {

    }

    public class CategoryAlreadyExistException : Exception
    {
        public CategoryAlreadyExistException()
            : base(String.Format(ErrorMessage.Err_Category_Already_Exist))
        { }
    }
}
