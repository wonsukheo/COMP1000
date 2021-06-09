using System.Collections.Generic;

namespace Lab6
{
    class Recyclebot
    {
        public List<Item> RecycleItems { get; private set; }
        public List<Item> NonRecycleItems { get; private set; }

        public Recyclebot()
        {
            RecycleItems = new List<Item>();
            NonRecycleItems = new List<Item>();
        }
        public void Add(Item item)
        {
            if (item.IsRecyclable() && (item.Weight >= 5.0 || item.Weight < 2.0))
            {
                NonRecycleItems.Add(item);
            }
            else
            {
                RecycleItems.Add(item);
            }
        }
        // Dump using foreach twice but simple
        public List<Item> Dump()
        {
            List<Item> notDumpItems = new List<Item>();
            
            foreach(Item item in NonRecycleItems)
            {
                if (item.Type != EType.Electronics && item.Type != EType.Furniture)
                {
                    if (item.IsToxicWaste)
                    {
                        notDumpItems.Add(item);
                    }
                    else
                    {
                        if (item.Volume == 11 || item.Volume == 10 || item.Volume == 15)
                        {
                            notDumpItems.Add(item);
                        }
                    }
                }
            }

            List<Item> dumpItems = NonRecycleItems;
            
            foreach(Item item in notDumpItems)
            {
                dumpItems.Remove(item);
            }

            return dumpItems;
        }
        // below is Dump() using foreach only once
        /*
        public List<Item> Dump()
        {
            List<Item> NotDumpItems = new List<Item>();
            List<Item> DumpItems = new List<Item>();

            foreach (Item item in NonRecycleItems)
            {
                if (item.IsToxicWaste)
                {
                    if (item.Volume != 10.0 && item.Volume != 11.0 && item.Volume != 15.0)
                    {
                        if (item.Type != EType.Electronics && item.Type != EType.Furniture)
                        {
                            NotDumpItems.Add(item);
                        }
                        else
                        {
                            DumpItems.Add(item);
                        }
                    }
                    else
                    {
                        if (item.Type != EType.Electronics && item.Type != EType.Furniture)
                        {
                            NotDumpItems.Add(item);
                        }
                        else
                        {
                            DumpItems.Add(item);
                        }
                    }
                }
                else
                {
                    if (item.Volume == 10.0 || item.Volume == 11.0 || item.Volume == 15.0)
                    {
                        if (item.Type != EType.Electronics && item.Type != EType.Furniture)
                        {
                            NotDumpItems.Add(item);
                        }
                        else
                        {
                            DumpItems.Add(item);
                        }
                    }
                    else
                    {
                        DumpItems.Add(item);
                    }
                }
            }
            return DumpItems;
        }
        */
    }
}
