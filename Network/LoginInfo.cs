// Berserker: Multi-platform open source game server.
// Copyright (C) 2011 Berserker Group
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Berserker {
    /*
     * This is the class the holds the login info (username, password)
     * of a player attemping to connect to the server. Note: The 
     * username and password do not have to be valid.
     */

    /// <summary>
    /// This is the class that holds the login information 
    /// of a player attempting to connect to the server. Note: The username
    /// and password are the ones being attemped, so they could be invalid.
    /// </summary>
    public class LoginInfo {
        private string name;
        private string pw;

        /// <summary>
        /// LoginInfo constructor.
        /// </summary>
        /// <param name="username">The username of this instance.</param>
        /// <param name="password">The password of this instance.</param>
        public LoginInfo(string username, string password) {
            name = username;
            //pw = password;
			pw = Hash.GetSha256(password).ToLower(); // TODO: MOVE?
        }

        /// <summary>
        /// Get the password assigned to this instance.
        /// </summary>
        /// <returns>The password.</returns>
        public string GetPassword() {
            return pw;
        }

        /// <summary>
        /// Get the username of this instance.
        /// </summary>
        /// <returns>The username.</returns>
        public string GetUsername() {
            return name;
        }
    }
}
