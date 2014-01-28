<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DragAndDropDemo.aspx.cs" Inherits="DragAndDropDemo" %>

<%@ Register Src="Controls/Navigation.ascx" TagName="Navigation" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Scriptaculous: Drag and Drop Demo</title>
    <style type="text/css">
    #Blue {
        background: blue;
    }
    #Red {
        background: red;
    }
    #Green {
        background: green;
    }
    #Yellow {
        background: yellow;
    }
    div.color {
        margin: 5px;
        border: 1px solid #000;
        height: 30px;
        width: 100px;
    }
    div.color p {
        margin: 1px;
        font-weight: bold;
    }
    </style>
    <script type="text/javascript" src="ui.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h3>Scriptaculous: Drag and Drop</h3>
    <fieldset>
    <legend>Drag and Drop</legend>
    <div id="Blue" class="color cdropTarget cdragTarget"><p>&nbsp;</p></div>
    <div id="Red" class="color cdropTarget cdragTarget"><p>&nbsp;</p></div>
    <div id="Green" class="color cdropTarget cdragTarget"><p>&nbsp;</p></div>
    <div id="Yellow" class="color cdropTarget cdragTarget"><p>&nbsp;</p></div>
    </fieldset>
    <div id="label"></div>

<script type="text/javascript">
       var colors = document.getElementsByClassName('color');

       function message(dragTarget,dropTarget)
       {
              var from = dragTarget.id;
              var to   = dropTarget.id;
              document.getElementById('label').innerHTML = '<b>' + from + ' dropped on to ' + to + '</b>';
       }

       colors.each(function(obj,at)
       {
            new Draggable(obj.id,{revert:true});
            Droppables.add(obj.id,
            {
                accept: 'cdragTarget',
                onDrop: function(dragTarget)
                { message(dragTarget,obj);}
            });
       });
</script>
    <div id="links"></div>
    <script type="text/javascript">
    showLinks('links');
    </script>
        <sst:EmbeddedScriptsManager ID="esm1" runat="server" UseScriptaculousDragDropScript="True" />
        <uc1:Navigation ID="Navigation1" runat="server" />
    </div>
    </form>
</body>
</html>
