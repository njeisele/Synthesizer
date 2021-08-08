using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Diagnostics;

namespace Synthesizer
{
    public class Player
    {

        private const int SAMPLE_RATE = 44100; // Samples per second ( I think this is more like samples per cycle)
        // TODO: figure this out
        private short BITS_PER_SAMPLE = 16; // size of a sample 2^16 = 65k
        int DEFAULT_BPM = 60; // 60 beats per minute
        Frequencies frequencies = new Frequencies();

        /*
         * Uses synthesizers to play the input score
         */
        public void Play(Score score)
        {


            // Holds the music data
            // It's a list of list because there is data for each part/instrument
            List<List<short>> waves = new List<List<short>>();

            // Helper : Averages two arrays

            foreach (ScorePart p in score.ScoreParts)
            {
                List<short> soundData = new List<short>();
                foreach (Measure m in p.Measures)
                {
                    List<short> soundDataForMeasure = GetSoundData(m);
                    soundData.AddRange(soundDataForMeasure);
                }
                waves.Add(soundData);
            }

            List<short> wave = new List<short>();
            int dataCount = waves[0].Count;
            Int16 numWaves = (short) waves.Count;
            for (int i = 0; i < dataCount; i++)
            {
                short val = 0;
                for (int j = 0; j < numWaves; j++)
                {
                    // Important: Need to do the division here because if we add all together
                    // they overflow since we are using vals up to MAX SHORT
                    val += (short) (waves[j][i] /  numWaves);
                }
                wave.Add(val);

                // This is not sounding right
            }
            // [0] sounds okay but has some clicking
            // [1] sounds okay but with clicking
            // possible precision/conversion error?
            // i think adding them together just makes the clicking worse
            //wave = waves[0];// TODO: REMOVE THIS, just checking if adding the waves is causing the issue

            short[] waveAsArray = wave.ToArray();
            //short[] waveAsArray = new short[SAMPLE_RATE];
            //for (int i =0; i < )
            // TODO: BECAUSE OF HOW IM DOING SAMPLE RATE MIGHT NEED THINK ABOUT THIS MATH MORE
            // SINCE IM DOING DIFFERENTLY FROM EXAMPLE

            byte[] binaryWave = new Byte[waveAsArray.Length * sizeof(short)];
            Buffer.BlockCopy(waveAsArray, 0, binaryWave, 0, waveAsArray.Length * sizeof(short));
            using (MemoryStream memoryStream = new MemoryStream())
            using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
            {
                short blockAlign = (short)(BITS_PER_SAMPLE / 8);
                int numChannels = 1;
                int numSamples = waveAsArray.Length;
                int subChunk2Size = numSamples * numChannels * blockAlign;
                binaryWriter.Write(new[] { 'R', 'I', 'F', 'F' });
                binaryWriter.Write(36 + subChunk2Size);
                binaryWriter.Write(new[] { 'W', 'A', 'V', 'E', 'f', 'm', 't', ' ' });
                binaryWriter.Write(16); // Sub chunk 1
                // Short is a 16 bit, and it wants this next value in 2 bytes (16 bits)
                binaryWriter.Write((short)1);
                binaryWriter.Write((short)numChannels);
                binaryWriter.Write(SAMPLE_RATE);
                binaryWriter.Write(SAMPLE_RATE * blockAlign);
                binaryWriter.Write(blockAlign);
                binaryWriter.Write(BITS_PER_SAMPLE);
                binaryWriter.Write(new[] { 'd', 'a', 't', 'a' });
                binaryWriter.Write(subChunk2Size);
                // Data is written in units of 1 bytes (so need to split the shorts in half)
                binaryWriter.Write(binaryWave);
                memoryStream.Position = 0; // Set position of memory stream to start
                MessageBox.Show("About to play");
                new SoundPlayer(memoryStream).Play();
            }
        }

        private List<short> GetSoundData(Measure m)
        {
            //int samplesPerWavelength = (int)(SAMPLE_RATE / freq);
            //short tempSample;
            //short ampStep = (short)((short.MaxValue * 2) / samplesPerWavelength);
            List<short> wave = new List<short>();
            int samplesPerBeat = SAMPLE_RATE / DEFAULT_BPM; // TODO: check
            float beatsPerSec = 1; // 60 bpm TODO: Add this in, leaving out to test
            int beatType = m.BeatType;
            // TODO: HANDLE CHORDS (I THINK I NEED TO LOOK AT VOICE OR SOMETHING?)
            int j = 0; // Trying to smooth where we sample the wave
            // this helps, the freq changing suddenly may also be an issue
            // Trying smoothing the frequneny transition
            float previousFreq = 0;
            float phase = 0;
            foreach (Note n in m.Notes)
            {
                Note x = n;
                float freq = frequencies.GetFreqForNote((n.NotePitch, n.Octave, n.Alter));
                float numberOfBeats = n.GetTimeAsFloat() * beatType;
                int numSamplesToTake = (int) (numberOfBeats * SAMPLE_RATE);
                int startingJ = j;
                //int freqRampSize = 2000; // Over how many samples we ramp the frequency to the target
                //float freqDelta = freq - previousFreq;
                //float freqIncrement = freqDelta / freqRampSize;
                for (; j < startingJ + numSamplesToTake; j++) {
                    float sample = AdvanceOscilator_Sine(ref phase, freq, SAMPLE_RATE);
                    wave.Add(Convert.ToInt16(short.MaxValue * sample));
                    //wave.Add(Convert.ToInt16(short.MaxValue * Math.Sin((Math.PI * 2 * currentFrequency * j / SAMPLE_RATE))));
                }
                previousFreq = freq;
            }
            return wave;
        }

        /* Helper function for smoother transition, keeps phase from going over 2pi to prevent float innacuracy */
        private float AdvanceOscilator_Sine(ref float phase, float freq, float sampleRate)
        {
            phase += 2 * (float) Math.PI * freq / sampleRate;

            while (phase >= 2 * (float) Math.PI)
                phase -= 2 * (float) Math.PI;

            while (phase < 0)
                phase += 2 * (float) Math.PI;

            return (float) Math.Sin(phase);
        }
    }
}


// TODO: Map from notes to freq
/*switch (e.KeyCode)
{
    case Keys.Z:
        freq = 65.4f;
        break;
    case Keys.X:
        freq = 138.59f;
        break;
    case Keys.C:
        freq = 261.62f;
        break;
    case Keys.V:
        freq = 523.25f;
        break;
    case Keys.B:
        freq = 1046.5f;
        break;
    case Keys.N:
        freq = 2093f;
        break;
    case Keys.M:
        freq = 4186.01f;
        break;
    default:
        freq = 440f;
        break;
} */


/*IEnumerable<Oscillator> oscillators = this.Controls.OfType<Oscillator>().Where(o => o.On);
           int oscillatorsCount = oscillators.Count(); // Will only have a few oscillators so short is fine
           foreach (Oscillator oscillator in oscillators)
           {
               switch (oscillator.WaveForm)
               {
                   case WaveForm.Sine:
                       for (int i = 0; i < wave.Length; i++)
                       {
                           // Generates numbers based on sin wave function
                           wave[i] += Convert.ToInt16(short.MaxValue * Math.Sin((Math.PI * 2 * freq * i / SAMPLE_RATE)) / oscillatorsCount);
                       }
                       break;
                   case WaveForm.Square:
                       for (int i = 0; i < wave.Length; i++)
                       {
                           // Generates numbers based on sin wave function
                           wave[i] += Convert.ToInt16(short.MaxValue * Math.Sign(Math.Sin((Math.PI * 2 * freq * i / SAMPLE_RATE))) / oscillatorsCount);
                       }
                       break;
                   case WaveForm.Saw:


                       for (int i = 0; i < SAMPLE_RATE; i++)
                       {
                           tempSample = -short.MaxValue;
                           for (int j = 0; j < samplesPerWavelength && i < SAMPLE_RATE; j++)
                           {
                               tempSample += ampStep;
                               wave[i++] += Convert.ToInt16(tempSample / oscillatorsCount);
                           }
                           i--;
                       }
                       break;
                   case WaveForm.Triangle:
                       tempSample = -short.MaxValue;
                       for (int i = 0; i < SAMPLE_RATE; i++)
                       {
                           if (Math.Abs(tempSample + ampStep) > short.MaxValue)
                           {
                               ampStep = (short)-ampStep;
                           }
                           tempSample += ampStep;
                           wave[i] += Convert.ToInt16(tempSample / oscillatorsCount);
                       }
                       break;
                   case WaveForm.Noise:
                       Random random = new Random();
                       for (int i = 0; i < SAMPLE_RATE; i++)
                       {
                           wave[i] += (short)(random.Next(-short.MaxValue, short.MaxValue) / oscillatorsCount);
                       }
                       break;
               }
           }*/

/*
 *  // size of gives size in bytes of each data type (so this is SAMPLE_RATE * 2)
            byte[] binaryWave = new Byte[SAMPLE_RATE * sizeof(short)];
            int samplesPerWavelength = (int)(SAMPLE_RATE / freq);
            short tempSample;
            short ampStep = (short)((short.MaxValue * 2) / samplesPerWavelength);

            IEnumerable<Oscillator> oscillators = this.Controls.OfType<Oscillator>().Where(o => o.On);
            int oscillatorsCount = oscillators.Count(); // Will only have a few oscillators so short is fine
            foreach (Oscillator oscillator in oscillators)
            {
                switch (oscillator.WaveForm)
                {
                    case WaveForm.Sine:
                        for (int i = 0; i < wave.Length; i++)
                        {
                            // Generates numbers based on sin wave function
                            wave[i] += Convert.ToInt16(short.MaxValue * Math.Sin((Math.PI * 2 * freq * i / SAMPLE_RATE)) / oscillatorsCount);
                        }
                        break;
                    case WaveForm.Square:
                        for (int i = 0; i < wave.Length; i++)
                        {
                            // Generates numbers based on sin wave function
                            wave[i] += Convert.ToInt16(short.MaxValue * Math.Sign(Math.Sin((Math.PI * 2 * freq * i / SAMPLE_RATE))) / oscillatorsCount);
                        }
                        break;
                    case WaveForm.Saw:


                        for (int i = 0; i < SAMPLE_RATE; i++)
                        {
                            tempSample = -short.MaxValue;
                            for (int j = 0; j < samplesPerWavelength && i < SAMPLE_RATE; j++)
                            {
                                tempSample += ampStep;
                                wave[i++] += Convert.ToInt16(tempSample / oscillatorsCount);
                            }
                            i--;
                        }
                        break;
                    case WaveForm.Triangle:
                        tempSample = -short.MaxValue;
                        for (int i = 0; i < SAMPLE_RATE; i++)
                        {
                            if (Math.Abs(tempSample + ampStep) > short.MaxValue)
                            {
                                ampStep = (short)-ampStep;
                            }
                            tempSample += ampStep;
                            wave[i] += Convert.ToInt16(tempSample / oscillatorsCount);
                        }
                        break;
                    case WaveForm.Noise:
                        Random random = new Random();
                        for (int i = 0; i < SAMPLE_RATE; i++)
                        {
                            wave[i] += (short)(random.Next(-short.MaxValue, short.MaxValue) / oscillatorsCount);
                        }
                        break;
                } */