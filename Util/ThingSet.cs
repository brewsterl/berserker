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
    /// A basic wrapper class used to avoid redundent code.
    /// </summary>
    public class ThingSet {
        //Stores things
        private HashSet<Thing> things;
 
        /// <summary>
        /// Default constructor, used to initalize a ThingSet.
        /// </summary>
        public ThingSet() {
            things = new HashSet<Thing>();
        }

        /// <summary>
        /// Gets all the things in this thing set.
        /// </summary>
        /// <returns>Things in this set</returns>
        public HashSet<Thing> GetThings() {
            return things;
        }

        /// <summary>
        /// Add a thing to this set.
        /// </summary>
        /// <param name="thing"></param>
        public void AddThing(Thing thing) {
            things.Add(thing);
        }

        /// <summary>
        /// Gets whether this set contains the specified thing.
        /// Note: Uses reference equality.
        /// </summary>
        /// <param name="thing">The thing to check.</param>
        /// <returns>True if this set contains the specified thing,
        /// false otherwise</returns>
        public bool ContainsThing(Thing thing) {
            return things.Contains(thing);
        }
    }
}
