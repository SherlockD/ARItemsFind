using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Design/New Quest")]

public class Quest : ScriptableObject {

    public string QuestName;
    public int ItemsCount;
    public int ItemsFindCount;
    public Item Item;
}


