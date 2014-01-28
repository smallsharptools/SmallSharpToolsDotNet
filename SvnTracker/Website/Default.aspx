<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SVN Feeds</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>SmallSharpTools Projects</h2>
        <ul>
        <li><a href="Rss.ashx?url=http://svn.offwhite.net/svn/SmallSharpTools.Tidy/trunk/&title=SmallSharpTools.Tidy">http://svn.offwhite.net/svn/SmallSharpTools.Tidy/trunk/</a></li>
        <li><a href="Rss.ashx?url=http://svn.offwhite.net/svn/SmallSharpTools.Packer/trunk/&title=SmallSharpTools.Packer">http://svn.offwhite.net/svn/SmallSharpTools.Packer/trunk/</a></li>
        <li><a href="Rss.ashx?url=http://svn.offwhite.net/svn/SmallSharpTools.WebPreview/trunk/&title=SmallSharpTools.WebPreview">http://svn.offwhite.net/svn/SmallSharpTools.WebPreview/trunk/</a></li>
        <li><a href="Rss.ashx?url=http://svn.offwhite.net/svn/SmallSharpTools.Messenger/trunk/&title=SmallSharpTools.Messenger">http://svn.offwhite.net/svn/SmallSharpTools.Messenger/trunk/</a></li>
        <li><a href="Rss.ashx?url=http://svn.offwhite.net/svn/SmallSharpTools.Logger/trunk/&title=SmallSharpTools.Logger">http://svn.offwhite.net/svn/SmallSharpTools.Logger/trunk/</a></li>
        </ul>
        
        <h2>Tigris</h2>
        <ul>
        <li><a href="Rss.ashx?url=http://svn.collab.net/repos/svn/trunk/&title=Subversion&username=guest">http://svn.collab.net/repos/svn/trunk/</a></li>
        <li><a href="Rss.ashx?url=http://tortoisesvn.tigris.org/svn/tortoisesvn/trunk/&title=Subversion&username=guest">http://tortoisesvn.tigris.org/svn/tortoisesvn/trunk</a></li>
        </ul>
        
        <h2>Halawai</h2>
        <ul>
        <li><a href="Rss.ashx?url=http://halawai.googlecode.com/svn/trunk/&title=Halawai">http://halawai.googlecode.com/svn/trunk/</a></li>
        </ul>
    </div>
    </form>
</body>
</html>
