using System.Collections.Generic;
using System;

namespace Lab4
{
    public sealed class MultiSet
    {
        List<string> elements = new List<string>();
        Dictionary<string, int> elements_multifier = new Dictionary<string, int>();

        public int CheckLexicographicOrder(string lhstring, string rhstring)
        {
            // return -1 if lefthand is bigger, return 0 if same, return 1 if righthand is bigger
            int i = 0;
            int lhstring_len = lhstring.Length;
            int rhstring_len = rhstring.Length;

            while (i < lhstring_len && i < rhstring_len)
            {
                if (lhstring[i] != rhstring[i])
                {
                    return (int)lhstring[i] > (int)rhstring[i] ? -1 : 1;
                }
                i++;
            }
            if (lhstring_len == rhstring_len)
            {
                return 0;
            }
            return lhstring_len > rhstring_len ? -1 : 1;
        }

        public void Add(string element)
        {
            int elementsListLength = elements.Count;
            bool isAdded = false;
            
            for (int i = 0; i < elementsListLength; i++) // if string is null, int < null not possible?
            {
                int isBig = CheckLexicographicOrder(elements[i], element);
                if (isBig == -1 || isBig == 0)
                {
                    elements.Insert(i, element);
                    isAdded = true;
                    break;
                }
            }

            if (isAdded == false)
            {
                elements.Add(element);
            }

            if (elements_multifier.ContainsKey(element))
            {
                int value;
                elements_multifier.TryGetValue(element, out value);
                elements_multifier[element] = ++value;
            }
            else
            {
                elements_multifier.Add(element, 1);
            }            
        }

        public bool Remove(string element)
        {
            if (elements.Remove(element))
            {
                int value;
                elements_multifier.TryGetValue(element, out value);

                if (value > 1)
                {
                    elements_multifier[element] = --value;
                }
                else
                {
                    elements_multifier.Remove(element);
                }
                return true;
            }
            return false;
        }

        public uint GetMultiplicity(string element)
        {
            int value;
            if (elements_multifier.TryGetValue(element, out value))
            {
                return (uint)value;
            }
            return 0;
        }

        public List<string> ToList()
        {
            return elements;
        }

        public MultiSet Union(MultiSet other)
        {
            MultiSet union = new MultiSet();
            for (int i = 0; i < elements.Count; i++)
            {
                union.Add(elements[i]);
            }
            for (int i = 0; i < other.elements.Count; i++)
            {
                union.Add(other.elements[i]);
            }
            MultiSet intersect = Intersect(other);

            for (int i = 0; i < intersect.elements.Count; i++)
            {
                union.Remove(intersect.elements[i]);
            }
            return union;
        }

        public MultiSet Intersect(MultiSet other)
        {
            MultiSet intersect = new MultiSet();

            for (int i = 0; i < elements.Count; i++)
            {
                if (other.elements.Contains(elements[i]))
                {
                    if (i > 0 && elements[i] == elements[i - 1])
                    {                       
                        continue;
                    }
                    int value1;
                    int value2;
                    elements_multifier.TryGetValue(elements[i], out value1);
                    other.elements_multifier.TryGetValue(elements[i], out value2);

                    int value3 = value2 > value1 ? value1 : value2;

                    for (int j = 0; j < value3; j++)
                    {
                        intersect.Add(elements[i]);
                    }
                }
            }
            return intersect; 
        }

        public MultiSet Subtract(MultiSet other)
        {
            MultiSet subtract = new MultiSet();
            subtract.elements = elements;
            subtract.elements_multifier = elements_multifier;

            MultiSet intersect = Intersect(other);
            for (int i = 0; i < intersect.elements.Count; i++)
            {
                subtract.Remove(intersect.elements[i]);
            }

            return subtract;
        }

        public List<MultiSet> FindPowerSet()
        {
            int n = elements.Count;
            int powerSetCount = 1 << n;
            List<MultiSet> result = new List<MultiSet>(powerSetCount);

            for (int setMask = 0; setMask < powerSetCount; setMask++)
            {
                MultiSet set = new MultiSet();
                for (int i = 0; i < n; i++)
                {
                    if ((setMask & (1 << i)) > 0)
                    {
                        set.Add(elements[i]);
                    }
                }
                result.Add(set);
            }
            return result;
        }

        public bool IsSubsetOf(MultiSet other)
        {
            foreach (var elements in elements_multifier)
            {
                int other_element_count;
                if (other.elements_multifier.TryGetValue(elements.Key, out other_element_count))
                {
                    if (elements.Value < other_element_count)
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
            foreach (var other_elements in other.elements_multifier)
            {
                int elements_count;
                if (elements_multifier.TryGetValue(other_elements.Key, out elements_count))
                {
                    if (other_elements.Value > elements_count)
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
