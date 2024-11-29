using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private InventoryItem[] inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        public void Initialize()
        {
            inventoryItems = new InventoryItem[Size];

            for (int i = 0; i < Size; i++)
            {
                inventoryItems[i] = InventoryItem.GetEmptyItem();
            }
        }

        // adding item with no quantity
        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public int AddItem(ItemSO item, int quantity)
        {
            // keeps checking if inventory is full, then adds 1 item to each non-full slot (non-stackable)
            // until quantity is no more
            if(item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Length; i++)
                {
                    while(quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1);
                    }
                    InformAboutChange();
                    return quantity;
                }
            }

            // if stackable, simple add quantity to stackable (if non-empty)
            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }

        private bool IsInventoryFull() 
            => inventoryItems.Where(item => item.IsEmpty).Any() == false;

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity
            };

            for (int i = 0; i < inventoryItems.Length; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Length; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                if(inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTake =
                        inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if (quantity > amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while(quantity > 0 && IsInventoryFull() == false)
            {
                // forces the returned quantity inputed in to be whatever the stacksize is
                // aka if already 4 in slot, and max 10, but we try to add 20, return that we added 6
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);

                // the remaining quantity is the remaining amount, 
                quantity -= newQuantity;

                // and just add it to the next first free slot
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue =
                new Dictionary<int, InventoryItem>();

            for (int i = 0; i < inventoryItems.Length; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public bool IsEmpty => item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
            };
        }

        public static InventoryItem GetEmptyItem()
            => new InventoryItem
            {
                item = null,
                quantity = 0,
            };
    }
}
