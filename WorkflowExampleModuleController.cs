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

using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Web.Api;
using MikeSmeltzer.Modules.WorkflowExample.Components;
using System;
using System.Net;
using System.Net.Http;

namespace MikeSmeltzer.Modules.WorkflowExample
{
    public class WorkflowExampleModuleController : DnnApiController
    {

        // TODO: Stubbed out for future use.  Module will be converted into using Web API for all data management.


        /*

        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public HttpResponseMessage GetAvailableWorkflows(int PortalID)
        {
            try
            {

                WorkflowController workflowController = new WorkflowController();

                return Request.CreateResponse(HttpStatusCode.OK, workflowController.GetAvailableWorkflows(PortalID));
       

            }
            catch (Exception ex)
            {

                Exceptions.LogException(ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);

            }

        }

        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public HttpResponseMessage GetPublishedContentItems(int moduleID)
        {
            try
            {

                var tc = new ItemController();

                return Request.CreateResponse(HttpStatusCode.OK, tc.GetItems(moduleID));


            }
            catch (Exception ex)
            {

                Exceptions.LogException(ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, String.Empty);

            }


        }

        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public HttpResponseMessage UpdateContentItem(int moduleID, int contentItemID, Item content)
        {
            try
            {


                var t = new Item();
                var tc = new ItemController();

                if (contentItemID > 0)
                {
                    t = tc.GetItem(moduleID, contentItemID);
                    t.ItemName = content.ItemName.Trim();
                    t.ItemDescription = content.ItemDescription.Trim();
                    t.LastModifiedByUserId = this.UserInfo.UserID;
                    t.LastModifiedOnDate = DateTime.Now;

                }
                else
                {
                    t = new Item()
                    {

                        CreatedByUserId = this.UserInfo.UserID,
                        CreatedOnDate = DateTime.Now,
                        ItemName = content.ItemName.Trim(),
                        ItemDescription = content.ItemDescription.Trim(),
                        LastModifiedByUserId = this.UserInfo.UserID,
                        LastModifiedOnDate = DateTime.Now


                    };
                }

                t.ModuleId = this.ActiveModule.ModuleID;

                if (t.ContentItemId > 0)
                {
                    tc.UpdateItem(t);
                }
                else
                {
                    tc.CreateItem(t.ModuleId, t);
                }


                return Request.CreateResponse(HttpStatusCode.OK, "");


            }
            catch (Exception ex)
            {

                Exceptions.LogException(ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, String.Empty);

            }


        }

        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public HttpResponseMessage GetUnpublishedContent(int moduleID)
        {
            try
            {

                var tc = new ItemController();

                return Request.CreateResponse(HttpStatusCode.OK, tc.GetItems(moduleID));

            }
            catch (Exception ex)
            {

                Exceptions.LogException(ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, String.Empty);

            }


        }

        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public HttpResponseMessage DeleteContentItem(int contentID)
        {
            try
            {

                return Request.CreateResponse(HttpStatusCode.OK, "");


            }
            catch (Exception ex)
            {

                Exceptions.LogException(ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, String.Empty);

            }


        }

    */

    }
}