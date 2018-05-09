using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public GameObject Starter;
    public GameObject ButtonPrefub;

    private StateManager StateManager;
    private List<GameObject> LevelsList = new List<GameObject>();
    private List<GameObject> SpawnedButtons = new List<GameObject>();

#region "Get Components"
    private GameObject GetStarter()
    {
        return gameObject.transform.parent.gameObject;
    }

    private StateManager GetStarterStateManager()
    {
        return Starter.GetComponent<StateManager>();
    }

    private List<GameObject> GetStatemanagerLevelsList()
    {
        return StateManager.LevelPrefubs;
    }
#endregion

#region "Spawn Main Menu Buttons"
    private GameObject GetMainMenuCanvas()
    {
        return gameObject.transform.Find("Canvas").gameObject;
    }

    private int GetLvlIndx(List<GameObject> Levels, GameObject Level)
    {
        return Levels.IndexOf(Level);
    }

    private void OnButtonClick(int Number)
    {
        StateManager.SettedLevelNum = Number;
    }

    private void ChangePosition(GameObject Button,float x,float y)
    {
        Button.GetComponent<RectTransform>().position = new Vector3(x, y, 0);
    }

    private void SetButtonName(GameObject Button,string Name)
    {
        Button.GetComponent<Button>().GetComponentInChildren<Text>().text = Name;
    }

    private void SpawnMenuButtons()
    {
        GameObject MainMenuCanvas = GetMainMenuCanvas();

        float startX = 200;
        float startY = 1141;

        float x = startX;
        float y = startY;

        int ButtonsNumber = 1;

        foreach(GameObject Level in LevelsList)
        {
            SpawnedButtons.Add(Instantiate(ButtonPrefub, Vector3.zero, Quaternion.identity, MainMenuCanvas.transform));        
            ChangePosition(SpawnedButtons[GetLvlIndx(LevelsList,Level)],x,y);
            SetButtonName(SpawnedButtons[GetLvlIndx(LevelsList, Level)], Level.name);
            SpawnedButtons[GetLvlIndx(LevelsList, Level)].GetComponent<Button>().onClick.AddListener(delegate { OnButtonClick(LevelsList.IndexOf(Level)); });
            if (ButtonsNumber % 2 == 0)
            {
                x = startX;
                y -= 250;
            }
            else
            {
                x += 330;
            }
            ButtonsNumber++;
            Debug.Log("Номер уровня: " + LevelsList.IndexOf(Level) + "Название уровня:" + Level.name);
        }
    }
#endregion

    void Start () {
        Starter = GetStarter();
        StateManager = GetStarterStateManager();
        LevelsList = GetStatemanagerLevelsList();
        SpawnMenuButtons();

    }

	void Update () {
		
	}
}
