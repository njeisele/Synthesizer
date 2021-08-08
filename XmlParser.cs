using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Synthesizer
{
    public class XmlParser
    {

        public static Score GetScoreFromFile(string file)
        {
            Debug.WriteLine(file);
            //var score = MusicXmlParser.GetScore(filePath);
            XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object
            xmlDoc.Load(file); // Load the XML document from the specified file
            Score score;
            List<ScorePart> scoreParts = new List<ScorePart>();
            XmlNodeList parts = xmlDoc.GetElementsByTagName("part");
            IEnumerator enumerator = parts.GetEnumerator();
            while (enumerator.MoveNext())
            {
                XmlNode currentPart = (XmlNode)enumerator.Current;
                ScorePart p = new ScorePart();
                List<Measure> scoreMeasures = new List<Measure>();
                XmlNodeList measures = currentPart.SelectNodes("measure");
                int beat = 4;
                int beatType = 4; // Store beat/beat type since it carries between measures
                foreach (XmlNode measure in measures)
                {
                    XmlNodeList notes = measure.SelectNodes("note");
                    List<Note> measureNotes = new List<Note>();

                    XmlNode attributes = measure.SelectSingleNode("attributes");
                    if (attributes != null)
                    {
                        XmlNode time = attributes.SelectSingleNode("time");
                        XmlNode beatNode = time.SelectSingleNode("beats");
                        XmlNode beatTypeNode = time.SelectSingleNode("beat-type");
                        beat = Int32.Parse(beatNode.InnerText);
                        beatType = Int32.Parse(beatTypeNode.InnerText);
                    }

                    foreach (XmlNode note in notes)
                    {
                        XmlNode type = note.SelectSingleNode("type");
                        string typeValue = type.InnerText;
                        if (typeValue == "16th")
                        {
                            typeValue = "sixteenth";
                        }

                        // TODO: check if the thing doesn't exist
                        XmlNode rest = note.SelectSingleNode("rest");
                        if (rest != null)
                        {
                            Note n = new Note
                            {
                                Octave = 0,
                                Alter = Note.Alteration.Normal,
                                NotePitch = Note.Pitch.Rest,
                                NoteTime = (Note.Time)Enum.Parse(typeof(Note.Time), typeValue)
                            };
                            measureNotes.Add(n);
                            Debug.WriteLine("");
                        }
                        else
                        {
                            XmlNode pitch = note.SelectSingleNode("pitch");
                            XmlNode step = pitch.SelectSingleNode("step");

                            XmlNode octave = pitch.SelectSingleNode("octave");
                            var octaveValue = octave.InnerText;
                            var stepValue = step.InnerText;

                            XmlNode alter = pitch.SelectSingleNode("alter");
                            Note.Alteration noteAlter;
                            if (alter != null)
                            {
                                var alterValue = alter.InnerText;
                                if (alterValue == "-1")
                                {
                                    noteAlter = Note.Alteration.Flat;
                                }
                                else if (alterValue == "1")
                                {
                                    noteAlter = Note.Alteration.Sharp;
                                }
                                else
                                {
                                    throw new FormatException("Found an alter that was not a flat or sharp");
                                }
                            }
                            else
                            {
                                noteAlter = Note.Alteration.Normal;
                            }
                            Note n = new Note
                            {
                                Octave = Int32.Parse(octaveValue),
                                Alter = noteAlter,
                                NotePitch = (Note.Pitch)Enum.Parse(typeof(Note.Pitch), stepValue),
                                NoteTime = (Note.Time)Enum.Parse(typeof(Note.Time), typeValue)
                            };
                            measureNotes.Add(n);
                            Debug.WriteLine("");
                        }
                    }
                    Measure scoreMeasure = new Measure
                    {
                        Beat = beat,
                        BeatType = beatType,
                        Notes = measureNotes
                    };
                    scoreMeasures.Add(scoreMeasure);
                }
                p = new ScorePart
                {
                    Measures = scoreMeasures
                };
                scoreParts.Add(p);
                Debug.Write(enumerator.Current + " ");
            }
            XmlNodeList scorePartInfo = xmlDoc.GetElementsByTagName("score-part");
            IEnumerator partEnumerator = parts.GetEnumerator();
            int i = 0;
            while (partEnumerator.MoveNext())
            {
                ScorePart s = scoreParts[i];
                /* TODO: Can get volume and settings for MIDI */
            }
            score = new Score
            {
                ScoreParts = scoreParts
            };

            return score;
        }
    }
}
