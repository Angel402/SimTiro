using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UI
{
    [CreateAssetMenu(menuName = "Angel/MapsData", fileName = "MapsData")]
    public class MapsData : ScriptableObject
    {
        [Serializable]
        public class MapData
        {
            public string name;
            public int maxActives;
            public Sprite preview;
        }

        [SerializeField] private List<MapData> mapsData;

        private static MapsData _data;

        public static MapsData Instance
        {
            get
            {
                if (_data == null) _data = Resources.Load<MapsData>("MapsData");
                return _data;
            }
        }

        public MapData GetMapData(int map, out bool wasFirst)
        {
            if (mapsData.Count > map)
            {
                wasFirst = false;
                return mapsData[map];
            }
            else
            {
                wasFirst = true;
                return mapsData[0];
            }
        }
    }
}