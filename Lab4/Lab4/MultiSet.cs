using System.Collections.Generic;
using System;

namespace Lab4
{
    public sealed class MultiSet
    {
        List<string> Elements = new List<string>();
        Dictionary<string, int> ElementsMultifier = new Dictionary<string, int>();

        public int CheckLexicographicOrder(string lhstring, string rhstring)
        {
            // return -1 if lefthand is bigger, return 0 if same, return 1 if righthand is bigger
            int i = 0;
            int lhStringLength = lhstring.Length;
            int rhStringLength = rhstring.Length;

            while (i < lhStringLength && i < rhStringLength)
            {
                if (lhstring[i] != rhstring[i])
                {
                    return (int)lhstring[i] > (int)rhstring[i] ? -1 : 1;
                }
                i++;
            }
            if (lhStringLength == rhStringLength)
            {
                return 0;
            }
            return lhStringLength > rhStringLength ? -1 : 1;
        }

        public void Add(string element)
        {
            int elementsListLength = Elements.Count;
            bool bIsAdded = false;
            
            for (int i = 0; i < elementsListLength; i++) // if string is null, int < null not possible?
            {
                int isBig = CheckLexicographicOrder(Elements[i], element);
                if (isBig == -1 || isBig == 0)
                {
                    Elements.Insert(i, element);
                    bIsAdded = true;
                    break;
                }
            }

            if (bIsAdded == false)
            {
                Elements.Add(element);
            }

            if (ElementsMultifier.ContainsKey(element))
            {
                int value;
                ElementsMultifier.TryGetValue(element, out value);
                ElementsMultifier[element] = ++value;
            }
            else
            {
                ElementsMultifier.Add(element, 1);
            }            
        }

        public bool Remove(string element)
        {
            if (Elements.Remove(element))
            {
                int value;
                ElementsMultifier.TryGetValue(element, out value);

                if (value > 1)
                {
                    ElementsMultifier[element] = --value;
                }
                else
                {
                    ElementsMultifier.Remove(element);
                }
                return true;
            }
            return false;
        }

        public uint GetMultiplicity(string element)
        {
            int value;
            if (ElementsMultifier.TryGetValue(element, out value))
            {
                return (uint)value;
            }
            return 0;
        }

        public List<string> ToList()
        {
            return Elements;
        }

        public MultiSet Union(MultiSet other)
        {
            MultiSet union = new MultiSet();
            for (int i = 0; i < Elements.Count; i++)
            {
                union.Add(Elements[i]);
            }
            for (int i = 0; i < other.Elements.Count; i++)
            {
                union.Add(other.Elements[i]);
            }
            MultiSet intersect = Intersect(other);

            for (int i = 0; i < intersect.Elements.Count; i++)
            {
                union.Remove(intersect.Elements[i]);
            }
            return union;
        }

        public MultiSet Intersect(MultiSet other)
        {
            MultiSet intersect = new MultiSet();

            for (int i = 0; i < Elements.Count; i++)
            {
                if (other.Elements.Contains(Elements[i]))
                {
                    if (i > 0 && Elements[i] == Elements[i - 1])
                    {                       
                        continue;
                    }
                    int value1;
                    int value2;
                    ElementsMultifier.TryGetValue(Elements[i], out value1);
                    other.ElementsMultifier.TryGetValue(Elements[i], out value2);

                    int value3 = value2 > value1 ? value1 : value2;

                    for (int j = 0; j < value3; j++)
                    {
                        intersect.Add(Elements[i]);
                    }
                }
            }
            return intersect; 
        }

        public MultiSet Subtract(MultiSet other)
        {
            MultiSet subtract = new MultiSet();
            subtract.Elements = Elements;
            subtract.ElementsMultifier = ElementsMultifier;

            MultiSet intersect = Intersect(other);
            for (int i = 0; i < intersect.Elements.Count; i++)
            {
                subtract.Remove(intersect.Elements[i]);
            }

            return subtract;
        }

        public List<MultiSet> FindPowerSet()
        {
            int n = Elements.Count;
            int powerSetCount = 1 << n;
            List<MultiSet> result = new List<MultiSet>(powerSetCount);

            for (int setMask = 0; setMask < powerSetCount; setMask++)
            {
                MultiSet set = new MultiSet();
                for (int i = 0; i < n; i++)
                {
                    if ((setMask & (1 << i)) > 0)
                    {
                        set.Add(Elements[i]);
                    }
                }
                bool bFlag = true;

                for (int i = 0; i < result.Count; i++)
                {
                    if (set.Elements.Count == result[i].Elements.Count)
                    {
                        for (int j = 0; j < result[i].Elements.Count; j++)
                        {
                            if (set.Elements[j] != result[i].Elements[j])
                            {
                                bFlag = true;
                                break;
                            }
                            if (j == result[i].Elements.Count - 1)
                            {
                                bFlag = false;
                            }
                        }
                    }
                }
                if (bFlag || result.Count == 0)
                {
                    for (int result_index = 0; result_index < result.Count; result_index++)
                    {
                        for (int element_index = 0; element_index < result[result_index].Elements.Count; element_index++)
                        {
                            int dicOrder = CheckLexicographicOrder(result[result_index].Elements[element_index], set.Elements[element_index]);

                            if (dicOrder == -1)
                            {
                                result.Insert(result_index, set);
                                goto end;
                            }
                        }
                    }
                    result.Add(set);
                }
            end:;

            }
            return result;
        }

        public bool IsSubsetOf(MultiSet other)
        {
            foreach (var elements in ElementsMultifier)
            {
                int otherElementsCount;
                if (other.ElementsMultifier.TryGetValue(elements.Key, out otherElementsCount))
                {
                    if (elements.Value < otherElementsCount)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsSupersetOf(MultiSet other)
        {
            foreach (var other_elements in other.ElementsMultifier)
            {
                int elementsCount;
                if (ElementsMultifier.TryGetValue(other_elements.Key, out elementsCount))
                {
                    if (other_elements.Value > elementsCount)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
