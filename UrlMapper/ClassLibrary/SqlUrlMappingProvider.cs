/*=======================================================================
  Copyright (C) SmallSharpTools.com.  All rights reserved.
 
  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
  
  Brennan Stehling
  brennan@smallsharptools.com
  http://www.smallsharptools.com/
=======================================================================*/

using System.Collections.Specialized;

namespace SmallSharpTools.UrlMapping
{
    public class SqlUrlMappingProvider : UrlMappingProvider
    {

        #region "  Variables  "
        string _connectionString = "";
        #endregion

        #region "  Implementation Methods  "

        /// <summary>
        /// SQL Implementation
        /// </summary>
        public override void Initialize(string name, NameValueCollection configValue)
        {
            _connectionString = configValue["connectionString"].ToString();
        }

        public override bool IsMappedPath(string path)
        {
            return false;
        }

        public override string GetMappedPath(string path)
        {
            return path;
        }

        public override bool IsLoggingEnabled
        {
            get { return false; }
        }

        #endregion

        #region "  Utility Methods  "

        //private void HandleError(SqlParameter[] paramArray, SqlException x, string sprocName)
        //{
        //    string sException = "Error Executing " + sprocName + ": " + x.Message + " \r\n";
        //    foreach (SqlParameter p in paramArray)
        //    {
        //        sException += p.ParameterName + "=" + p.Value + "\r\n";
        //    }
        //    logger.Error(sException, x);
        //    throw new Exception(sException, x);
        //}

        /// <summary>
        /// SQL Implementation
        /// </summary>
        //public void ClearUrlMappingCache(String userName)
        //{
        //    String cacheKey = "UrlMapping::" + userName;
        //    if (HttpRuntime.Cache.Get(cacheKey) != null)
        //    {
        //        HttpRuntime.Cache.Remove(cacheKey);
        //    }
        //}

        //private String GetBooleanAsChar(bool value)
        //{
        //    if (value)
        //    {
        //        return "1";
        //    }
        //    else {
        //        return "0";
        //    }
        //}

        //private void LogDataSet(DataSet ds)
        //{
        //    foreach (DataTable table in ds.Tables)
        //    {
        //        logger.Info("############################################");
        //        logger.Info("Table: " + table.TableName);
        //        foreach (DataRow row in table.Rows)
        //        {
        //            foreach (DataColumn column in table.Columns)
        //            {
        //                logger.Info(String.Format("{0}: {1} <{2}>", column.ColumnName, row[column.ColumnName].ToString(), row[column.ColumnName].GetType().ToString()));
        //            }
        //        }
        //        logger.Info("############################################");
        //    }
        //}

        #endregion
    }
}
