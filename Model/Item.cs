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

namespace MikeSmeltzer.Modules.WorkflowExample.Components
{

    public class Item
    {

        public Item()
        {


        }

        public Item(String contentKey, int itemId, string itemName, string itemDescription)
        {

            this.ContentKey = Guid.Parse(contentKey);
            this.ContentItemId = itemId;
            this.ItemName = itemName;
            this.ItemDescription = itemDescription;
            this.Published = false;

        }

        public Item(String contentKey, int itemId, string itemName, string itemDescription, bool isPublished)
        {

            this.ContentKey = Guid.Parse(contentKey);
            this.ContentItemId = itemId;
            this.ItemName = itemName;
            this.ItemDescription = itemDescription;
            this.Published = isPublished;

        }


        public Guid ContentKey { get; set; }

        ///<summary>
        /// The ID of your object with the name of the ItemName
        ///</summary>
        public int ContentItemId { get; set; }

        

        ///<summary>
        /// A string with the name of the ItemName
        ///</summary>
        public string ItemName { get; set; }

        ///<summary>
        /// A string with the description of the object
        ///</summary>
        public string ItemDescription { get; set; }

        public bool Published { get; set; }


        ///<summary>
        /// The ModuleId of where the object was created and gets displayed
        ///</summary>
        public int ModuleId { get; set; }

        ///<summary>
        /// An integer for the user id of the user who created the object
        ///</summary>
        public int CreatedByUserId { get; set; }

        ///<summary>
        /// An integer for the user id of the user who last updated the object
        ///</summary>
        public int LastModifiedByUserId { get; set; }

        ///<summary>
        /// The date the object was created
        ///</summary>
        public DateTime CreatedOnDate { get; set; }

        ///<summary>
        /// The date the object was updated
        ///</summary>
        public DateTime LastModifiedOnDate { get; set; }
    }
}
