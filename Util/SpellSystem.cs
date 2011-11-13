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
    /// This class handles casting spells.
    /// </summary>
    public class SpellSystem {
        Map map;

        /// <summary>
        /// Constructs this spell system with the
        /// specified game map.
        /// </summary>
        /// <param name="gameMap"></param>
        public SpellSystem(Map gameMap) {
            map = gameMap;
        }


        /// <summary>
        /// Use this method cast the specified spell. Note: This method only
        /// appends and does not send protocol data.
        /// </summary>
        /// <param name="caster">The creature casting the spell</param>
        /// <param name="spell">The spell to cast</param>
        /// <param name="tSet">The set of affected things</param>
        public void CastSpell(string msg, Creature caster, Spell spell, GameWorld world) {
            /*string error = caster.CanCastSpell(spell);
            if (error != null) {
                caster.AddAnonymousChat(ChatAnonymous.WHITE, error);
                return;
            }*/ //TODO: Uncomment

            if (spell.IsSpellValid != null && !spell.IsSpellValid(world, msg)) {
                world.AddMagicEffect(MagicEffect.PUFF, caster.CurrentPosition);
                return;
            }
            if (spell.RequiresTarget) {
                Tile tile = map.GetTile(spell.SpellCenter);
                if (tile == null || !tile.ContainsType(Constants.TYPE_CREATURE)) {
                    world.AddMagicEffect(MagicEffect.PUFF, caster.CurrentPosition);
                    caster.AddAnonymousChat(ChatAnonymous.WHITE, "No target selected.");
                    return;
                }
            }
            //Constants.
            //Not the most efficient method but it is simple and works.
            int length = spell.SpellArea.GetLength(0);
            int width = spell.SpellArea.GetLength(1);
             
            Position startPos = new Position();
            startPos.x = (ushort)(spell.SpellCenter.x - (width / 2));
            startPos.y = (ushort)(spell.SpellCenter.y - (length / 2));
            startPos.z = spell.SpellCenter.z;
            Position local = new Position();

            List<Thing> things = new List<Thing>();
            for (int i = 0; i < length; i++) {
                for (int j = 0; j < width; j++) {
                    local.x = (ushort)(startPos.x + j);
                    local.y = (ushort)(startPos.y + i);
                    local.z = startPos.z;
                    if (map.GetTile(local) == null 
                        /*|| !map.GetTile(local).CanMoveTo(caster)
                         * TODO: Finish*/) {
                        continue;
                    }

                    if (spell.SpellArea[i, j] && 
                        !map.GetTile(local).ContainsType(Constants.TYPE_BLOCKS_MAGIC)) {
                        ThingSet tSet = map.GetThingsInVicinity(local);
                        foreach (Thing thing in tSet.GetThings()) {
                            thing.AddEffect(spell.SpellEffect, local);
                            if (spell.HasDistanceType()) {
                                thing.AddShootEffect((byte)spell.DistanceEffect,
                                    caster.CurrentPosition, spell.SpellCenter);
                            }
                        }

                        List<Thing> localThings = map.GetTile(local).GetThings();
                        
                        if (spell.Action != null) {
                            spell.Action.Invoke(world, local, localThings); 
                        }

                        foreach (Thing thing in map.GetTile(local).GetThings()) {
                            things.Add(thing);
                        }
                    }
                }
            }

            foreach (Thing thing in things) {
                thing.AppendHandleDamage(spell.GetDamage(), caster, spell.Immunity, world, true);
            }

            //caster.NotifyOfSuccessfulCast(spell); TODO: Uncomment
        }
    }
}
