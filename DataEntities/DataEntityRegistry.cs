// -----------------------------------------------------------------------
// <copyright file="DataEntityRegistry.cs" company="-">
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

namespace Tasslehoff.Library.DataEntities
{
    using System;
    using System.Runtime.InteropServices;
    using Tasslehoff.Library.Collections;

    /// <summary>
    /// DataEntityRegistry class.
    /// </summary>
    [ComVisible(false)]
    public class DataEntityRegistry : DictionaryBase<Type, IDataEntityMap>
    {
        // methods

        /// <summary>
        /// Registers a data entity class.
        /// </summary>
        /// <typeparam name="T">IDataEntity implementation.</typeparam>
        public void Register<T>() where T : IDataEntity, new()
        {
            this.Add(typeof(T), new DataEntityMap<T>());
        }
    }
}
