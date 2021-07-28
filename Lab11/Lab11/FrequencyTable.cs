using System;
using System.Collections.Generic;

namespace Lab11
{
    public static class FrequencyTable
    {
        public static List<Tuple<Tuple<int, int>, int>> GetFrequencyTable(int[] data, int maxBinCount)
        {
            int[] sortedData = new int[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                sortedData[i] = data[i];
            }
            Sort(sortedData);

            int max = sortedData[data.Length - 1];
            int min = sortedData[0];

            int binWidth = (int)Math.Ceiling((double)(max - min) / maxBinCount);
            if (binWidth < 1)
            {
                binWidth = 1;
            }
            
            int binCount = ((max - min) / binWidth) + 1;
            
            if (binCount > maxBinCount)
            {
                binWidth++;
                binCount = ((max - min) / binWidth) + 1;
            }

            List<Tuple<Tuple<int, int>, int>> FrequencyTable = new List<Tuple<Tuple<int, int>, int>>();

            for (int i = 0; i < binCount; i++)
            {
                int dataCount = 0;
                for (int j = 0; j < sortedData.Length; j++)
                {
                    if (sortedData[j] >= min + binWidth * i && sortedData[j] < min + binWidth * (i + 1))
                    {
                        dataCount++;
                    }
                    else if (sortedData[j] > min + binWidth * (i + 1))
                    {
                        break;
                    }
                }

                if (sortedData[sortedData.Length - 1] < min + binWidth * i)
                {
                    break;
                } 

                FrequencyTable.Add(new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(min + binWidth * (i), min + binWidth * (i + 1)), dataCount));
            }

            return FrequencyTable;
        }
        public static int CompareInt(int a, int b)
        {
            if (a > b)
            {
                return -1;
            }
            else if (a < b)
            {
                return 1;
            }

            return 0;
        }

        public static int[] Sort(int[] data)
        {
   
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data.Length - 1 - i; j++)
                {
                    if (CompareInt(data[j], data[j + 1]) == -1)
                    {
                        int temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                        //switched = true;
                    }
                }                    
            }
            
            return data; 
        }
    }
}
