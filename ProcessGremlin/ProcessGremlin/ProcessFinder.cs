﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProcessGremlins
{
    public class ProcessFinder : IProcessFinder
    {
        private readonly Func<IEnumerable<Process>> finderFunc;

        public ProcessFinder(Func<IEnumerable<Process>> finderFunc)
        {
            this.finderFunc = finderFunc;
        }

        public ProcessFinder(IEnumerable<IProcessFinder> finders)
        {
            this.finderFunc = () => finders.SelectMany(finder => finder.Invoke()); 
        }

        public IEnumerable<Process> Invoke()
        {
            return this.finderFunc.Invoke();
        }
    }
}