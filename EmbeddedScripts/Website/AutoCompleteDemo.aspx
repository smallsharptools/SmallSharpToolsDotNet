<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutoCompleteDemo.aspx.cs" Inherits="AutoCompleteDemo" %>

<%@ Register Src="Controls/Navigation.ascx" TagName="Navigation" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Scriptaculous: Auto-Complete Demo</title>

<style type="text/css">
div.autocomplete {
      position:absolute;
      width:250px;
      background-color:white;
      border:1px solid #888;
      margin:0px;
      padding:0px;
    }
    div.autocomplete ul {
      list-style-type:none;
      margin:0px;
      padding:0px;
    }
    div.autocomplete ul li.selected { 
      background-color: yellow;
      font-weight: bold;
    }
    div.autocomplete ul li {
      list-style-type:none;
      display:block;
      margin:0;
      padding:2px;
      height:20px;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>Scriptaculous: Autocomplete Demo</h3>
        <fieldset>
        <legend accesskey="l"> Lookup </legend>
        <input type="text" id="autocomplete" name="autocomplete_parameter"/><div id="autocomplete_choices" class="autocomplete"></div>
        <script type="text/javascript">new Ajax.Autocompleter('autocomplete', 'autocomplete_choices', 'AutoCompleteHandler.ashx', {paramName: 'searchValue', minChars: 2})</script><br/>
        </fieldset>
    
        <sst:EmbeddedScriptsManager ID="esm1" runat="server" UseScriptaculousControlsScript="True" UseScriptaculousEffectsScript="True" UseScriptaculousScript="True" />
    </div>
        <uc1:Navigation ID="Navigation1" runat="server" />
    </form>
</body>
</html>
