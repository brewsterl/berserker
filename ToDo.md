# Common #
  * Memory profiling. Drastically reduced memory usage.
  * Move most files from the Game and Util directories to the root directory.
  * Create namespaces for the classes still in those directories.
  * Replace most entries in Constants.cs with entries in Enums.cs.
  * Optimize AddScreenMoveByOne().

# Database #
  * Port the database handling code from SharpOT and remove the existing (bad) database code.

# DynamicCompile #
  * ~~Complete rewrite (rename to DynamicCompiler or similar)~~.
  * Keep all compiled assemblies in one folder instead of a thousand (./data/asm?)
  * Add an option to not save the compiled assembly (for the configuration file).
  * Save checksums after compilation. Recompile if the source file has changed.

# Items #
  * Replace items.bin (items.dat) with items.xml. Easier for editing (since the current list is incorrect).
  * Create an item ID conversion list, mapping Tibia 7.x item ID's to Tibia 6.4 item ID's.
  * Separate the Item class to three classes (Item, ItemInfo, ItemAction).
  * Item: Representation of an actual existing item in the game.
  * ItemInfo: Information about all items in the game.
  * ItemAction: Item scripting.

# Map #
  * New map file format. Use DynamicCompiler?
  * Create converter from OpenTibia OTBM to the new format.

# Player #
  * Reduce size of Player class. Move methods to some other class?

# Scripts #
  * Rope.
  * Shovel.

# Spells #
  * Separate Spell class to four classes (SpellInfo, SpellInstant, SpellMonster, SpellRune).
  * SpellInfo: Information about all spells in the game.
  * SpellInstant: Instant-cast spells.
  * SpellMonster: Spells cast by monsters.
  * SpellRune: Rune spells.