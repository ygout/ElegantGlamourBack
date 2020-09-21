using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElegantGlamour.Core.Error
{
    // Category
    public static partial class ErrorMessage
    {
        public static string Err_Category_Title_Not_Empty => "Le titre de la catégorie ne peut pas être vide";
        public static string Err_Category_Title_Max_Size => "La taille maximale du titre est de 50 caractères";
        public static string Err_Category_Not_Empty => "La catégorie ne peut pas être à vide";
        public static string Err_Category_Already_Exist => "La catégorie existe dejà";
        public static string Err_Category_Does_Not_Exist => "La catégorie n'existe pas"; 
    }
}
