using System;
using System.Collections.Generic;
using System.Text;

namespace SmallSharpTools.VSX.Composite
{
    public class CompositeHelper
    {
        
        #region "  Methods  "
        
        // <summary>
        // Converts a property name to field name (Name to _name)
        // </summary>
        //<param name="propertyName"></param>
        //<returns></returns>
        public static string GetFieldName(string propertyName)
        {
            return "_" + 
                propertyName.Substring(0,1).ToLower() + 
                propertyName.Substring(1);
        }
        
        // <summary>
        // Converts a property name to parameter name (Name to name)
        // </summary>
        //<param name="propertyName"></param>
        //<returns></returns>
        public static string GetParameterName(string propertyName)
        {
            return propertyName.Substring(0,1).ToLower() + 
                propertyName.Substring(1);
        }

        #endregion

    }
}
