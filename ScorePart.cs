using System;
using System.Collections.Generic;
using System.Text;

namespace Synthesizer
{
    public class ScorePart
    {
        /*
         * Part of score for one instrument
         */
        public List<Measure> Measures { get; set; }
        public int Volume { get; set; }
    }
}
