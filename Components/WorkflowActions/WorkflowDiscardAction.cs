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

using DotNetNuke.Entities.Content;
using DotNetNuke.Entities.Content.Workflow.Actions;
using DotNetNuke.Entities.Content.Workflow.Dto;
using DotNetNuke.Entities.Content.Workflow.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MikeSmeltzer.Modules.WorkflowExample.Components.WorkflowActions
{
    public class WorkflowDiscardAction : IWorkflowAction
    {

        private readonly IContentTypeController contentTypeController = new ContentTypeController();

        private readonly IContentController contentController = new ContentController();

        public void DoActionOnStateChanging(StateTransaction stateTransaction)  
        {

         

        }


        public void DoActionOnStateChanged(StateTransaction stateTransaction)
        {


            // Workflow discarded, delete the item
            ContentItem contentItem = contentController.GetContentItem(stateTransaction.ContentItemId);

            if (contentItem != null)
                contentController.DeleteContentItem(contentItem);

        }

        public ActionMessage GetActionMessage(StateTransaction stateTransaction, WorkflowState currentState)
        {

            return new ActionMessage
            {

                Subject = "The Item has been discarded.",
                Body = "Your item has been discarded and the change has been deleted."


            };
           
        }
 

    }
}