<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjProposalSubmission.aspx.cs" Inherits="Project_Aris.DivisionControls.ProjProposalSubmission" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Proposals</title>
    <link href="../Style/table.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <h2>Project Proposals </h2>
    <form id="formTable" runat="server">
        <table>
            <tr>
                <th>Proposal ID</th>
                <th>Scientist ID</th>
                <th>Division ID</th>
                <th>Domain</th>
                <th>Type</th>
                <th>Title</th>
                <th>Sub Agency</th>
                <th>Submission Date</th>
                <th>Nature</th>
                <th>Summary</th>
                <th>Fund Estimate</th>
                <th>Present Status</th>
                <th>PI</th>
                <th>Co-PIs</th>
                <th>SupervisoerID</th>
                <th>Attachment</th>
                <th>Action</th>

            </tr>
            <asp:Repeater ID="rptProposals" runat="server" OnItemCommand="rptSubmissions_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("ProposalID") %></td>
                        <td>
                            <asp:Label ID="SID" runat="server"><%# Eval("ScientID") %></asp:Label>
                        </td>
                        <td><%# Eval("DivID") %></td>
                        <td><%# Eval("PropUnderDomain") %></td>
                        <td><%# Eval("PropType") %></td>
                        <td><%# Eval("PropTitle") %></td>
                        <td><%# Eval("PropSubAgency") %></td>
                        <td><%# Eval("PropSubDate", "{0:dd/MM/yyyy}") %></td>
                        <td><%# Eval("PropNature") %></td>
                        <td><%# Eval("PropSummary") %></td>
                        <td><%# Eval("PropFundEstimate") %></td>
                        <td><asp:Label ID="lblPresentStatus" runat="server" Text='<%# Eval("PropPresentStatus") %>'></asp:Label></td>
                        <td><%# Eval("PropPI") %></td>
                        <td><%# Eval("PropCoPIs") %></td>
                        <td><%#Eval("SupervisoerID") %></td>
                        <td><a href='<%# Eval("PropAttachment") %>' target="_blank" id ="Attach">View Attachment</a></td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="status-field">
                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="update-button" CommandName="UpdateSubmission" CommandArgument='<%# Eval("ProposalID") + "," + Eval("ScientID") + "," + Eval("[SupervisoerID]") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </table>
    </form>
</body>
</html>
