﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymbolCollections.SymbolCollections
{
    internal class NumbersCollection: SymbolCollection
    {
        public override void CreateSymbols()
        {
           
            Symbols += "0123456789";
        }
    }
}
