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

        <div class="home">
            <form id="form1" runat="server">
        <div class="container">
            <h1>Review Meetings</h1>
            <asp:Repeater ID="rptReviewMeetings" runat="server">
                <ItemTemplate>
                    <div class="card">
                        <h2><%# Eval("ProposalID") %></h2>
                        <ul>
                            <li><strong>Date:</strong> <%# Eval("ReviewMeetingDt", "{0:dd/MM/yyyy}") %></li>
                            <li><strong>Action:</strong> <%# Eval("ReviewAction") %></li>
                        </ul>
                        <p><%# Eval("ReviewComent") %></p>
                        <asp:LinkButton ID="lnkViewAttachment" runat="server" Text="View Attachment" CommandArgument='<%# Eval("ProceedingAttachment") %>' OnClick="lnkViewAttachment_Click"></asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
        </div>

         <div id="View" class="page">
            <iframe src="PMEControls/ProjProposal.aspx" frameborder="0"></iframe>
        </div>
        
        <div id="Add" class="page">
            <iframe src="OnlyScientistControls/AddProjProposal.aspx" rameborder="0"></iframe>
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
