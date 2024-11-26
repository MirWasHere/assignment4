using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    public UIInventoryItem[] items;

    public void InitializeInventoryUI(int inventorySize) {

        items = new UIInventoryItem[inventorySize];

        for (int i = 0; i < inventorySize; i++) {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            items[i] = uiItem;
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }

    }

    private void HandleItemSelection(UIInventoryItem obj) {
        Debug.Log(obj.name);
    }

    private void HandleShowItemActions(UIInventoryItem obj) {

    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
