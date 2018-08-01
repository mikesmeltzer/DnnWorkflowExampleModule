/*

MIT License

Copyright (c) 2018 Mike Smeltzer (www.mikesmeltzer.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using System;
using MikeSmeltzer.Modules.WorkflowExample.Components;
using DotNetNuke.Services.Exceptions;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MikeSmeltzer.Modules.WorkflowExample.Model;

namespace MikeSmeltzer.Modules.WorkflowExample
{
    /// -----------------------------------------------------------------------------
    /// <summary>   
    /// The Edit class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from DNNModule1ModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : WorkflowExampleModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Implement your edit logic for your module
                if (!Page.IsPostBack)
                {

                    var tc = new ItemController();

                    if (ItemId > 0 && ContentKey != null)
                    {
                       
                        // Get the current content item
                        var t = tc.GetItem(ModuleId, ItemId); 
                        if (t != null)
                        {
                            txtName.Text = t.ItemName;
                            txtDescription.Text = t.ItemDescription;
                          
                        }



                        // Get all content items
                        var versionHistory = tc.GetItemHistory(ModuleId, ContentKey);
                        rptVersionHistory.DataSource = versionHistory;
                        rptVersionHistory.DataBind();

                    }

                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var t = new Item();
            var tc = new ItemController();

            if (ItemId > 0)
            {
                t = tc.GetItem(ModuleId, ItemId);
                t.ItemName = txtName.Text.Trim();
                t.ItemDescription = txtDescription.Text.Trim();
                t.LastModifiedByUserId = UserId;
                t.LastModifiedOnDate = DateTime.Now;
              
            }
            else
            {
                t = new Item()
                {
                   
                    CreatedByUserId = UserId,
                    CreatedOnDate = DateTime.Now,
                    ItemName = txtName.Text.Trim(),
                    ItemDescription = txtDescription.Text.Trim(),

                };
            }

            t.LastModifiedOnDate = DateTime.Now;
            t.LastModifiedByUserId = UserId;
            t.ModuleId = ModuleId;

            if (t.ContentItemId > 0)
            {
                tc.UpdateItem(t);
            }
            else
            {
                tc.CreateItem(ModuleId, t);
            }

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
     
        }


        protected void rptVersionHistoryOnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                var t = (ItemHistory)e.Item.DataItem;

                if (t.ContentItemID == ItemId)
                {

                    ((ItemHistory)e.Item.DataItem).State = "Published";

                    HtmlTableRow tableRow = ((HtmlTableRow)e.Item.FindControl("trItem"));
                    tableRow.Attributes["style"] = "font-weight:bold;";

                } 

            }
        }

    }
}