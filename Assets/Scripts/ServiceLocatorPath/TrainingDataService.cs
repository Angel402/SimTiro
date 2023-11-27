using System.Collections.Generic;
using UI;

namespace ServiceLocatorPath
{
    public class TrainingDataService : ITrainingDataService
    {
        private List<TrainingData> _mapsData;

        public void SetTrainingData(List<TrainingData> mapsData)
        {
            _mapsData = mapsData;
        }

        public List<TrainingData> GetTrainingData()
        {
            return _mapsData;
        }
    }
}