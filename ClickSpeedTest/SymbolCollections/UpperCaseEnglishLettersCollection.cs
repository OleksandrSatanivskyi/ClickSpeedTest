using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Еуые.SymbolCollections
{
    internal class UpperCaseEnglishLettersCollection: SymbolCollection
    {
        public override void CreateSymbols()
        {
            
            for (int i = 65; i <= 90; i++)
                Symbols += (char)i;
        }
    }
}
