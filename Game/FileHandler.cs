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
using System.IO;

namespace Berserker {
    public class FileHandler {
        private string GetPath(string append) {
            string dataPath = Config.GetDataPath();
            return dataPath + append;
        }

        public FileHandler() {
        }

        public Map LoadMap(string mapPath) {
            // try {
            BinaryReader bReader = new BinaryReader(File.Open(mapPath, FileMode.Open));
            int sizex = bReader.ReadUInt16(); //Height
            int sizey = bReader.ReadUInt16(); //Width
            Map map = new Map(sizex, sizey);
            uint tileCount = bReader.ReadUInt32(); //Tile Count
            for (int i = 0; i < tileCount; i++) {
                ushort x = bReader.ReadUInt16(); //X position
                ushort y = bReader.ReadUInt16(); //Y position
                byte z = bReader.ReadByte(); //Z position
                ushort id = bReader.ReadUInt16(); //Tile ID

                map.SetTile(x, y, z, new Tile(Item.CreateItem(id)));
                byte itemCount = bReader.ReadByte();
                for (int j = 0; j < itemCount; j++) {
                    ushort itemID = bReader.ReadUInt16();
                    Item item = Item.CreateItem(itemID);
                    map.AddThing(item, new Position(x, y, z));
                }
            }

            bReader.Close();
                 //TODO: Remove this code
               map.SetTile(320, 320, 6, new Tile(Item.CreateItem("Grass")));
            return map;
            //} catch (Exception e) {
            //    lastError = e.ToString();
            //    return null;
            //}
        }

        public void SaveComment(string comment, Player player) {
            TextWriter tw = new StreamWriter(GetPath("comments.txt"), true);
            tw.WriteLine("********************************************");
            tw.WriteLine("Player name: " + player.Name);
            tw.WriteLine("Comment: " + comment);
            tw.WriteLine("********************************************");
            tw.WriteLine("");
            tw.Close();
        }
    }
}
