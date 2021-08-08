using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Diagnostics;

namespace Synthesizer
{
    public partial class Synth : Form
    {
        private const int SAMPLE_RATE = 44100; // Samples per second
        private short BITS_PER_SAMPLE = 16; // size of a sample 2^16 = 65k

        public Synth(Score s)
        {
            InitializeComponent();
            /*
             * Testing music reader
             */
            Debug.WriteLine(s);
            Player p = new Player();
            p.Play(s);

            
            //var score = MusicXmlParser.GetScore("Music/Chords.xml");
            //Debug.WriteLine(score);   
        }

        private void SynthKeyDown(object sender, KeyEventArgs e)
        {
            short[] wave = new short[SAMPLE_RATE];
            float freq;
            switch (e.KeyCode) {
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
            }
            // size of gives size in bytes of each data type (so this is SAMPLE_RATE * 2)
            byte[] binaryWave = new Byte[SAMPLE_RATE * sizeof(short)];
            int samplesPerWavelength = (int)(SAMPLE_RATE / freq);
            short tempSample;
            short ampStep = (short)((short.MaxValue * 2) / samplesPerWavelength);

            IEnumerable<Oscillator> oscillators = this.Controls.OfType<Oscillator>().Where(o => o.On);
            int oscillatorsCount = oscillators.Count(); // Will only have a few oscillators so short is fine
            foreach (Oscillator oscillator in oscillators)
            {
                switch(oscillator.WaveForm)
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
                
            }

            Buffer.BlockCopy(wave, 0, binaryWave, 0, wave.Length * sizeof(short));
            using (MemoryStream memoryStream = new MemoryStream())
            using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream)) {
                short blockAlign = (short)(BITS_PER_SAMPLE / 8);
                int numChannels = 1;
                int subChunk2Size = SAMPLE_RATE * numChannels * blockAlign;
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
                new SoundPlayer(memoryStream).Play();
            }
        }

        private void oscillator2_Enter(object sender, EventArgs e)
        {

        }
    }
    public enum WaveForm
    {
        Sine, Square, Saw, Triangle, Noise
    }
}
