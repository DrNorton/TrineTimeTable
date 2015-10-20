using System;

namespace TrainTimeTable.Shared.Models
{
    /// <summary>
    /// Data to represent an item in the nav menu.
    /// </summary>
    public class NavMenuItem
    {
        public string Background { get; set; }
        public string Label { get; set; }
        public int Symbol { get; set; }
        public char SymbolAsChar
        {
            get
            {
                return (char)this.Symbol;
            }
        }

        public Type ViewModelType { get; set; }
        public object Arguments { get; set; }
    }
}
