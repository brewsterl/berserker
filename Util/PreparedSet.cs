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
    /// A basic wrapper used for checking which player
    /// had their Prepare() method called. Used mainly for keeping
    /// track of adding protocol messages before sending them.
    /// </summary>
    public class PreparedSet {
        //Stores players
        private HashSet<Player> players = new HashSet<Player>();

        public HashSet<Player> GetThings() {
            return players;
        }

        public void AddPlayer(Player playerToAdd) {
            if (!players.Contains(playerToAdd)) {
                playerToAdd.ResetNetMessages();
                players.Add(playerToAdd);
            }
        }

        public void Clear() {
            players.Clear();
        }
    }
}
