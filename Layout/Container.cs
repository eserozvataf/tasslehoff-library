﻿// -----------------------------------------------------------------------
// <copyright file="Container.cs" company="-">
// Copyright (c) 2013 larukedi (eser@sent.com). All rights reserved.
// </copyright>
// <author>larukedi (http://github.com/larukedi/)</author>
// -----------------------------------------------------------------------

//// This program is free software: you can redistribute it and/or modify
//// it under the terms of the GNU General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
//// 
//// This program is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU General Public License for more details.
////
//// You should have received a copy of the GNU General Public License
//// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace Tasslehoff.Library.Layout
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Web.UI.HtmlControls;
    using Tasslehoff.Library.Text;

    /// <summary>
    /// Container class.
    /// </summary>
    [Serializable]
    [DataContract]
    [LayoutProperties(Icon = "th-large")]
    public class Container : LayoutControl
    {
        // fields

        /// <summary>
        /// Tag name
        /// </summary>
        [DataMember(Name = "TagName")]
        private string tagName = "div";

        // properties
        
        /// <summary>
        /// Gets or sets tag name
        /// </summary>
        /// <value>
        /// Tag name
        /// </value>
        [IgnoreDataMember]
        public virtual string TagName
        {
            get
            {
                return this.tagName;
            }
            set
            {
                this.tagName = value;
            }
        }

        // methods

        /// <summary>
        /// Creates web control
        /// </summary>
        public override void CreateWebControl()
        {
            HtmlGenericControl element = new HtmlGenericControl(this.TagName);
            this.AddWebControlAttributes(element, element.Attributes);
            this.AddWebControlChildren(element);
            this.MakeWebControlAwareOf(element);

            this.WebControl = element;
        }

        /// <summary>
        /// Occurs when [export].
        /// </summary>
        /// <param name="jsonOutputWriter">Json Output Writer</param>
        public override void OnExport(MultiFormatOutputWriter jsonOutputWriter)
        {
            // base.OnExport(jsonOutputWriter);

            if (this.TagName != "div")
            {
                jsonOutputWriter.WriteProperty("TagName", this.TagName);
            }
        }

        /// <summary>
        /// Occurs when [export].
        /// </summary>
        /// <param name="jsonOutputWriter">Json Output Writer</param>
        public override void OnGetEditProperties(List<string> properties)
        {
            properties.Add("TagName");
        }
    }
}
