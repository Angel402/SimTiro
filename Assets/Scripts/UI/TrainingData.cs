using System.Collections.Generic;

namespace UI
{
    public class TrainingData
    {
        public string mapName;
        public List<ulong> activesInMap;

        public TrainingData(string mapName, List<ulong> activesInMap)
        {
            this.mapName = mapName;
            this.activesInMap = activesInMap;
        }
    }
}