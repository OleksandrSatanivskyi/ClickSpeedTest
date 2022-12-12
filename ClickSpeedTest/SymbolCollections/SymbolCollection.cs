using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymbolCollections.SymbolCollections
{
    internal abstract class SymbolCollection
    {
        public string Symbols { get; set; }
        protected SymbolCollection PrimaryCollection { get; set; }

        public SymbolCollection()
        {
            Symbols = null;
            PrimaryCollection = null;
        }

        public void SetPrimaryCollection(SymbolCollection symbolCollection)
        {
            if(PrimaryCollection == null)
                    PrimaryCollection = symbolCollection;
            else
                PrimaryCollection.SetPrimaryCollection(symbolCollection);

            PrimaryCollection.CreateSymbols();
            Symbols = PrimaryCollection.Symbols;
        }
        public abstract void CreateSymbols();
    }
}
