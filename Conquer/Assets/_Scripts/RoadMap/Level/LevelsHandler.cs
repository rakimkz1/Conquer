using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Level
{ 
    public class LevelsHandler : MonoBehaviour
    {
        [SerializeField] private LevelPoint[] levelPoints;
        private SaveManager saveManager;
        private SaveData saveData;
        [Inject]
        private void Contruct(SaveManager saveManager)
        {
            this.saveManager = saveManager;
            saveData = saveManager.Load();
            SetLevelProperties();
        }

        private void SetLevelProperties()
        {
            List<LevelData> dataList = saveData.Get<List<LevelData>>(SaveDataKeys.LEVEL_POINTS_DATA, out bool isContain);

            if (isContain == false)
            {
                SetLevelDatas();
                return;
            }

            for (int i = 0; i < levelPoints.Length; i++)
            {
                levelPoints[i].SetData(dataList[i]);
            }
        }

        private void SetLevelDatas()
        {
            List<LevelData> levelDataList = new List<LevelData>();
            for(int i =0; i < levelPoints.Length; i++)
                levelDataList.Add(levelPoints[i].GetData());

            saveData.Set<List<LevelData>>(SaveDataKeys.LEVEL_POINTS_DATA, levelDataList);
            saveManager.Save(saveData);
        }
    }
}