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
using System.Text.RegularExpressions;

//TODO: Finish this crap
namespace Berserker {
    public class ItemConversion {
        public static Dictionary<int, int> GetConversionDictionary() {
            string directory = System.Environment.CurrentDirectory;
            string path = directory + "/data/items/item_conversion.txt";
            if (!File.Exists(path)) {
                Console.WriteLine("Error: file " + path + " not found");
                return null;
            }

            Dictionary<int, int> _64TO71 = new Dictionary<int, int>();

            TextReader tr = new StreamReader(path);

            string val = tr.ReadLine();
            while (val != "-1") {
                if (val == "") {
                } else {
                    string[] ids = Regex.Split(val, "\\s+");
                    int key = int.Parse(ids[0]);
                    int value = int.Parse(ids[1]);
                    if (_64TO71.ContainsKey(key)) {
                    } else {
                        _64TO71.Add(key, value);
                    }
                }
                val = tr.ReadLine();
            }
            tr.Close();
            return _64TO71;
        }
    }
}
