using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Inventory.UI
{
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

        [SerializeField]
        private ItemActionPanel actionPanel;

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

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            items[itemIndex].Select();
        }

        public void UpdateData(int itemIndex,
            Sprite itemImage, int itemQuantity)
        {
            if (items.Length > itemIndex)
            {
                items[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        internal void ResetAllItems()
        {
            foreach (var item in items)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI) {
            int index = System.Array.IndexOf(items, inventoryItemUI);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }

        private void HandleShowItemActions(UIInventoryItem inventoryItemUI) {
            int index = System.Array.IndexOf(items, inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
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

        public void AddAction(string actionName, Action performAction)
        {
            actionPanel.AddButon(actionName, performAction);
        }

        public void ShowItemAction(int itemIndex)
        {
            actionPanel.Toggle(true);
            actionPanel.transform.position = items[itemIndex].transform.position;
        }

        public void DeselectAllItems() 
        {
            foreach (UIInventoryItem item in items)
            {
                item.Deselect();
            }
            actionPanel.Toggle(false);

        }

        public void Hide() {
            gameObject.SetActive(false);
            actionPanel.Toggle(false);
        }
    }
}
