// -----------------------------------------------------------------------
// <copyright file="NetHelpers.cs" company="-">
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

namespace Tasslehoff.Library.Helpers
{
    using System;

    /// <summary>
    /// NetUtils class.
    /// </summary>
    public static class NetHelpers
    {
        // methods

        /// <summary>
        /// Converts to IP range.
        /// </summary>
        /// <param name="ipAddress">The IP address</param>
        /// <returns>The IP range</returns>
        public static string ConvertToIPRange(string ipAddress)
        {
            string[] ipArray = ipAddress.Split('.');
            double ipRange = 0;

            for (int i = 0; i < 4; i++)
            {
                int numPosition = int.Parse(ipArray[3 - i].ToString());
                if (i == 4)
                {
                    ipRange += numPosition;
                }
                else
                {
                    ipRange += (numPosition % 256) * Math.Pow(256, i);
                }
            }

            return ipRange.ToString();
        }
    }
}
