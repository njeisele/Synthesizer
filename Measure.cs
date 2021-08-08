using System;
using System.Collections.Generic;
using System.Text;

namespace Synthesizer
{
    public class Measure
    {
        public List<Note> Notes { get; set; }
        // Time is beat over beat type
        // In the xml I think this is the same until it is explcitly changed
        public int Beat { get; set; }
        public int BeatType { get; set; }
    }
}
