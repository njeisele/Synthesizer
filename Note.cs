using System;
using System.Collections.Generic;
using System.Text;

namespace Synthesizer
{
    public class Note
    {

        public enum Pitch
        {
            A, B, C, D, E, F, G, Rest
        }
        public enum Alteration
        {
            Flat, Sharp, Normal
        }
        public enum Time
        {
            eighth, quarter, sixteenth, half, whole
        }


        public int Octave { get; set; } /* Can only be 1 through 8 (Or some range)*/
     



        public Alteration Alter { get; set; }
        public Pitch NotePitch { get; set; }

        public Time NoteTime { get; set; }
        public float GetTimeAsFloat()
        {
            return NoteTime switch
            {
                Time.sixteenth => 0.0625f,
                Time.eighth => 0.125f,
                Time.quarter => 0.25f,
                Time.half => 0.5f,
                Time.whole => 1f,
                _ => 1f,
            };
        }

       


    }
}