using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LoadSlotUI
{
    public Text dayText;
    public Text placeText;
    public Text slotText;
    public Text playTimeText;
    public Image clockImage;
    public Image dayImage;
}

public class LoadPanel : MonoBehaviour
{
    public List<LoadSlotUI> loadslotuis = new List<LoadSlotUI>(3);
}
