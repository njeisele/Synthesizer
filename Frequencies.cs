using System;
using System.Collections.Generic;
using System.Text;
using static Synthesizer.Note;

namespace Synthesizer
{
    public class Frequencies
    {
        Dictionary<(Pitch, int, Alteration), float> NoteToFreq;


        private void AddFrequencies(Dictionary<(Pitch, int, Alteration), float> dict, Pitch p, Alteration a, float baseFrequency) 
        {
            for (int octave = 0; octave < 9; octave++)
            {
                float freq = baseFrequency * (octave + 1);
                dict.Add((p, octave, a), (float)(baseFrequency * Math.Pow(2, octave))); // Each octave doubles the frequency, quick way to calculate all freqs
            }
        }
        public Frequencies()
        {
            // TODO: Store this somewhere else, b/c instantiating this for every note is bad
            this.NoteToFreq = new Dictionary<(Pitch, int, Alteration), float>();

            // TODO: I think this could be made shorter if there is a pattern with the space b/w notes
            AddFrequencies(NoteToFreq, Pitch.A, Alteration.Normal, 27.50f);
            AddFrequencies(NoteToFreq, Pitch.A, Alteration.Flat, 25.96f);
            AddFrequencies(NoteToFreq, Pitch.A, Alteration.Sharp, 29.14f);

            AddFrequencies(NoteToFreq, Pitch.B, Alteration.Normal, 30.87f);
            AddFrequencies(NoteToFreq, Pitch.B, Alteration.Flat, 29.14f);

            AddFrequencies(NoteToFreq, Pitch.C, Alteration.Normal, 16.35f);
            //AddFrequencies(NoteToFreq, Pitch.C, Alteration.Flat, 16.35f);
            AddFrequencies(NoteToFreq, Pitch.C, Alteration.Sharp, 17.32f);

            AddFrequencies(NoteToFreq, Pitch.D, Alteration.Normal, 18.35f);
            AddFrequencies(NoteToFreq, Pitch.D, Alteration.Flat, 17.32f);
            AddFrequencies(NoteToFreq, Pitch.D, Alteration.Sharp, 19.45f);

            AddFrequencies(NoteToFreq, Pitch.E, Alteration.Normal, 20.60f);
            AddFrequencies(NoteToFreq, Pitch.E, Alteration.Flat, 19.45f);
            AddFrequencies(NoteToFreq, Pitch.E, Alteration.Sharp, 21.83f);

            AddFrequencies(NoteToFreq, Pitch.F, Alteration.Normal, 20.60f);
            AddFrequencies(NoteToFreq, Pitch.F, Alteration.Flat, 21.83f);
            AddFrequencies(NoteToFreq, Pitch.F, Alteration.Sharp, 23.12f);

            AddFrequencies(NoteToFreq, Pitch.G, Alteration.Normal, 23.12f);
            AddFrequencies(NoteToFreq, Pitch.G, Alteration.Flat, 24.50f);
            AddFrequencies(NoteToFreq, Pitch.G, Alteration.Sharp, 25.96f);

            AddFrequencies(NoteToFreq, Pitch.Rest, Alteration.Normal, 23.12f);
            AddFrequencies(NoteToFreq, Pitch.Rest, Alteration.Flat, 24.50f);
            AddFrequencies(NoteToFreq, Pitch.Rest, Alteration.Sharp, 25.96f);

        }

        public float GetFreqForNote((Pitch, int, Alteration) noteData)
        {
            return this.NoteToFreq[noteData];
        }
    }
}




// TODO: might be better to parse this from an excel file
/*NoteToFreq.Add((Pitch.A, 0, Alteration.Normal), 27.5f);
NoteToFreq.Add((Pitch.A, 1, Alteration.Normal), 55f);
NoteToFreq.Add((Pitch.A, 2, Alteration.Normal), 313f);
NoteToFreq.Add((Pitch.A, 3, Alteration.Normal), 220f);
NoteToFreq.Add((Pitch.A, 4, Alteration.Normal), 440f);
NoteToFreq.Add((Pitch.A, 5, Alteration.Normal), 880f);
NoteToFreq.Add((Pitch.A, 6, Alteration.Normal), 1760f);
NoteToFreq.Add((Pitch.A, 7, Alteration.Normal), 3520f);
NoteToFreq.Add((Pitch.A, 8, Alteration.Normal), 7040f);

NoteToFreq.Add((Pitch.A, 0, Alteration.Flat), 25.96f);
NoteToFreq.Add((Pitch.A, 1, Alteration.Flat), 51.91f);
NoteToFreq.Add((Pitch.A, 2, Alteration.Flat), 103.83f);
NoteToFreq.Add((Pitch.A, 3, Alteration.Flat), 207.65f);
NoteToFreq.Add((Pitch.A, 4, Alteration.Flat), 415.30f);
NoteToFreq.Add((Pitch.A, 5, Alteration.Flat), 415.30f);
NoteToFreq.Add((Pitch.A, 6, Alteration.Flat), 1661.22f);
NoteToFreq.Add((Pitch.A, 7, Alteration.Flat), 3322.44f);
NoteToFreq.Add((Pitch.A, 8, Alteration.Flat), 6644.88f);

NoteToFreq.Add((Pitch.A, 0, Alteration.Sharp), 25.96f);
NoteToFreq.Add((Pitch.A, 1, Alteration.Sharp), 58.27f);
NoteToFreq.Add((Pitch.A, 2, Alteration.Sharp), 116.54f);
NoteToFreq.Add((Pitch.A, 3, Alteration.Sharp), 233.08f);
NoteToFreq.Add((Pitch.A, 4, Alteration.Sharp), 466.16f);
NoteToFreq.Add((Pitch.A, 5, Alteration.Sharp), 932.33f);
NoteToFreq.Add((Pitch.A, 6, Alteration.Sharp), 1864.66f);
NoteToFreq.Add((Pitch.A, 7, Alteration.Sharp), 3729.31f);
NoteToFreq.Add((Pitch.A, 8, Alteration.Sharp), 7458.62f);
NoteToFreq.Add((Pitch.B, 0, Alteration.Normal), 30.87f);
NoteToFreq.Add((Pitch.B, 1, Alteration.Normal), 61.74f);
NoteToFreq.Add((Pitch.B, 2, Alteration.Normal), 123.47f);
NoteToFreq.Add((Pitch.B, 3, Alteration.Normal), 246.94f);
NoteToFreq.Add((Pitch.B, 4, Alteration.Normal), 493.88f);
NoteToFreq.Add((Pitch.B, 5, Alteration.Normal), 987.77f);
NoteToFreq.Add((Pitch.B, 6, Alteration.Normal), 1975.53f);
NoteToFreq.Add((Pitch.B, 7, Alteration.Normal), 3951.07f);
NoteToFreq.Add((Pitch.B, 8, Alteration.Normal), 7902.13f);

NoteToFreq.Add((Pitch.B, 0, Alteration.Flat), 29.14f);
NoteToFreq.Add((Pitch.B, 1, Alteration.Flat), 58.27f);
NoteToFreq.Add((Pitch.B, 2, Alteration.Flat), 116.54f);
NoteToFreq.Add((Pitch.B, 3, Alteration.Flat), 246.94f);
NoteToFreq.Add((Pitch.B, 4, Alteration.Flat), 466.16f);
NoteToFreq.Add((Pitch.B, 5, Alteration.Flat), 932.33f);
NoteToFreq.Add((Pitch.B, 6, Alteration.Flat), 1864.66f);
NoteToFreq.Add((Pitch.B, 7, Alteration.Flat), 3729.31f);
NoteToFreq.Add((Pitch.B, 8, Alteration.Flat), 7902.13f);

// Should not be called
NoteToFreq.Add((Pitch.B, 0, Alteration.Sharp), 1254.55f);
NoteToFreq.Add((Pitch.B, 1, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.B, 2, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.B, 3, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.B, 4, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.B, 5, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.B, 6, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.B, 7, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.B, 8, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.C, 0, Alteration.Normal), 16.35f);
NoteToFreq.Add((Pitch.C, 1, Alteration.Normal), 32.70f);
NoteToFreq.Add((Pitch.C, 2, Alteration.Normal), 65.41f);
NoteToFreq.Add((Pitch.C, 3, Alteration.Normal), 130.81f);
NoteToFreq.Add((Pitch.C, 4, Alteration.Normal), 261.63f);
NoteToFreq.Add((Pitch.C, 5, Alteration.Normal), 523.25f);
NoteToFreq.Add((Pitch.C, 6, Alteration.Normal), 1046.50f);
NoteToFreq.Add((Pitch.C, 7, Alteration.Normal), 2093.00f);
NoteToFreq.Add((Pitch.C, 8, Alteration.Normal), 4186.01f); */

/*NoteToFreq.Add((Pitch.C, 0, Alteration.Flat), 1254.55f);
NoteToFreq.Add((Pitch.C, 1, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.C, 2, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.C, 3, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.C, 4, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.C, 5, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.C, 6, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.C, 7, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.C, 8, Alteration.Flat), 40f);*/
/*
NoteToFreq.Add((Pitch.C, 0, Alteration.Sharp), 17.32f);
NoteToFreq.Add((Pitch.C, 1, Alteration.Sharp), 34.65f);
NoteToFreq.Add((Pitch.C, 2, Alteration.Sharp), 69.30f);
NoteToFreq.Add((Pitch.C, 3, Alteration.Sharp), 138.59f);
NoteToFreq.Add((Pitch.C, 4, Alteration.Sharp), 277.18f);
NoteToFreq.Add((Pitch.C, 5, Alteration.Sharp), 554.37f);
NoteToFreq.Add((Pitch.C, 6, Alteration.Sharp), 2217.46f);
NoteToFreq.Add((Pitch.C, 7, Alteration.Sharp), 2217.46f);
NoteToFreq.Add((Pitch.C, 8, Alteration.Sharp), 4434.92f);
NoteToFreq.Add((Pitch.D, 0, Alteration.Normal), 27.5f);
NoteToFreq.Add((Pitch.D, 1, Alteration.Normal), 55f);
NoteToFreq.Add((Pitch.D, 2, Alteration.Normal), 313f);
NoteToFreq.Add((Pitch.D, 3, Alteration.Normal), 220f);
NoteToFreq.Add((Pitch.D, 4, Alteration.Normal), 440f);
NoteToFreq.Add((Pitch.D, 5, Alteration.Normal), 880f);
NoteToFreq.Add((Pitch.D, 6, Alteration.Normal), 1760f);
NoteToFreq.Add((Pitch.D, 7, Alteration.Normal), 3520f);
NoteToFreq.Add((Pitch.D, 8, Alteration.Normal), 7040f);

NoteToFreq.Add((Pitch.D, 0, Alteration.Flat), 1254.55f);
NoteToFreq.Add((Pitch.D, 1, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.D, 2, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.D, 3, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.D, 4, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.D, 5, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.D, 6, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.D, 7, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.D, 8, Alteration.Flat), 40f);

NoteToFreq.Add((Pitch.D, 0, Alteration.Sharp), 1254.55f);
NoteToFreq.Add((Pitch.D, 1, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.D, 2, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.D, 3, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.D, 4, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.D, 5, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.D, 6, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.D, 7, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.D, 8, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.E, 0, Alteration.Normal), 27.5f);
NoteToFreq.Add((Pitch.E, 1, Alteration.Normal), 55f);
NoteToFreq.Add((Pitch.E, 2, Alteration.Normal), 313f);
NoteToFreq.Add((Pitch.E, 3, Alteration.Normal), 220f);
NoteToFreq.Add((Pitch.E, 4, Alteration.Normal), 440f);
NoteToFreq.Add((Pitch.E, 5, Alteration.Normal), 880f);
NoteToFreq.Add((Pitch.E, 6, Alteration.Normal), 1760f);
NoteToFreq.Add((Pitch.E, 7, Alteration.Normal), 3520f);
NoteToFreq.Add((Pitch.E, 8, Alteration.Normal), 7040f);

NoteToFreq.Add((Pitch.E, 0, Alteration.Flat), 1254.55f);
NoteToFreq.Add((Pitch.E, 1, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.E, 2, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.E, 3, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.E, 4, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.E, 5, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.E, 6, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.E, 7, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.E, 8, Alteration.Flat), 40f);

NoteToFreq.Add((Pitch.E, 0, Alteration.Sharp), 1254.55f);
NoteToFreq.Add((Pitch.E, 1, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.E, 2, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.E, 3, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.E, 4, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.E, 5, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.E, 6, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.E, 7, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.E, 8, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.F, 0, Alteration.Normal), 27.5f);
NoteToFreq.Add((Pitch.F, 1, Alteration.Normal), 55f);
NoteToFreq.Add((Pitch.F, 2, Alteration.Normal), 313f);
NoteToFreq.Add((Pitch.F, 3, Alteration.Normal), 220f);
NoteToFreq.Add((Pitch.F, 4, Alteration.Normal), 440f);
NoteToFreq.Add((Pitch.F, 5, Alteration.Normal), 880f);
NoteToFreq.Add((Pitch.F, 6, Alteration.Normal), 1760f);
NoteToFreq.Add((Pitch.F, 7, Alteration.Normal), 3520f);
NoteToFreq.Add((Pitch.F, 8, Alteration.Normal), 7040f);

NoteToFreq.Add((Pitch.F, 0, Alteration.Flat), 1254.55f);
NoteToFreq.Add((Pitch.F, 1, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.F, 2, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.F, 3, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.F, 4, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.F, 5, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.F, 6, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.F, 7, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.F, 8, Alteration.Flat), 40f);

NoteToFreq.Add((Pitch.F, 0, Alteration.Sharp), 1254.55f);
NoteToFreq.Add((Pitch.F, 1, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.F, 2, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.F, 3, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.F, 4, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.F, 5, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.F, 6, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.F, 7, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.F, 8, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.G, 0, Alteration.Normal), 27.5f);
NoteToFreq.Add((Pitch.G, 1, Alteration.Normal), 55f);
NoteToFreq.Add((Pitch.G, 2, Alteration.Normal), 313f);
NoteToFreq.Add((Pitch.G, 3, Alteration.Normal), 220f);
NoteToFreq.Add((Pitch.G, 4, Alteration.Normal), 440f);
NoteToFreq.Add((Pitch.G, 5, Alteration.Normal), 880f);
NoteToFreq.Add((Pitch.G, 6, Alteration.Normal), 1760f);
NoteToFreq.Add((Pitch.G, 7, Alteration.Normal), 3520f);
NoteToFreq.Add((Pitch.G, 8, Alteration.Normal), 7040f);

NoteToFreq.Add((Pitch.G, 0, Alteration.Flat), 1254.55f);
NoteToFreq.Add((Pitch.G, 1, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.G, 2, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.G, 3, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.G, 4, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.G, 5, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.G, 6, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.G, 7, Alteration.Flat), 40f);
NoteToFreq.Add((Pitch.G, 8, Alteration.Flat), 40f);

NoteToFreq.Add((Pitch.G, 0, Alteration.Sharp), 1254.55f);
NoteToFreq.Add((Pitch.G, 1, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.G, 2, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.G, 3, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.G, 4, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.G, 5, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.G, 6, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.G, 7, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.G, 8, Alteration.Sharp), 40f);
NoteToFreq.Add((Pitch.Rest, 0, Alteration.Normal), 0f);
NoteToFreq.Add((Pitch.Rest, 1, Alteration.Normal), 0f);
NoteToFreq.Add((Pitch.Rest, 2, Alteration.Normal), 0f);
NoteToFreq.Add((Pitch.Rest, 3, Alteration.Normal), 0f);
NoteToFreq.Add((Pitch.Rest, 4, Alteration.Normal), 0f);
NoteToFreq.Add((Pitch.Rest, 5, Alteration.Normal), 0f);
NoteToFreq.Add((Pitch.Rest, 6, Alteration.Normal), 0f);
NoteToFreq.Add((Pitch.Rest, 7, Alteration.Normal), 0f);
NoteToFreq.Add((Pitch.Rest, 8, Alteration.Normal), 0f);

NoteToFreq.Add((Pitch.Rest, 0, Alteration.Flat), 0f);
NoteToFreq.Add((Pitch.Rest, 1, Alteration.Flat), 0f);
NoteToFreq.Add((Pitch.Rest, 2, Alteration.Flat), 0f);
NoteToFreq.Add((Pitch.Rest, 3, Alteration.Flat), 0f);
NoteToFreq.Add((Pitch.Rest, 4, Alteration.Flat), 0f);
NoteToFreq.Add((Pitch.Rest, 5, Alteration.Flat), 0f);
NoteToFreq.Add((Pitch.Rest, 6, Alteration.Flat), 0f);
NoteToFreq.Add((Pitch.Rest, 7, Alteration.Flat), 0f);
NoteToFreq.Add((Pitch.Rest, 8, Alteration.Flat), 0f);

NoteToFreq.Add((Pitch.Rest, 0, Alteration.Sharp), 0f);
NoteToFreq.Add((Pitch.Rest, 1, Alteration.Sharp), 0f);
NoteToFreq.Add((Pitch.Rest, 2, Alteration.Sharp), 0f);
NoteToFreq.Add((Pitch.Rest, 3, Alteration.Sharp), 0f);
NoteToFreq.Add((Pitch.Rest, 4, Alteration.Sharp), 0f);
NoteToFreq.Add((Pitch.Rest, 5, Alteration.Sharp), 0f);
NoteToFreq.Add((Pitch.Rest, 6, Alteration.Sharp), 0f);
NoteToFreq.Add((Pitch.Rest, 7, Alteration.Sharp), 0f);
NoteToFreq.Add((Pitch.Rest, 8, Alteration.Sharp), 0f); */