//   Node.java
//   Java Spatial Index Library
//   Copyright (C) 2002 Infomatiq Limited
//  
//  This library is free software; you can redistribute it and/or
//  modify it under the terms of the GNU Lesser General Public
//  License as published by the Free Software Foundation; either
//  version 2.1 of the License, or (at your option) any later version.
//  
//  This library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//  Lesser General Public License for more details.
//  
//  You should have received a copy of the GNU Lesser General Public
//  License along with this library; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

// Ported to C# By Dror Gluska, April 9th, 2009
using System.Numerics;
using System.Collections.Generic;

namespace RUMTree
{

    //import com.infomatiq.jsi.Rectangle;

    /**
     * <p>Used by RTree. There are no public methods in this class.</p>
     * 
     * @author aled@sourceforge.net
     * @version 1.0b2p1
     */
    public class Node<T>
    {


        internal int nodeId = 0;
        internal Rectangle mbr = null;
        internal Rectangle[] entries = null;
        internal int[] ids = null;
        //RUM tree 
        internal BigInteger[] stamps = null;
        private Dictionary<int, UpdatedMemo> OIDsToUM;
        private Dictionary<int, T> IdsToItems;
        //RUM tree end
        internal int level;
        internal int entryCount;

        public Node(int nodeId, int level, int maxNodeEntries, Dictionary<int, UpdatedMemo> OIDsToUM, Dictionary<int, T> IdsToItems)
        {
            this.IdsToItems = IdsToItems;
            this.OIDsToUM = OIDsToUM;
            this.nodeId = nodeId;
            this.level = level;
            entries = new Rectangle[maxNodeEntries];
            ids = new int[maxNodeEntries];
            stamps = new BigInteger[maxNodeEntries];
        }

        public int collectGarbage(int minNodeEntries)
        {
            int idx=0;
            int oldentryCount = entryCount;
            while( idx < entryCount)
            {

                if (OIDsToUM.ContainsKey(ids[idx])
                    && stamps[idx] != -1 && OIDsToUM[ids[idx]].latestStamp > stamps[idx])
                {
                    //clear entry,resize
                    int idToDel = ids[idx]; ids[idx] = ids[entryCount - 1]; 
                    OIDsToUM[idToDel].decreaseObsoleteEntryCnt(); 
                    if (OIDsToUM[idToDel].ObsoleteEntryCnt == 0) OIDsToUM.Remove(idToDel);//in case this is the only obsolete node
                    IdsToItems.Remove(idToDel);
                    entries[idx] = entries[entryCount - 1]; entries[entryCount - 1] = null;
                    BigInteger stamptmp = stamps[idx]; stamps[idx] = stamps[entryCount - 1]; stamps[entryCount - 1] = stamptmp;
                    entryCount--;
                }
                else
                {
                    idx++;
                }
            }
            //recalculate mbr if necessary
            if (entryCount >= minNodeEntries)
            {
                mbr.set(entries[0].min, entries[0].max);

                for (int i = 1; i < entryCount; i++)
                {
                    mbr.add(entries[i]);
                }
            }
            return oldentryCount - entryCount;
            
        }

        internal void addEntry(Rectangle r, int id, BigInteger stamp)
        {
            ids[entryCount] = id;
            entries[entryCount] = r.copy();
            stamps[entryCount] = stamp;
            entryCount++;
            if (mbr == null)
            {
                mbr = r.copy();
            }
            else
            {
                mbr.add(r);
            }
        }

        internal void addEntryNoCopy(Rectangle r, int id, BigInteger stamp )
        {
            ids[entryCount] = id;
            entries[entryCount] = r;
            stamps[entryCount] = stamp;
            entryCount++;
            if (mbr == null)
            {
                mbr = r.copy();
            }
            else
            {
                mbr.add(r);
            }
        }

        // Return the index of the found entry, or -1 if not found
        internal int findEntry(Rectangle r, int id)
        {
            for (int i = 0; i < entryCount; i++)
            {
                if (id == ids[i] && r.Equals(entries[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        // delete entry. This is done by setting it to null and copying the last entry into its space.
        internal void deleteEntry(int i, int minNodeEntries)
        {
            int lastIndex = entryCount - 1;
            Rectangle deletedRectangle = entries[i];
            entries[i] = null;
            if (i != lastIndex)
            {
                entries[i] = entries[lastIndex];
                ids[i] = ids[lastIndex];
                stamps[i] = stamps[lastIndex];
                entries[lastIndex] = null;
            }
            entryCount--;

            // if there are at least minNodeEntries, adjust the MBR.
            // otherwise, don't bother, as the Node<T> will be 
            // eliminated anyway.
            if (entryCount >= minNodeEntries)
            {
                recalculateMBR(deletedRectangle);
            }
        }

        // oldRectangle is a rectangle that has just been deleted or made smaller.
        // Thus, the MBR is only recalculated if the OldRectangle influenced the old MBR
        internal void recalculateMBR(Rectangle deletedRectangle)
        {
            if (mbr.edgeOverlaps(deletedRectangle))
            {
                mbr.set(entries[0].min, entries[0].max);

                for (int i = 1; i < entryCount; i++)
                {
                    mbr.add(entries[i]);
                }
            }
        }

        public int getEntryCount()
        {
            return entryCount;
        }

        public Rectangle getEntry(int index)
        {
            if (index < entryCount)
            {
                return entries[index];
            }
            return null;
        }

        public int getId(int index)
        {
            if (index < entryCount)
            {
                return ids[index];
            }
            return -1;
        }

        /**
         * eliminate null entries, move all entries to the start of the source node
         */
        internal void reorganize(RUMTree<T> rtree)
        {
            int countdownIndex = rtree.maxNodeEntries - 1;
            for (int index = 0; index < entryCount; index++)
            {
                if (entries[index] == null)
                {
                    while (entries[countdownIndex] == null && countdownIndex > index)
                    {
                        countdownIndex--;
                    }
                    entries[index] = entries[countdownIndex];
                    ids[index] = ids[countdownIndex];
                    stamps[index] = stamps[countdownIndex];
                    entries[countdownIndex] = null;
                }
            }
        }

        internal bool isLeaf()
        {
            return (level == 1);
        }

        public int getLevel()
        {
            return level;
        }

        public Rectangle getMBR()
        {
            return mbr;
        }
    }

}