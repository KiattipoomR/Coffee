using UnityEngine;
using Utils;

namespace Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        [ReadOnly, SerializeField] private Item.Item item;
        [ReadOnly, SerializeField] private int stack;

        public Item.Item Item => item;
        public int Stack => stack;
        public float TotalWeight => Item.ItemData.Weight * Stack;

        public InventorySlot(Item.Item item, int stack)
        {
            this.item = item;
            this.stack = stack;
        }
        
        private bool IsNotFull(int amountToAdd)
        {
            return Stack + amountToAdd <= Item.ItemData.MaxStack;
        }

        private void AddToStack(int amount)
        {
            stack += amount;
        }
        
        private void RemoveFromStack(int amount)
        {
            stack -= amount;
        }
    }
}