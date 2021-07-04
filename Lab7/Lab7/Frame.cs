using System;
using System.Collections.Generic;
using System.Text;

namespace Lab7
{
    public class Frame
    {
        public EFeatureFlags Features { get; private set; }
        public uint ID { get; private set; }
        public string Name { get; private set; }

        public Frame(uint id, string name)
        {
            Features = EFeatureFlags.Default; // 1. work with = 0? 2. necessary?
            ID = id;
            Name = name;
        }

        public void ToggleFeatures(EFeatureFlags features)
        {
            // feature on/off
            Features ^= (EFeatureFlags)features;
        }
        public void TurnOnFeatures(EFeatureFlags features)
        {
            Features |= (EFeatureFlags)features;
        }
        public void TurnOffFeatures(EFeatureFlags features)
        {
            Features &= ~(EFeatureFlags)features;
        }
    }
}
