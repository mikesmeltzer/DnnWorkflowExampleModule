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

using System.Collections.Generic;
using DotNetNuke.Entities.Content;
using System;
using System.Linq;
using DotNetNuke.Entities.Modules;
using MikeSmeltzer.Modules.WorkflowExample.Model;

namespace MikeSmeltzer.Modules.WorkflowExample.Components
{
    public class ItemController
    {

        private const string ItemContentTypeName = "WorkflowModule_Item";

        private readonly IContentTypeController contentTypeController = new ContentTypeController();

        private readonly IContentController contentController = new ContentController();

        private readonly ContentType itemContentType;

        private readonly WorkflowController workflowController = new WorkflowController();

        private readonly ModuleController moduleController = new ModuleController();

        public ItemController() 
         {

             this.itemContentType = InitializeContentType(); 
        
        } 

        private ContentType ItemContentType 
         {
             get { return this.itemContentType; } 
         } 


         private ContentType InitializeContentType() 
         {
             var contentType = this.contentTypeController.GetContentTypes().SingleOrDefault(ct => ct.ContentType == ItemContentTypeName); 
 
             return contentType; 
         }

         public IEnumerable<Item> GetItems(int moduleId)
         {
             return from ci in this.contentController.GetContentItemsByModuleId(moduleId)
                    where ci.ContentTypeId == this.ItemContentType.ContentTypeId && bool.Parse(ci.Metadata["Published"].ToString()) == true
                    select
                        new Item(
                            ci.ContentKey,
                            ci.ContentItemId,
                            ci.Metadata["Name"],
                            ci.Content);
         }

         public Item GetItem(int moduleId, int contentItemId)
         {
             return (from ci in this.contentController.GetContentItemsByModuleId(moduleId)
                    where ci.ContentTypeId == this.ItemContentType.ContentTypeId && ci.ContentItemId == contentItemId
                    select
                        new Item(
                            ci.ContentKey,
                            ci.ContentItemId,
                            ci.Metadata["Name"],
                            ci.Content)).FirstOrDefault();
         }

         public List<ItemHistory> GetItemHistory(int moduleId, String contentKey)
         {
             
             // TODO:  Get the last X amount of versions of the content item.  This should be defined in site settings.
             //        HTML Pro uses it, find out it's name and location
            
             return (from ci in this.contentController.GetContentItemsByModuleId(moduleId)
                     where ci.ContentTypeId == this.ItemContentType.ContentTypeId && ci.ContentKey == contentKey
                     select
                         new ItemHistory(
                             ci.ContentItemId,
                             ci.CreatedByUserID,
                             workflowController.GetStateName(ci),
                             ci.CreatedOnDate
                            )).ToList<ItemHistory>();
         }

         public void CreateItem(int moduleId, Item item)
         {
             var contentItem = new ContentItem { ContentTypeId = this.ItemContentType.ContentTypeId, ModuleID = moduleId };
             
             FillContentItem(item, contentItem, true);

             this.contentController.AddContentItem(contentItem);

             int workflowId = -1;
             
             if (moduleController.GetModule(moduleId).ModuleSettings.Contains("Workflow"))
                 workflowId = int.Parse(moduleController.GetModule(moduleId).ModuleSettings["Workflow"].ToString());

             workflowController.StartWorkflow(contentItem, workflowId);

         }

         public void UpdateItem(Item item)
         {

             // TODO:  This isn't working properly..
             // Make it so that you can updaet a draft item.  If item is out of draft, but not published, lock down

             var existingContentItem = contentController.GetContentItem(item.ContentItemId);

             var newContentItem = new ContentItem { ContentKey = item.ContentKey.ToString(), ContentTypeId = this.ItemContentType.ContentTypeId, ModuleID = item.ModuleId };

            newContentItem.Content = item.ItemDescription; // This could be empty... Or it could be your item serialized and this is then used as the main content store or as the main content store for versioned items
            newContentItem.Metadata["Name"] = item.ItemName; // Doesn't need to be here
            newContentItem.Metadata["Published"] = Convert.ToString(false); // This could be a field on a separate data store

            this.contentController.AddContentItem(newContentItem);

            String currentWorkflow = workflowController.GetStateName(newContentItem);

             if (String.IsNullOrEmpty(currentWorkflow))
             {
                 int workflowId = -1;

                 if (moduleController.GetModule(item.ModuleId).ModuleSettings.Contains("Workflow"))
                     workflowId = int.Parse(moduleController.GetModule(item.ModuleId).ModuleSettings["Workflow"].ToString());

                 workflowController.StartWorkflow(newContentItem, workflowId);

             }

         }

        public void DeleteItem(int itemId)
        {


            var existingContentItem = contentController.GetContentItem(itemId);

            var newContentItem = new ContentItem { ContentKey = existingContentItem.ContentKey.ToString(), ContentTypeId = this.ItemContentType.ContentTypeId, ModuleID = existingContentItem.ModuleID };

            newContentItem.Metadata["Published"] = Convert.ToString(false);

            newContentItem.Metadata["PendingDelete"] = Convert.ToString(true);

            this.contentController.AddContentItem(newContentItem);

            String currentWorkflow = workflowController.GetStateName(newContentItem);

            if (String.IsNullOrEmpty(currentWorkflow))
            {
                int workflowId = -1;

                if (moduleController.GetModule(newContentItem.ModuleID).ModuleSettings.Contains("Workflow"))
                    workflowId = int.Parse(moduleController.GetModule(newContentItem.ModuleID).ModuleSettings["Workflow"].ToString());

                workflowController.StartWorkflow(newContentItem, workflowId);

            }


        }


         private static void FillContentItem(Item item, ContentItem contentItem, bool isNewItem)
         {
             
             if (isNewItem)
                contentItem.ContentKey = Guid.NewGuid().ToString(); // This could be a PK on a separate data store
             
             contentItem.Content = item.ItemDescription; // This could be empty... Or it could be your item serialized and this is then used as the main content store or as the main content store for versioned items

             contentItem.Metadata["Name"] = item.ItemName; // Doesn't need to be here
             contentItem.Metadata["Published"] = Convert.ToString(item.Published); // This could be a field on a separate data store

         }


    }
}
