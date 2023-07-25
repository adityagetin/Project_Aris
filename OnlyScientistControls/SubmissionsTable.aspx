<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubmissionsTable.aspx.cs" Inherits="Project_Aris.SubmissionsTable" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Approvals</title>
    <link href="../Style/table.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="formSubmissions" runat="server">
        <h2>Submissions</h2>
        <table>
            <tr>
                <th>Submission ID</th>
                <th>Proposal ID</th>
                <th>Scientist ID</th>
                <th>Supervisor ID</th>
                <th>Submission Status</th>
                <th>Submission Date</th>
                <th>Comment</th>
                <th>Action</th>
            </tr>
            <asp:Repeater ID="rptSubmissions" runat="server" OnItemCommand="rptSubmissions_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("SubmissionID") %></td>
                        <td><%# Eval("ProposalID") %></td>
                        <td><%# Eval("ScientID") %></td>
                        <td><%# Eval("SupervisorID") %></td>
                        <td>
                            <asp:Label ID="lblSubmissionStatus" runat="server" Text='<%# Eval("SubmissionStatus") %>'
                                CssClass='<%# Eval("SubmissionStatus").ToString() %>'></asp:Label>
                        </td>
                        <td><%# Eval("SubmissionDate", "{0:dd/MM/yyyy}") %></td>
                        <td><%# Eval("Comment") %>
                        </td>
                         <td>
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="status-field">
                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="update-button" CommandName="UpdateSubmission" CommandArgument='<%# Eval("SubmissionID") + "," + Eval("ProposalID") + "," + Eval("ScientID") + "," + Eval("SupervisorID") + "," + Eval("SubmissionStatus") + "," + Eval("SubmissionDate") + "," + Eval("Comment") %>' />
                        </td>

                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </form>
</body>
</html>
