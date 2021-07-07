using System;
using System.Collections.Generic;

namespace Assignment3
{
    public static class StepMaker
    {
        public static List<int> MakeSteps(int[] steps, INoise noise)
        {
            int depth = 0;
            int recursive = 0;
            int stepsLength = steps.Length;
            List<int> newSteps = new List<int>();

            for (int i = 0; i < stepsLength; i++)
            {
                newSteps.Add(steps[i]);
            }

            for (int i = 0; i < newSteps.Count; i++)
            {
                if (i == 0)
                {
                    continue;
                }

                if (newSteps[i] - newSteps[i - 1] > 10)
                {   
                    if (i < recursive)
                    {
                        depth++;
                    }

                    MakeStepsRecursive(newSteps, i - 1, noise, depth);
                    
                    recursive += 4;
                    i = 0;
                }
            }

            return newSteps;
        }
        public static List<int> MakeStepsRecursive(List<int> newSteps, int startIndex, INoise noise, int depth)
        {
            for (int i = 1; i < 5; i++)
            {
                newSteps.Insert(startIndex + i, newSteps[startIndex] + i * (newSteps[startIndex + i] - newSteps[startIndex]) / 5 + noise.GetNext(depth));
            }                        
            return newSteps;
        }
    }
}
