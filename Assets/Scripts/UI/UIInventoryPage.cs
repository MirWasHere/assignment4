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

    [SerializeField]
    private UIInventoryDescription itemDescription;

    public event Action<int> OnDescriptionRequested, OnItemActionRequested;

    public UIInventoryItem[] items;

    // MOCK TEST DATA DELETE LATER
    public Sprite image;
    public int quantity;
    public string title, desc;

    private void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
    }

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

    public void UpdateData(int itemIndex,
        Sprite itemImage, int itemQuantity)
    {
        if (items.Length > itemIndex)
        {
            items[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    private void HandleItemSelection(UIInventoryItem inventoryItemUI) {
        Debug.Log(inventoryItemUI.name);
    }

    private void HandleShowItemActions(UIInventoryItem inventoryItemUI) {

    }

    public void Show() {
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void ResetSelection() 
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    public void DeselectAllItems() 
    {
        foreach (UIInventoryItem item in items) {
            item.Deselect();
        }

    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
