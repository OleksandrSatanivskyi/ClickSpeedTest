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
        private Random random { get; set; }
        public SymbolCollection()
        {
            Symbols = null;
            PrimaryCollection = null;
            random = new Random();
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

        public char GetRandomSymbol()
         => Symbols[random.Next(0, Symbols.Length)];
    }
}
