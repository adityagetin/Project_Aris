<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Table.aspx.cs" Inherits="Project_Aris.Table" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Proposals</title>
    <link href="../Style/Tables.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <h2>Project Proposals </h2>
    <br />
    
    <form id="formTable" runat="server">
       <div>
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

            </tr>
            <asp:Repeater ID="rptProposals" runat="server" OnItemDataBound="rptProposals_ItemDataBound">
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
                        <td>
                            <asp:Label ID="lblPresentStatus" runat="server" Text='<%# Eval("PropPresentStatus") %>'></asp:Label>
                        </td>
                        <td><%# Eval("PropPI") %></td>
                        <td><%# Eval("PropCoPIs") %></td>
                        <td>
                            <a href='<%# Eval("PropAttachment") %>' target="_blank" id ="Attach">View Attachment</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </table>
       </div>
        
    </form>
</body>
</html>
