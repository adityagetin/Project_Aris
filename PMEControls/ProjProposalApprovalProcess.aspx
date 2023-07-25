<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjProposalApprovalProcess.aspx.cs" Inherits="Project_Aris.ProjProposalApprovalProcess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Style/table.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="formSubmissions" runat="server">
        <h2>Submissions Table</h2>
        <table>
            <tr>
                <th>Approval StepID</th>
                <th>Submission ID</th>
                <th>Supervisor ID</th>
                <th>Submission Status</th>
                <th>Submission Date</th>
                <th>Comment</th>
                <th>Action</th>
            </tr>
            <asp:Repeater ID="rptSubmissions" runat="server" OnItemCommand="rptSubmissions_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("ApprovalStepId") %></td>
                        <td><%# Eval("SubmissionID") %></td>
                        <td><%# Eval("SupervisorID") %></td>
                        <td><%# Eval("SubmissionStatus") %></td>
                        <td><%# Eval("SubmissionDate", "{0:dd/MM/yyyy}") %></td>
                        <td><%# Eval("Comment") %>
                        </td>
                         <td>
                            <asp:Button ID="Accept" runat="server" Text="Accept" CssClass="Accept-button" CommandName="AcceptSubmission" CommandArgument='<%# Eval("SubmissionID")%>' OnClientClick="target='_blank';"/><br />
                             <asp:Button ID="Reject" runat="server" Text="Reject" CssClass="Reject-button" CommandName="RejectSubmission" CommandArgument='<%# Eval("SubmissionID")%>'/>
                        </td>

                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </form>
</body>

</html>