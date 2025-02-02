﻿//
// Data Access Tier:  interface between business tier and data store.
//

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DataAccessTier
{

  public class Data
  {
    //
    // Fields:
    //
    private string _DBFile;
    private string _DBConnectionInfo;

    ///
    /// <summary>
    /// Constructs a new instance of the data access tier.  The format
    /// of the filename should be either |DataDirectory|\filename.mdf,
    /// or a complete Windows pathname.
    /// </summary>
    /// <param name="DatabaseFilename">Name of database file</param>
    /// 
    public Data(string DatabaseFilename)
    {
      string version;

      //version = "v11.0";    // for VS 2013:
      version = "MSSQLLocalDB";  // for VS 2015:

      _DBFile = DatabaseFilename;
      _DBConnectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename={1};Integrated Security=True;",
        version,
        DatabaseFilename);
    }

    ///
    /// <summary>
    ///  Opens and closes a connection to the database, e.g. to
    ///  startup the server and make sure all is well.
    /// </summary>
    /// <returns>true if successful, false if not</returns>
    /// 
    public bool OpenCloseConnection()
    {
      SqlConnection db = new SqlConnection(_DBConnectionInfo);

      bool  state = false;

      try
      {
        db.Open();

        state = (db.State == ConnectionState.Open);
      }
      catch
      {
        // nothing, just discard:
      }
      finally
      {
        if (db != null & db.State == ConnectionState.Open)
          db.Close();
      }

      return state;
    }

    ///
    /// <summary>
    /// Executes an sql SELECT query that returns a single value.
    /// </summary>
    /// <param name="sql">query to execute</param>
    /// <returns>an object containing the single, scalar result</returns>
    ///
    public object ExecuteScalarQuery(string sql)
    {
      SqlConnection db = new SqlConnection(_DBConnectionInfo);

      try
      {
        db.Open();

	   // fill the sql command 
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = db;
        cmd.CommandText = sql;

	   // execute the query
        object result = cmd.ExecuteScalar();

        db.Close();

        return result;
        
      }
      catch
      {
        throw;  // if we get an exception, re-throw:
      }
      finally
      {
        if (db != null & db.State == ConnectionState.Open)
          db.Close();
      }
    }

    ///
    /// <summary>
    /// Executes an sql SELECT query that generates a temporary 
    /// table containing 0 or more rows.
    /// </summary>
    /// <param name="sql">query to execute</param>
    /// <returns>a table in the form of a DataSet</returns>
    /// 
    public DataSet ExecuteNonScalarQuery(string sql)
    {
      SqlConnection db = new SqlConnection(_DBConnectionInfo);

      try
      {
        db.Open();

	   // fill the sql command 
	   SqlCommand cmd = new SqlCommand();
        cmd.Connection = db;
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

	   // execute the query
	   cmd.CommandText = sql;
        adapter.Fill(ds);

        db.Close();
    
        return ds;

      }
      catch
      {
        throw;  // if we get an exception, re-throw:
      }
      finally
      {
        if (db != null & db.State == ConnectionState.Open)
          db.Close();
      }
    }

    ///
    /// <summary>
    /// Executes an sql ACTION query --- insert, update, or delete --- that
    /// modifies the database.
    /// </summary>
    /// <param name="sql">query to execute</param>
    /// <returns>the # of rows modified by the query</returns>
    /// 
    public int ExecuteActionQuery(string sql)
    {
      SqlConnection db = new SqlConnection(_DBConnectionInfo);

      try
      {
	   db.Open();

	   // fill the sql command 
	   SqlCommand cmd = new SqlCommand();
        cmd.Connection = db;
        cmd.CommandText = sql;

	   // execute the query
	   int rowsModified = cmd.ExecuteNonQuery();

        db.Close();

	   //if (rowsModified != 1)
	   //  MessageBox.Show("update failed");

	   return rowsModified;
      }
      catch
      {
        throw;  // if we get an exception, re-throw:
      }
      finally
      {
        if (db != null & db.State == ConnectionState.Open)
          db.Close();
      }
    }

  }//class

}//namespace
