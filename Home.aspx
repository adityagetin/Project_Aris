<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Project_Aris.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css"  href="Style/Welcome.css"/>
</head>
<body>
<div id="navbar">
        
        <div class ="welcome">
         <h3>HELLO! <asp:Label runat="server" ID="name"></asp:Label></h3>
        </div>
        <div class ="options">
           
                 
                 <a href="#" onclick="loadPage('Add')">Submit New Proposals</a>
                <a href="#" onclick="loadPage('View')">View My Sumissions</a>
                <a href="#" onclick="loadPage('Proposals')">View Proposals</a>
                <a href="#" onclick="loadPage('sub')">Submissions</a>
                <a href="#" onclick="loadPage('userPage')">Approval Process</a>
                <a href="Default.aspx"  style="color:orange;">LogOut</a>
        </div>
    </div>

    <div id="pageContainer">


         <div id="View" class="page">
            <iframe src="PMEControls/ProjProposal.aspx" frameborder="0"></iframe>
        </div>
        
        <div id="Add" class="page">
            <iframe src="OnlyScientistControls/AddProjProposal.aspx" frameborder="0"></iframe>
        </div>

        <div id="Proposals" class="page">
            <iframe src="DivisionControls/Table.aspx" frameborder="0"></iframe>
        </div>

        <div id="sub" class="page">
            <iframe src="OnlyScientistControls/SubmissionsTable.aspx" frameborder="0"></iframe>
        </div>

        <div id="userPage" class="page">
            <iframe src="PMEControls/ProjProposalApprovalProcess.aspx" frameborder="0"></iframe>
        </div>
    </div>


    <script src="Scripts/PageScript.js"></script>
</body>
</html>
