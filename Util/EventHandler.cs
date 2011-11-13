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
using System.Text;
using System.Threading;

namespace Berserker {
    public delegate void eventDelegate();
    public delegate void creatureAttackDelegate(Creature creature);

    //Credits to fandoras for most of this code
    public class Event {
        public eventDelegate eventDelegate;

        public Event() {
        }
    }

    public class EventHandler {
        SortedDictionary<long, List<Event>> sdict;
        private object lockThis = new object();

        private void addEvent(long time, eventDelegate eventDel) {
            Event e = new Event();
            e.eventDelegate = eventDel;
            time = System.DateTime.Now.Ticks + time;

            time >>= 10;

            lock (sdict) {
                List<Event> elist;
                sdict.TryGetValue(time, out elist);
                /*try {
                    elist = sdict[time];
                } catch {}
                 */

                if (elist == null) {
                    elist = new List<Event>();
                    sdict.Add(time, elist);
                }

                elist.Add(e);
            }
        }

        public EventHandler() {
            sdict = new SortedDictionary<long, List<Event>>();
        }

        public GameWorld World {
            get;
            set;
        }

        public void addEventInCS(long time, eventDelegate eventDel) {
            time = time * 100000;
            addEvent(time, eventDel);
        }

        void Schedule() {
            while (true) {
                KeyValuePair<long, List<Event>> first;
                List<Event> elist = null;

                lock (sdict) {
                    SortedDictionary<long, List<Event>>.Enumerator e = sdict.GetEnumerator();
                    if (e.MoveNext()) {
                        first = e.Current;
                        if (first.Key <= (System.DateTime.Now.Ticks >> 10)) {
                            elist = first.Value;
                            sdict.Remove(first.Key);
                        }
                    }
                }

                if (elist == null) {
                    Thread.Sleep(10);
                    continue;
                }

                foreach (Event e in elist) {
                    World.InvokeEvent(e.eventDelegate);
                }
            }
        }

        public void Start() {
            //Create the thread and start it
            Thread eventThread = new Thread(new ThreadStart(Schedule));
            eventThread.Start();
        }
    }
}
