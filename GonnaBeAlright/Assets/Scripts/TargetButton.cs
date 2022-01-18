using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public RPGBattle battleManager;
    public int id;

    public void OnSelect(BaseEventData eventData)
    {
        battleManager.HighlightTarget(id, true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        battleManager.HighlightTarget(id, false);
    }
}
