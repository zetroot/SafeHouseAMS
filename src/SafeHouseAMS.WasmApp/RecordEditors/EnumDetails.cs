using System;
using EnumDesriptor;

namespace SafeHouseAMS.WasmApp.RecordEditors
{
    public class EnumDetails<T> where T : struct, Enum
    {
        public bool Selected { get; set; }
        public string Description { get; }
        public T Item { get; }

        public EnumDetails(T item)
        {
            Description = item.GetDescription();
            Item = item;
        }
    }
}
