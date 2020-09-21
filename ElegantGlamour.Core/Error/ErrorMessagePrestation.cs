using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElegantGlamour.Core.Error
{
    // Prestation
    public static partial class ErrorMessage
    {
        public static string Err_Prestation_Title_Not_Empty => "Le titre de la préstation ne peut pas être vide";
        public static string Err_Prestation_Description_Not_Empty => "La description de la préstation ne peut pas être vide";
        public static string Err_Prestation_Price_Not_Empty => "Le prix de la préstation ne peut pas être vide";
        public static string Err_Prestation_Duration_Not_Empty => "La durée de la préstation ne peut pas être vide";
        public static string Err_Prestation_Duration_Not_Equal_To_0 => "La durée de la préstation ne peut pas être équale à 0";
        public static string Err_Prestation_Id_Does_Not_Exist => "L'Id de la prestation n'a pas été trouvé";
        
    }
}
