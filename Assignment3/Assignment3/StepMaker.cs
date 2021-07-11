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

                if (Math.Abs(newSteps[i] - newSteps[i - 1]) > 10)
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
                /*int result = i * (newSteps[startIndex + i] - newSteps[startIndex]) * 2;
                result /= 10;
                result += newSteps[startIndex] + noise.GetNext(depth);*/
                int result = ((10 - i * 2) * newSteps[startIndex] + (i * 2) * newSteps[startIndex + i]) / 10;

                newSteps.Insert(startIndex + i, result + noise.GetNext(depth));
            }                        
            return newSteps;
        }
    }
}
