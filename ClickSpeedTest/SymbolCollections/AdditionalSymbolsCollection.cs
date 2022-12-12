using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymbolCollections.SymbolCollections
{
    internal class AdditionalSymbolsCollection: SymbolCollection
    {
        public override void CreateSymbols()
        {
           
            this.Symbols += "[]{}\\|.,<>?/!@#$%^&*-_+='\";:~`";
        }
    }
}
