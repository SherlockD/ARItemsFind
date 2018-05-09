using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



class Point
{
    public string name;
    public GameObject ItemModel;
    public Vector3 localPosition = new Vector3();
    public Vector3 rotation = new Vector3();
    public Vector3 localscale = new Vector3();

    public void SetName(string name)
    {
        this.name = name; ;
    }

    public void SetLocalPosition(float x,float y,float z)
    {
        localPosition.x = x;
        localPosition.y = y;
        localPosition.z = z;
    }

    public void SetLocalPosition(Vector3 localPosition)
    {
        this.localPosition.x = localPosition.x;
        this.localPosition.y = localPosition.y;
        this.localPosition.z = localPosition.z;
    }

    public void SetRotation(float x, float y, float z)
    {
        rotation.x = x;
        rotation.y = y;
        rotation.z = z;
    }

    public void SetRotation(Vector3 rotation)
    {
        this.rotation.x = rotation.x;
        this.rotation.y = rotation.y;
        this.rotation.z = rotation.z;
    }

    public void SetLocalScale(float x, float y, float z)
    {
        localscale.x = x;
        localscale.y = y;
        localscale.z = z;
    }

    public void SetLocalScale(Vector3 localscale)
    {
        this.localscale.x = localscale.x;
        this.localscale.y = localscale.y;
        this.localscale.z = localscale.z;
    }

    public void SetItemModel(GameObject ItemModel)
    {
        this.ItemModel = ItemModel;
    }
}

public class LevelManager : MonoBehaviour {

    public List<Quest> MapQuests = new List<Quest>();

    private GameObject MapTargets;
    private List<Point> Points = new List<Point>();
    private List<GameObject> SpawnedItems = new List<GameObject>();

#region "Current Level Load Settings"
    private GameObject GetCurrentLevel()
    {
        return gameObject;
    }

    private GameObject GetCurrentLevelMapTargets()
    {
        GameObject CurrentLevel = GetCurrentLevel();

        return gameObject.transform.Find("PointTargets").gameObject;
    }

    private void LoadLevelSettings()
    {
        MapTargets = GetCurrentLevelMapTargets();
    }
#endregion

#region "Item Parse"
    private Transform GetItemChild(GameObject Parent, int index)
    {
        return Parent.transform.GetChild(index);
    }

    private GameObject GetItemModel(Item item)
    {
        return item.Prefub;
    }

    private string GetItemName(Item item)
    {
        return item.Itemname;
    }

    private Vector3 GetPointLocalPosition(GameObject MapTargets,int i)
    {
        return GetItemChild(MapTargets, i).transform.localPosition;
    }

    private Vector3 GetItemRotation(Item item)
    {
        return item.Rotation;
    }

    private Vector3 GetItemLocalScale(Item item)
    {
        return item.LocalScale;
    }

    private Item GetItemSettings(Transform itemObj)
    {
        return itemObj.GetComponent<PointTarget>().SpawnItem;
    }

    private void DeActive(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void ItemsParse(GameObject MapTargets)
    {
        for (int i = 0; i < MapTargets.transform.childCount; i++)
        {
            var NewPoint = new Point();
            Item SelectedChild = GetItemSettings(GetItemChild(MapTargets,i));

            Debug.Log(GetItemName(SelectedChild));

            NewPoint.SetName(GetItemName(SelectedChild));
            NewPoint.SetItemModel(GetItemModel(SelectedChild));
            NewPoint.SetLocalPosition(GetPointLocalPosition(MapTargets,i));
            NewPoint.SetRotation(GetItemRotation(SelectedChild));
            NewPoint.SetLocalScale(GetItemLocalScale(SelectedChild));

            Destroy(GetItemChild(MapTargets,i).gameObject);

            Points.Add(NewPoint);
        }
    }
    #endregion

#region "ItemSpawn"

    private int LastIdx(List<GameObject> list)
    {
        return list.Count - 1;
    }

    private void SetItemWorldPosition(GameObject item,Vector3 localPosition,Vector3 localRotation,Vector3 localScale)
    {
        item.transform.localPosition = localPosition;
        item.transform.rotation *= Quaternion.Euler(localRotation);
        item.transform.localScale = localScale;
    }

    public void ItemsSpawn()
    {
        foreach (var point in Points)
        {
            SpawnedItems.Add(Instantiate(point.ItemModel, Vector3.zero, Quaternion.identity, gameObject.transform));
            SetItemWorldPosition(SpawnedItems[LastIdx(SpawnedItems)], point.localPosition, point.rotation,point.localscale);
            SpawnedItems[LastIdx(SpawnedItems)].tag = "ItemSpawned";
        }
    }
    #endregion

#region "ShowQuests"

    private string QuestGetState(Quest quest)
    {
        return quest.QuestName + " " + quest.ItemsFindCount.ToString() + "/" + quest.ItemsCount.ToString();
    }

    private void QuestsReset()
    {
        foreach(var quest in MapQuests)
        {
            quest.ItemsFindCount = 0;
        }
    }

    public void ShowQuests()
    {
        float yPos = 20;
        foreach (var quest in MapQuests)
        {
            GUIStyle Style = new GUIStyle();
            Style.fontSize = 20;           
            string questState = QuestGetState(quest);
            GUI.Label(new Rect((Screen.width/2), yPos, 90, 20),questState, Style);
            yPos += 20;
        }
    }

#endregion

    public void DestroyObj(GameObject obj)
    {
        Destroy(obj);
    }

    void Start () {
        LoadLevelSettings();
        ItemsParse(MapTargets);
        ItemsSpawn();
        QuestsReset();      
	}
	
	void Update () {

#region "RayCast"
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "ItemSpawned")
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
#endregion

    }

    private void OnGUI()
    {
        //ShowQuests();
        float yPos = 20;
        foreach (var quest in MapQuests)
        {
            GUIStyle Style = new GUIStyle();
            Style.fontSize = 20;
            string questState = QuestGetState(quest);
            GUI.Label(new Rect((Screen.width / 2), yPos, 90, 20), questState, Style);
            yPos += 20;
        }
    }
}
