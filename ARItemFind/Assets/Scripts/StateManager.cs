using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    private GameObject LevelPrefub;
    private int Num;
    private bool PlaneFinderEnabled;

    public Level(GameObject LevelPrefub, int Num,bool PlaneFinderEnabled)
    {
        this.PlaneFinderEnabled = PlaneFinderEnabled;
        this.LevelPrefub = LevelPrefub;
        this.Num = Num;
    }

    public bool GetLevelPlaneFinderState()
    {
        return PlaneFinderEnabled;
    }

    public GameObject GetLevelPrefub()
    {
        return LevelPrefub;
    }

    public int GetLevelNum()
    {
        return Num;
    }
}

public class StateManager : MonoBehaviour {

    public GameObject ARCamera;
    public GameObject GroundPlaneStage;
    public GameObject PlaneFinder;
    public List<GameObject> LevelPrefubs = new List<GameObject>();

    private List<Level> Levels = new List<Level>();
    private GameObject SpawnedLevel;
    internal int SettedLevelNum = 0;
    private int DrawedLevelNum;
    private ARCameraDisabler ARCameraDisabler = new ARCameraDisabler();

#region "Load Levels"
    private void DestroySpawnedLevel(ref GameObject SpawnedLevel)
    {
        Destroy(SpawnedLevel);
    }

    private int GetLevelIndex(List<GameObject> LevelPrefubs, GameObject Level)
    {
        return LevelPrefubs.IndexOf(Level);
    }


    private bool GetLevelPlaneFinderState(GameObject Level)
    {
        return Level.GetComponent<LevelState>().PlaneFinderEnabled;
    }

    private void LoadLevels()
    {
        foreach(GameObject LevelPrefub in LevelPrefubs)
        {
            var NewLevel = new Level(LevelPrefub, GetLevelIndex(LevelPrefubs,LevelPrefub), GetLevelPlaneFinderState(LevelPrefub));

            Levels.Add(NewLevel);
        }
    }
    #endregion

#region "Draw Level"

    private void SetObjectLocalPositionToZero(GameObject Object)
    {
        Object.transform.localPosition = Vector3.zero;
    }

    private bool GetLevelPlaneFinderStateByNum(int Num)
    {
        return Levels[Num].GetLevelPlaneFinderState();
    }

    private PlaneFinderDisabler GetPlaneFinderDisabler()
    {
        return ARCamera.GetComponent<PlaneFinderDisabler>();
    }

    private GameObject GetLevelPrefubByNum(int Num)
    {
        return Levels[Num].GetLevelPrefub();
    }

    private void DrawLevel(Level Level)
    {
        SpawnedLevel = Instantiate(Level.GetLevelPrefub(), Vector3.zero, Quaternion.identity, GroundPlaneStage.transform);
        DrawedLevelNum = Level.GetLevelNum();
    }

    private GameObject GetObjectParent(GameObject Object)
    {
        return Object.transform.parent.gameObject;
    }

    private void DrawLevel(int Num)
    {
        if (GetLevelPlaneFinderStateByNum(Num))
        {
            GetPlaneFinderDisabler().PlaneFinderEnable();
            SpawnedLevel = Instantiate(GetLevelPrefubByNum(Num), Vector3.zero, Quaternion.identity, GroundPlaneStage.transform);
            SetObjectLocalPositionToZero(SpawnedLevel);
        }
        else
        {
            GetPlaneFinderDisabler().PlaneFinderDisable();
            SpawnedLevel = Instantiate(GetLevelPrefubByNum(Num), Vector3.zero, Quaternion.identity,gameObject.transform);
        }
        DrawedLevelNum = Num;
    }
#endregion

    private void ChangeLevel(int Num)
    {
        Destroy(SpawnedLevel);
        DrawLevel(Num);
    }

    void Start ()
    {
       ChangeLevel(0);
    }
	
	void Update ()
    {
        if (DrawedLevelNum != SettedLevelNum)
        {
            ChangeLevel(SettedLevelNum);
        }
    }

    private void Awake()
    {
        ARCameraDisabler.ARCamera = ARCamera;
        LoadLevels();
    }
}
