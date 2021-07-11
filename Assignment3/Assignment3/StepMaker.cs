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
                    MakeStepsRecursive(newSteps, i - 1, noise, depth, recursive);
                }
            }

            return newSteps;
        }
        public static List<int> MakeStepsRecursive(List<int> newSteps, int startIndex, INoise noise, int depth, int recursive)
        {
            List<int> addedSteps = new List<int>(4);

            for (int i = 1; i < 5; i++)
            {
                int result = ((10 - i * 2) * newSteps[startIndex] + (i * 2) * newSteps[startIndex + 1]) / 10;

                addedSteps.Add(result + noise.GetNext(depth));
            }

            newSteps.InsertRange(startIndex + 1, addedSteps);
            recursive += 4;

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
                        MakeStepsRecursive(newSteps, i - 1, noise, depth + 1, recursive);
                    }
                    else
                    {
                        MakeStepsRecursive(newSteps, i - 1, noise, depth, recursive);
                    }
                }
            }

            return newSteps;
        }
    }
}