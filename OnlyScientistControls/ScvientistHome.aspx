<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScvientistHome.aspx.cs" Inherits="Project_Aris.OnlyScientistControls.ScvientistHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scientist Login</title>
    <link rel="stylesheet" type="text/css" href="../Style/homes.css" />
</head>
<body>
    <div id="navbar">
        <div class="welcome">
            <h3>HELLO! <asp:Label runat="server" ID="name"></asp:Label></h3>
        </div>
        <div class="options">
            <a href="#" onclick="loadPage('Add')">
                Add Submission
            </a>
            <a href="#" onclick="loadPage('View')">
                View Submisiion
            </a>
            <a href="../Default.aspx">
                LogOut!
            </a>
        </div>
    </div>
    <div id="pageContainer">
        <div id="Add" class="page">
            <iframe src="AddProjProposal.aspx" frameborder="0"></iframe>
        </div>
        <div id="View" class="page">
            <iframe src="Table.aspx" frameborder="0"></iframe>
        </div>
    </div>
    <script src="../Scripts/PageScript.js"></script>
</body>
</html>
