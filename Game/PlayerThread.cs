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

/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Berserker {
    public class PlayerThread {
        private Player player;
        private GameWorld world; //A reference to the game world
        private ProtocolReceive protoReceive;

        private void Run() {
            for (; ; ) {
               // try {
                    lock (this) { //TODO: Fix
                        bool proceed = false; // protoReceive.ProcessNextMessage(player, world);
                       if (!proceed) {
                           return;
                       }
                    }
               // } catch (Exception e) {
                //    Tracer.Print(e.ToString());
               //     return;
               // }
            }
        }

        public PlayerThread(Player nPlayer, GameWorld gameWorld,
            ProtocolReceive protocolReceive) {
            player = nPlayer;
            world = gameWorld;
            protoReceive = protocolReceive;
        }

        public void StartThread() {
            new Thread(Run).Start();
        }
    }
}
*/