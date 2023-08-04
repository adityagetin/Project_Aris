<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjProposal.aspx.cs" Inherits="Project_Aris.ProjProposal" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Proposals</title>
    <link rel="stylesheet" type="text/css" href="../Style/table.css" />
</head>
<body>
    
    <form id="formProposal" runat="server">
        <div>
            <h2>Project Proposals</h2>
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
                    <th>Attachment</th>
                    <th>Action</th>
                </tr>
                <asp:Repeater ID="rptProposals" runat="server" OnItemCommand="rptProposals_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("p.ProposalID") %></td>
                            <td><%# Eval("p.ScientID") %></td>
                            <td><%# Eval("p.DivID") %></td>
                            <td><%# Eval("p.PropUnderDomain") %></td>
                            <td><%# Eval("p.PropType") %></td>
                            <td><%# Eval("p.PropTitle") %></td>
                            <td><%# Eval("p.PropSubAgency") %></td>
                            <td><%# Eval("p.PropSubDate", "{0:dd/MM/yyyy}") %></td>
                            <td><%# Eval("p.PropNature") %></td>
                            <td><%# Eval("p.PropSummary") %></td>
                            <td><%# Eval("p.PropFundEstimate", "{0:C2}") %></td>
                            <td><%# Eval("p.PropPresentStatus") %></td>
                            <td><%# Eval("p.PropPI") %></td>
                            <td><%# Eval("p.PropCoPIs") %></td>
                            <td><a href='<%# Eval("p.PropAttachment") %>' target="_blank" id="Attach">View Attachment</a></td>
                            <td>
                                <asp:Button runat="server"  ID="Review" Text="Review" CssClass="update-button" CommandName="Review" CommandArgument='<%# Eval("p.ProposalID") %>' target="_blank" />

                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </form>
</body>
</html>

