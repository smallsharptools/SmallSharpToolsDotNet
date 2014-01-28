using System;
using Tidy;

namespace TestTidy
{
	/// <summary>
	/// Testing the Tidy .NET wrapper
	/// </summary>
	class TestIt
	{
    static void Status( int status )
    {
      Console.Write( "\tdone. (" );
      Console.Write( status );
      Console.WriteLine( ")." );
    }

		// [STAThread]
		static void Main(string[] args)
    {
      string inFile =  @"foo.html";
      string outFile = @"foo.out.html";
      string optFile = @"foo.tidy";
      string errFile = @"foo.errors.txt";
      int status = 0;

      Console.Write( "Creating document object ..." );
      Document tdoc = new Document();
      Console.WriteLine( "\tdone." );

      if ( status >= 0 )
      {
        Console.Write( "Loading options ..." );
        status = tdoc.LoadConfig( optFile );
        Status( status );
      }

      if ( status >= 0 )
      {
        Console.Write( "Setting error file ..." );
        status = tdoc.SetErrorFile( errFile );
        Status( status );
      }

      if ( status >= 0 )
      {
        Console.Write( "parse file..." );
        status = tdoc.ParseFile( inFile );
        Status( status );
      }

      if ( status >= 0 )
      {
        Console.Write( "cleaning file..." );
        status = tdoc.CleanAndRepair();
        Status( status );
      }

      if ( status >= 0 )
      {
        Console.Write( "running diagnostics..." );
        status = tdoc.RunDiagnostics();
        Status( status );
      }

      if ( status > 1 )
      {
        tdoc.SetOptBool( TidyOptionId.TidyForceOutput, 1 );
      }

      if ( status >= 0 )
      {
        Console.Write( "saving file...");
        status = tdoc.SaveFile( outFile );
        Status( status );
      }
    }
  }
}
