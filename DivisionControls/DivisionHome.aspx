<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DivisionHome.aspx.cs" Inherits="Project_Aris.DivisionControls.DivisionHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Division Login</title>
    <link rel="stylesheet" type="text/css" href="../Style/Welcome.css" />
</head>
<body>
    <div id="navbar">
        <div class="welcome">
            <h3>HELLO! <asp:Label runat="server" ID="name"></asp:Label></h3>
        </div>
        <div class="options">
            <a href="#" onclick="loadPage('Proposals')">
                View Proposals
            </a>

            <a href="#" onclick ="loadPage('Submissions')">
                View Submissions
            </a>
            
            <a href="../Default.aspx">
                LogOut!
            </a>
        </div>
    </div>
    <div id="pageContainer">
       
        <div id="Proposals" class="page">
            
            <iframe src="../DivisionControls/ProjProposalSubmission.aspx" frameborder="0"></iframe>
        </div>

         <div id ="Submissions" class="page">
            <iframe src="SubmissionsTable.aspx" frameborder="0"></iframe>
        </div>
    </div>
    <script src="../Scripts/PageScript.js"></script>
</body>
</html>
