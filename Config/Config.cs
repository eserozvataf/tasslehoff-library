﻿// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="-">
// Copyright (c) 2014 Eser Ozvataf (eser@sent.com). All rights reserved.
// Web: http://eser.ozvataf.com/ GitHub: http://github.com/larukedi
// </copyright>
// <author>Eser Ozvataf (eser@sent.com)</author>
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

namespace Tasslehoff.Library.Config
{
    using System;
    using System.IO;
    using System.Reflection;
    using Tasslehoff.Library.Helpers;

    /// <summary>
    /// Config class
    /// </summary>
    public class Config
    {
        // constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Config"/> class.
        /// </summary>
        protected Config()
        {
            this.Reset(true);
        }

        // methods

        /// <summary>
        /// Serializes a configuration object into a string
        /// </summary>
        /// <returns>Serialized config object</returns>
        public virtual string Dump()
        {
            return SerializationHelpers.JsonSerialize(this);
        }

        /// <summary>
        /// Serializes a config object into a file
        /// </summary>
        /// <param name="path">The path serialized object will be written</param>
        public void SaveToFile(string path)
        {
            File.WriteAllText(path, this.Dump());
        }

        /// <summary>
        /// Resets the specified config object.
        /// </summary>
        /// <param name="isFirstInit">Whether it is called during initialization or not</param>
        protected void Reset(bool isFirstInit)
        {
            Type type = this.GetType();

            foreach (PropertyInfo property in type.GetProperties())
            {
                object value = null;
                ConfigEntryAttribute[] attributes = (ConfigEntryAttribute[])property.GetCustomAttributes(typeof(ConfigEntryAttribute), true);

                if (attributes.Length > 0)
                {
                    if (!isFirstInit && attributes[0].SkipInReset)
                    {
                        continue;
                    }

                    value = attributes[0].DefaultValue;
                }

                property.SetValue(this, value, null);
            }
        }

        /// <summary>
        /// Resets the specified config object.
        /// </summary>
        public virtual void Reset()
        {
            this.Reset(false);
        }
    }
}
