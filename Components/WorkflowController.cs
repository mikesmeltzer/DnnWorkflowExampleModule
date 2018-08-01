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
using DotNetNuke.Entities.Content.Workflow.Dto;
using DotNetNuke.Entities.Content.Workflow.Entities;
using DotNetNuke.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MikeSmeltzer.Modules.WorkflowExample.Components
{
    public class WorkflowController
    {

        IWorkflowManager _workflowManager;
        IUserController _userController;
        IWorkflowEngine _workflowEngine;

        public WorkflowController()
        {

            _workflowManager = WorkflowManager.Instance;
            _workflowEngine = WorkflowEngine.Instance;
            _userController = UserController.Instance;

        }

        public void StartWorkflow(ContentItem contentItem, int workflowId)
        {

            _workflowEngine.StartWorkflow(workflowId, contentItem.ContentItemId, _userController.GetCurrentUserInfo().UserID);

        }

        public void CompleteState(ContentItem contentItem, string comment)
        {

            StateTransaction stateTransaction = new StateTransaction
            {  
               
                ContentItemId = contentItem.ContentItemId,
                CurrentStateId = contentItem.StateID,

                Message = new StateTransactionMessage
                {

                    UserComment = comment
                },
                UserId = _userController.GetCurrentUserInfo().UserID

            };

            _workflowEngine.CompleteState(stateTransaction);

        }

        public void DiscardState(ContentItem contentItem, string comment)
        {

            StateTransaction stateTransaction = new StateTransaction
            {

                ContentItemId = contentItem.ContentItemId,
                CurrentStateId = contentItem.StateID,
                Message = new StateTransactionMessage
                {

                    UserComment = comment
                },
                UserId = _userController.GetCurrentUserInfo().UserID

            };

            _workflowEngine.DiscardState(stateTransaction);

        }

        public String GetStateName(ContentItem contentItem)
        {
            String stateName = String.Empty;

            if (contentItem.StateID != -1)
            {

                if (_workflowManager.GetWorkflow(contentItem) != null )
                {

                    WorkflowState workflow = _workflowManager.GetWorkflow(contentItem).States.Where(i => i.StateID == contentItem.StateID).FirstOrDefault();

                    if (workflow != null)
                    {

                        stateName = workflow.StateName;

                    }

                }


            }

            return stateName;

        }

        public List<Workflow> GetAvailableWorkflows(int portalID)
        {

            List<Workflow> workflows = _workflowManager.GetWorkflows(portalID).ToList();

            return workflows;

        }

    }
}