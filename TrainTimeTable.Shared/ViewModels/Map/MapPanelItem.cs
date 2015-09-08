using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TrainTimeTable.Shared.ViewModels.Map
{
    public class MapPanelItem
    {
        public string Label { get; set; }
        public int Symbol { get; set; }
        public char SymbolAsChar
        {
            get
            {
                return (char)this.Symbol;
            }
        }

   
        public ICommand Command { get; set; }
    }
}
