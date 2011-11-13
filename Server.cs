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
using System.Net.Sockets;
using System.Net;
using System.Threading; // not used?

namespace Berserker
{
	class Server
	{
		static void Main(string[] args)
		{
			new Server().Run(args);
		}
		
		private GameWorld world;
		private TcpListener listener;
		
		// TODO: Move to enums.cs or constants.cs
		private const string InvalidNameOrPassword = "Invalid username or password.";
		
		public void Run(string[] args)
		{
			// TODO: Print version information
#if DEBUG
			// TODO: Print "DEBUGGING ON"-message
			Log.WriteDebug("Debugging on.");
#endif
			// Load configuration
			// Remove configuration asm/
			if (Directory.Exists("asm"))
				Directory.Delete("asm", true);
			
			string configFile = "config.cs";
			
			// Read config file in command line argument
			if (args.Length > 0)
			{
				if (!File.Exists(args[0]))
				{
					Console.WriteLine("Usage: mono Berserker.exe [config file]");
					Console.WriteLine();
					
					return;
				}
				else
				{
					configFile = args[0];
				}
			}
			
			if (!File.Exists(configFile))
			{
				// TODO: Create new template config file
				configFile = "config.cs";
			}
			
			Log.WriteBegin("Loading config file (" + configFile + ")...");
			
			try
			{
				DynamicCompile compiler = new DynamicCompile();
				Dictionary<string, string> values = (Dictionary<string, string>) compiler.Compile(configFile, null);
				
				Config.Load(values);
			}
			catch (Exception e)
			{
				Log.WriteError(e.ToString());
				
				return;
			}
			
			Log.WriteEnd();
			
			// TODO: Write load-message
			Log.WriteBegin("Loading items...");
			Item.LoadItems();
			Log.WriteEnd();
			
			// TODO: Write load-message
			Log.WriteBegin("Loading spells...");
			Spell.Load();
			Log.WriteEnd();
			
			// TODO: Write load-message
			Log.WriteBegin("Loading monsters...");
			Monster.Load();
			Log.WriteEnd();
			
			// TODO: Write load-message
			Log.WriteBegin("Loading commands...");
			Command.Load();
			Log.WriteEnd();
			
			// TODO: Write load-message
			Log.WriteBegin("Loading map...");
			Map map = Map.Load();
			world = new GameWorld(map);
			Log.WriteEnd();
			
			// TODO: Write load-message
			Log.WriteBegin("Loading non-person characters...");
			NPC.Load();
			List<NPC> allNPCs = NPC.GetAllNPCs();
			
			foreach (NPC npc in allNPCs)
				world.SendAddNPC(npc, npc.CurrentPosition);
			
			Log.WriteEnd();
			
			// TODO: Write load-message
			Log.WriteBegin("Loading monster spawns...");
			Respawn.Load(world);
			Log.WriteEnd();
			
			// TODO: Write load-message (listener)
			Log.WriteBegin("Starting TCP listener...");
			listener = new TcpListener(IPAddress.Any, Config.GetPort());
			listener.Start();
			Log.WriteEnd();
			
			Log.WriteLine("Server is now running.");
			// TODO: Write message: SERVER NOW RUNNING
			
			AcceptConnections();
		}
		
        /// <summary>
        /// Accept incoming connection requests.
        /// </summary>
		private void AcceptConnections()
		{
			// TODO: Remove!
			Player player = new Player(null);
			player.Name = "Ivan";
			player.Password = Hash.GetSha256("Ivan");
			player.CurrentPosition = new Position(32369, 32241, 7);
            player.MaxCapacity = 420; //TODO: Verify if correct
            player.MagicLevel = 1;
            player.MaxHP = 150;
            player.CurrentHP = 150;
            player.MaxMana = 0;
            player.CurrentMana = 0;
            player.CurrentVocation = Vocation.NONE;
            player.BaseSpeed = 220;
            player.SavePlayer();
			
			while (true)
			{
#if DEBUG
				// TODO: Write message here ("accept connections")
#endif
				HandleConnection();
			}
		}
		
		private void HandleConnection()
		{
			Socket socket = listener.AcceptSocket();
			
			//HandlePlayerConnection(socket, new ProtocolReceive65(socket), new ProtocolSend65(socket)); //old
			HandlePlayerConnection(socket, new ProtocolReceive(socket), new ProtocolSend(socket));
		}
		
		private void HandlePlayerConnection(Socket socket, ProtocolReceive protocolReceive, ProtocolSend protocolSend)
		{
			LoginInfo playerLogin = protocolReceive.HandlePlayerLogin(socket);
			GameWorld localWorld = world;		
#if DEBUG
			// TODO: Write message here ("Logging in (name/password-hash)...")
#endif
			// TODO: Kick current player and let this one log in
			if (localWorld.IsPlayerOnline(playerLogin.GetUsername()))
			{
				protocolSend.Reset();
				protocolSend.AddSorryBox("A player with this name is already online.");
				protocolSend.MarkSocketAsClosed();
				protocolSend.WriteToSocket();
#if DEBUG				
				// TODO: Write message here ("Player is already online")
#endif
				return;
			}
			
			Player player = new Player(protocolSend);
			
			if (!player.LoadPlayer(playerLogin))
			{
				protocolSend.Reset();
				protocolSend.AddSorryBox("A player with this name is already online.");
				protocolSend.MarkSocketAsClosed();
				protocolSend.WriteToSocket();
#if DEBUG				
				// TODO: Write message here ("Invalid username or password.")
#endif
				return;
			}
			
			localWorld.SendAddPlayer(player, player.CurrentPosition);
			protocolReceive.StartReceiving(world, player);
#if DEBUG
			// TODO: Write message here ("Logged in (name)")
#endif
			// TODO?: Threading
			//new PlayerThread(player, localWorld, protoReceive).StartThread();
		}
	}
}
