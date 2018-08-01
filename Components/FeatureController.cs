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

using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Content.Workflow.Actions;
using System;
using DotNetNuke.Entities.Content;
using MikeSmeltzer.Modules.WorkflowExample.Components.WorkflowActions;

namespace MikeSmeltzer.Modules.WorkflowExample.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for DNNModule1
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------

    //uncomment the interfaces to add the support.
    public class FeatureController : IUpgradeable //: IPortable, ISearchable, IUpgradeable
    {
   
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="Version">The current version of the module</param>
        public string UpgradeModule(string Version)
        {

            WorkflowActionManager _workflowActionManager = new WorkflowActionManager();

            var message = String.Format(" Workflow Module -  Workflow Upgradeable actions for version {0}.)", Version);

            switch (Version)
            {
                case "00.00.03":


                    // Create content type for module
                    var typeController = new ContentTypeController(); 
			        var objContentType = new ContentType { ContentType = "WorkflowModule_Item" };
                    int contentTypeId =  typeController.AddContentType(objContentType);

                    message += "Added content type for workflow module. " + Environment.NewLine;

                    // Register 5 workflow actions: StartWorkflow, CompleteWorkflow, DiscardWorkflow, DiscardState, CompleteState
                    _workflowActionManager.RegisterWorkflowAction(new WorkflowAction
                    {
                        ContentTypeId = contentTypeId,
                        ActionType = WorkflowActionTypes.StartWorkflow.ToString(),
                        ActionSource = typeof(WorkflowStartAction).AssemblyQualifiedName
                    });

                    _workflowActionManager.RegisterWorkflowAction(new WorkflowAction
                    {
                        ContentTypeId = contentTypeId,
                        ActionType = WorkflowActionTypes.CompleteWorkflow.ToString(),
                        ActionSource = typeof(WorkflowCompleteAction).AssemblyQualifiedName
                    });

                    _workflowActionManager.RegisterWorkflowAction(new WorkflowAction
                    {
                        ContentTypeId = contentTypeId,
                        ActionType = WorkflowActionTypes.DiscardWorkflow.ToString(),
                        ActionSource = typeof(WorkflowDiscardAction).AssemblyQualifiedName
                    });

                    _workflowActionManager.RegisterWorkflowAction(new WorkflowAction
                    {
                        ContentTypeId = contentTypeId,
                        ActionType = WorkflowActionTypes.DiscardState.ToString(),
                        ActionSource = typeof(StateDiscardAction).AssemblyQualifiedName
                    });

                    _workflowActionManager.RegisterWorkflowAction(new WorkflowAction
                    {
                        ContentTypeId = contentTypeId,
                        ActionType = WorkflowActionTypes.CompleteState.ToString(),
                        ActionSource = typeof(StateCompleteAction).AssemblyQualifiedName
                    });

                    message += "Added workflow actions. " + Environment.NewLine;
                    break;


            }

            return message;


        }

        #region Optional Interfaces

       

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// -----------------------------------------------------------------------------
        //public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo ModInfo)
        //{
        //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

        //List<DNNModule1Info> colDNNModule1s = GetDNNModule1s(ModInfo.ModuleID);

        //foreach (DNNModule1Info objDNNModule1 in colDNNModule1s)
        //{
        //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objDNNModule1.Content, objDNNModule1.CreatedByUser, objDNNModule1.CreatedDate, ModInfo.ModuleID, objDNNModule1.ItemId.ToString(), objDNNModule1.Content, "ItemId=" + objDNNModule1.ItemId.ToString());
        //    SearchItemCollection.Add(SearchItem);
        //}

        //return SearchItemCollection;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        

        #endregion

    }

}
