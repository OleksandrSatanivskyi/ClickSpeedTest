using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Еуые.SymbolCollections
{
    internal class LowerCaseEnglishLettersCollection : SymbolCollection
    {
        public override void CreateSymbols()
        {
            
            for (int i = 97; i <= 122; i++)
                Symbols += (char)i;
        }
    }
}
