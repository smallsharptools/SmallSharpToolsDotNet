LibTidy COM Wrapper

This libray is an ATL wrapper around the new library version of
HTML Tidy.  It is _not_ an updated version of the original TidyCOM,
 which wrapped the 04Aug2000 version of Tidy.

To use it, all you should need to do is unzip the contents somewhere
and register the DLL as follows:

regsvr32 /c TidyATL.dll

Try running the VB test driver.  Note, the test driver is not 
pretty, but it will tell you if you can call into the COM lib 
successfully.

For an overview of TidyLib see:
http://tidy.sourceforge.net/docs/libintro.html

This page contains links to the Doxygen API docs, which are fairly
well fleshed out.  The API is not identical to the raw C library,
but it is very similar.  The names used are the same as my C++
wrapper, you can view at http://users.rcn.com/creitzel/tidy/tidyx.h.
You can also view the COM API using OLE View tool.  The pop-up API
help works in VB, so you should be able to get started.  The IDL file
is included in the ZIP for further documentation.  Bottom line, if
you dig just a bit and play with a small test program, you should
figure things out quickly enough.

Please send bugs, questions, comments, suggestions, etc. to me, 
Charles Reitzel, at creitzel@rcn.com.

Enjoy!

