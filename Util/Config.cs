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
    /// <summary>
    /// This is the configuration class. It holds
    /// all of the server's settings.
    /// </summary>
    public static class Config {
        private static int port;
        private static string welcomeMsg;
        private static string path;
        private static string motd;
        private static ushort motdNumber;
        private static string DBUsername;
        private static string DBPassword;
        private static string DBHost;
        private static string DBPort;
        private static string DBName;
        private static string mapName;
        //private static bool accountManagerEnabled;	TODO: REMOVE

        /// <summary>
        /// Initialize the configuration.
        /// </summary>
        public static void Load(Dictionary<string, string> parser) {
            port = int.Parse(parser["port"]);
            welcomeMsg = parser["welcomeMsg"];
            path = parser["path"] + System.IO.Path.DirectorySeparatorChar;
            motd = parser["motd"];
            motdNumber = ushort.Parse(parser["motdNumber"]);
            DBUsername = parser["DBUsername"];
            DBPassword = parser["DBPassword"];
            DBHost = parser["DBHost"];
            DBPort = parser["DBPort"];
            DBName = parser["DBName"];
            mapName = parser["mapName"];
            //accountManagerEnabled = bool.Parse(parser["accountManagerEnabled"]);		TODO: REMOVE
        }

        /// <summary>
        /// Get the port that the server listens on.
        /// </summary>
        /// <returns>The port that the server listens on.</returns>
        public static int GetPort() {
            return port;
        }

        /// <summary>
        /// Gets the welcome message.
        /// </summary>
        /// <returns>The welcome message.</returns>
        public static string GetWelcomeMessage() {
            return welcomeMsg;
        }

        /// <summary>
        /// Gets the path to the data folder.
        /// </summary>
        /// <returns>The path to the data folder.</returns>
        public static string GetDataPath() {
            return path;
        }

        /// <summary>
        /// Gets the current message of the day.
        /// </summary>
        /// <returns>The message of the dya.</returns>
        public static string GetMOTD() {
            return motd;
        }

        /// <summary>
        /// Gets the current MOTD number.
        /// </summary>
        /// <returns>The message of the day number</returns>
        public static ushort GetMessageNumber() {
            return motdNumber;
        }

        /// <summary>
        /// Gets the database username.
        /// </summary>
        /// <returns>Database username.</returns>
        public static string GetDBUsername() {
            return DBUsername;
        }

        /// <summary>
        /// Gets the database password.
        /// </summary>
        /// <returns>Database password.</returns>
        public static string GetDBPassword() {
            return DBPassword;
        }

        /// <summary>
        /// Gets the IP of the database host.
        /// </summary>
        /// <returns>IP of database host.</returns>
        public static string GetDBHost() {
            return DBHost;
        }

        /// <summary>
        /// Gets the database port.
        /// </summary>
        /// <returns>Database port.</returns>
        public static string GetDBPort() {
            return DBPort;
        }

        /// <summary>
        /// Gets the name of the database.
        /// </summary>
        /// <returns>Database name.</returns>
        public static string GetDBName() {
            return DBName;
        }

        /// <summary>
        /// Gets the map name to be used.
        /// </summary>
        /// <returns>Map name to be used.</returns>
        public static string GetMapName() {
            return mapName;
        }

        // TODO: REMOVE
		/*/// <summary>
        /// Gets whether the account manager is enabled.
        /// </summary>
        /// <returns>True if the account manager is enabled, false otherwise.</returns>
        public static bool IsAccountManageEnabled() {
            return accountManagerEnabled;
        }*/

        /// <summary>
        /// Gets the local spell directory.
        /// </summary>
        /// <returns>Local spell directory.</returns>
        public static string GetSpellDirectory() {
            return "spells" + System.IO.Path.DirectorySeparatorChar;
        }

        /// <summary>
        /// Gets the local monster directory.
        /// </summary>
        /// <returns>Local monster directory.</returns>
        public static string GetMonsterDirectory() {
            return "monsters" + System.IO.Path.DirectorySeparatorChar;
        }

        /// <summary>
        /// Gets the local NPC directory.
        /// </summary>
        /// <returns>Local NPC directory</returns>
        public static string GetNPCDirectory() {
            return "npcs" + System.IO.Path.DirectorySeparatorChar;
        }

        /// <summary>
        /// Gets the local command directory.
        /// </summary>
        /// <returns>Local command directory.</returns>
        public static string GetCommandDirectory() {
            return "commands" + System.IO.Path.DirectorySeparatorChar;
        }
		
        /// <summary>
        /// Gets the local world directory.
        /// </summary>
        /// <returns>Local world directory.</returns>
        public static string GetWorldDirectory() {
            return "world" + System.IO.Path.DirectorySeparatorChar;
        }

        public static string GetItemDirectory() {
            return "items" + System.IO.Path.DirectorySeparatorChar;
        }

        public static string GetItemActionWalkDir() {
            return "walk" + System.IO.Path.DirectorySeparatorChar;
        }

        public static string GetItemActionUseDir() {
            return "use" + System.IO.Path.DirectorySeparatorChar;
        }

        public static string GetItemActionUseWithDir() {
            return "usewith" + System.IO.Path.DirectorySeparatorChar;
        }
		
		public static string GetDatabaseName()
		{
			return "players.db";
		}
		
		public static string GetDatabaseSchemaName()
		{
			return "players.sql";
		}
		
		// TODO: REMOVE
        /*public static Position GetActManagerTemple() {
            return new Position(32097, 32219, 7);
        }*/

        public static string GetWorldName() {
            return "Aperia";
        }
        public static byte GetXPRate() {
            return 100;
        }
        public static byte GetManaRate() {
            return 100;
        }

        public static byte GetSkillRate() {
            return 100;
        }
    }
}
