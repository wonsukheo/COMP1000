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
            if(item.isRecyclable() && (item.Weight >= 5.0 || item.Weight < 2.0))
            {
                NonRecycleItems.Add(item);
            }
            else
            {
                RecycleItems.Add(item);
            }
        }
        /*
        public List<Item> Dump()
        {
            List<Item> DumpItems = new List<Item>();
            
            foreach(Item item in NonRecycleItems)
            {
                if (item.IsToxicWaste)
                {
                    if (item.Type != EType.Furniture && item.Type != EType.Electronics)
                    {
                        continue;
                    }
                    else
                    {
                        DumpItems.Add(item);
                        continue;
                    }
                }
                DumpItems.Add(item);
            }

            return DumpItems;
        }*/

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
    }
}
