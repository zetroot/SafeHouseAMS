using System;

namespace SafeHouseAMS.WasmApp.RecordEditors
{
    public class EnumDetails<T> where T : Enum
    {
        public bool Selected { get; set; }
        public string Description { get; }
        public T Item { get; }

        public EnumDetails(T item, string description)
        {
            Description = description;
            Item = item;
        }
    }
}
