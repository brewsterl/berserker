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
    /// This class is used for keeping track of whether certain actions are
    /// performed too quickly. This is used for things such as limiting walking
    /// speed, exhaustion on spells, etc.
    /// </summary>
    public class Elapser {
        private uint ticks;
        private DateTime dTime;

        /// <summary>
        /// Elapser constructor, used to initialize an Elapser object.
        /// </summary>
        /// <param name="timeInCS">The time interval to check.</param>
        public Elapser(uint timeInCS) {
            //60 000 000 = 60 seconds in ticks
            ticks = timeInCS * 100000;

            dTime = new DateTime(2005, 1, 1); //Arbitrary date set long ago
        }

        /// <summary>
        /// Set the time interval in centiseconds between each elapsed call
        /// that will return true.
        /// </summary>
        /// <param name="timeInCs">The time in centiseconds.</param>
        public void SetTimeInCS(uint timeInCS) {
            ticks = timeInCS * 100000;
        }

        /// <summary>
        /// Returns false if not enough time was given from the previous Elapsed call.
        /// Returns true if enough time was given and also resets the counter.
        /// </summary>
        /// <returns>True if enough time has passed and resets counter,
        /// false otherwise.</returns>
        public bool Elapsed() {
            double elapsedTicks = (DateTime.Now.Ticks - dTime.Ticks);
            if (elapsedTicks < ticks)
                return false;

            dTime = DateTime.Now;
            return true;
        }
    }
}
