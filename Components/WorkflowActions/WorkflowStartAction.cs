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
using DotNetNuke.Entities.Content.Workflow;
using DotNetNuke.Entities.Content.Workflow.Actions;
using DotNetNuke.Entities.Content.Workflow.Dto;
using DotNetNuke.Entities.Content.Workflow.Entities;

namespace MikeSmeltzer.Modules.WorkflowExample.Components.WorkflowActions
{
    public class WorkflowStartAction : IWorkflowAction
    {
    
        public void DoActionOnStateChanging(StateTransaction stateTransaction)  
        {

            // TODO:  Do soemthing before workflow starts


        }


        public void DoActionOnStateChanged(StateTransaction stateTransaction)
        {

            // TODO:  Do soemthing when the workflow has started

            // If this is a direct publish workflow, there should be only one state in the workflow
            // Automatically complete the workflow or else it stays in limbo

            ContentController contentController = new ContentController();

            ContentItem content = contentController.GetContentItem(stateTransaction.ContentItemId);

            Workflow workflow = WorkflowManager.Instance.GetWorkflow(content);

           if (workflow.FirstState == workflow.LastState)
            {

            WorkflowController workflowController = new WorkflowController();

                workflowController.CompleteState(content, "Completing direct publish workflow.");

            }


        }

        public ActionMessage GetActionMessage(StateTransaction stateTransaction, WorkflowState currentState)
        {

            ContentController contentController = new ContentController();

            return new ActionMessage
            {

                Subject = contentController.GetContentItem(stateTransaction.ContentItemId).ContentTitle + " - New Item Created",
                Body = "Your item has been created.  Would you like to submit it for approval?"


            };
           
        }
 

    }
}