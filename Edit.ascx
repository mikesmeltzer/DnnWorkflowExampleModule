<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="MikeSmeltzer.Modules.WorkflowExample.Edit" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<div class="dnnForm dnnEditBasicSettings" id="dnnEditBasicSettings">
    <div class="dnnFormExpandContent dnnRight "><a href=""><%=LocalizeString("ExpandAll")%></a></div>

    <h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead dnnClear">
        <a href="" class="dnnSectionExpanded">
            <%=LocalizeString("BasicSettings")%></a></h2>
    <fieldset>
        <div class="dnnFormItem">
            <dnn:label ID="lblName" runat="server" />
            <asp:TextBox ID="txtName" runat="server" />
        </div>
        <div class="dnnFormItem">

            <dnn:label ID="lblDescription" runat="server" />
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="20" />
        </div>

    </fieldset>

        <h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead dnnClear">
        <a href="" class="dnnSectionExpanded">
           Item History</a></h2>
    <fieldset>

        <table  class="dnnGrid"style="width: 100%; border-collapse: collapse;" cellspacing="0" cellpadding="0">
            <tbody>
                <tr class="dnnGridHeader" style="white-space: nowrap;">
                    <td>Version</td>
                    <td>Date</td>
                    <td>User</td>
                    <td>State</td>
                    <td></td>
                </tr>
   
            <asp:Repeater ID="rptVersionHistory" OnItemDataBound="rptVersionHistoryOnItemDataBound" runat="server">
                <ItemTemplate>
                    <tr id="trItem" class="dnnGridItem" runat="server">
                        <td><%#DataBinder.Eval(Container.DataItem,"ContentItemID").ToString() %></td>
                        <td><%#DataBinder.Eval(Container.DataItem,"Date").ToString() %></td>
                        <td><%#DataBinder.Eval(Container.DataItem,"Username").ToString() %></td>
                        <td><%#DataBinder.Eval(Container.DataItem,"State").ToString() %></td>
                        <td>Delete - Restore - Move to Next State</td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                     <tr id="trItem" class="dnnGridAltItem" runat="server">
                        <td><%#DataBinder.Eval(Container.DataItem,"ContentItemID").ToString() %></td>
                        <td><%#DataBinder.Eval(Container.DataItem,"Date").ToString() %></td>
                        <td><%#DataBinder.Eval(Container.DataItem,"Username").ToString() %></td>
                        <td><%#DataBinder.Eval(Container.DataItem,"State").ToString() %></td>
                        <td>Delete - Restore - Move to Next State</td>
                    </tr>

                </AlternatingItemTemplate>
            </asp:Repeater>
                         </tbody>
        </table>


    </fieldset>


</div>
<asp:LinkButton ID="btnSubmit" runat="server"
    OnClick="btnSubmit_Click" resourcekey="btnSubmit" CssClass="dnnPrimaryAction" />
<asp:LinkButton ID="btnCancel" runat="server"
    OnClick="btnCancel_Click" resourcekey="btnCancel" CssClass="dnnSecondaryAction" />



<script type="text/javascript">
    /*globals jQuery, window, Sys */
    (function ($, Sys) {
        function dnnEditBasicSettings() {
            $('#dnnEditBasicSettings').dnnPanels();
            $('#dnnEditBasicSettings .dnnFormExpandContent a').dnnExpandAll({ expandText: '<%=Localization.GetString("ExpandAll", LocalResourceFile)%>', collapseText: '<%=Localization.GetString("CollapseAll", LocalResourceFile)%>', targetArea: '#dnnEditBasicSettings' });
        }

        $(document).ready(function () {
            dnnEditBasicSettings();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                dnnEditBasicSettings();
            });
        });

    }(jQuery, window.Sys));
</script>
