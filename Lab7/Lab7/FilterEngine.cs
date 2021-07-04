using System;
using System.Collections.Generic;
using System.Text;

namespace Lab7
{
    public static class FilterEngine
    {
        public static List<Frame> FilterFrames(List<Frame> frames, EFeatureFlags features)
        {
            List<Frame> filteredList = new List<Frame>();
            
            foreach (Frame f in frames)
            {
                EFeatureFlags temp = f.Features & features;
  
                if ((temp & features) != 0)
                {
                    filteredList.Add(f);
                }
            }

            return filteredList;
        }

        public static List<Frame> FilterOutFrames(List<Frame> frames, EFeatureFlags features)
        {
            List<Frame> filteredList = new List<Frame>();

            foreach (Frame f in frames)
            {
                EFeatureFlags temp = f.Features ^ features;

                if (f.Features == (f.Features & temp))
                {
                    filteredList.Add(f);
                }
            }

            return filteredList;
        }

        public static List<Frame> Intersect(List<Frame> frames1, List<Frame> frames2)
        {
            List<Frame> filteredList = new List<Frame>();

            foreach (Frame f1 in frames1)
            {
                foreach (Frame f2 in frames2)
                {
                    if (f1.ID == f2.ID)
                    {
                        filteredList.Add(f1);
                    }
                }
            }
            return filteredList;
        }

        public static List<int> GetSortKeys(List<Frame> frames, List<EFeatureFlags> features)
        {
            List<int> filteredList = new List<int>();

            foreach (Frame f in frames)
            {
                int score = 0;
                int multiple = 1 << features.Count;

                foreach (EFeatureFlags flag in features)
                {                   
                    EFeatureFlags temp = f.Features & flag;

                    if (temp == flag)
                    {
                        score += multiple;
                    }
                    multiple >>= 1;
                }

                filteredList.Add(score);
            }

            return filteredList;
        }
    }
}
