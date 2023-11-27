using System.Collections.Generic;
using UI;

namespace ServiceLocatorPath
{
    public interface ITrainingDataService
    {
        void SetTrainingData(List<TrainingData> mapsData);
        List<TrainingData> GetTrainingData();
    }
}