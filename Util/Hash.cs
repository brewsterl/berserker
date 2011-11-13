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
using System.Security.Cryptography;

namespace Berserker
{
	public static class Hash
	{
		public static string GetMd5(string data)
		{
			byte[] input = ASCIIEncoding.ASCII.GetBytes(data);
			byte[] output = MD5.Create().ComputeHash(input);
			
			StringBuilder sb = new StringBuilder();
			
			for (int i = 0; i < output.Length; i++)
				sb.Append(output[i].ToString("X2"));
			
			return sb.ToString();
		}
		
		private static SHA256 sha = new SHA256Managed();
		
        public static string GetSha256(string data)
        {
            byte[] hash = sha.ComputeHash(Encoding.Unicode.GetBytes(data));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
	}
}

