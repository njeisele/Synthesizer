using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace Synthesizer
{
    public class Oscillator : GroupBox // Gets all functionality of a group box
    {

        public Oscillator()
        {
            int x0 = 10;
            int y0 = 15;
            int width = 70;
            int height = 30;

            this.Controls.Add(new Button()
            {
                Name = "Sine",
                Location = new Point(x0, y0),
                Text = "Sine",
                BackColor = Color.Green
            }
            );
            this.Controls.Add(new Button()
            {
                Name = "Square",
                Location = new Point(x0 + width, y0),
                Text = "Square",
            }
             );
            this.Controls.Add(new Button()
            {
                Name = "Saw",
                Location = new Point(x0 + 2*width, y0),
                Text = "Saw"
            }
            );
            this.Controls.Add(new Button()
            {
                Name = "Triangle",
                Location = new Point(x0, y0 + height),
                Text = "Triangle"
            }
            );
            this.Controls.Add(new Button()
            {
                Name = "Noise",
                Location = new Point(x0 + width, y0 + height),
                Text = "Noise"
            }
            );

            foreach (Control control in this.Controls)
            {
                control.Size = new Size(width, height);
                control.Font = new Font("Microsoft Sans Serif", 6.75f);
                control.Click += WaveButton_Click; // Sets click event handler
            }

            this.Controls.Add(new CheckBox()
            {
                Name = "OscillatorOn",
                Location = new Point(x0 + width * 3, y0),
                Size = new Size(width, height),
                Text = "On",
                Checked = true
            }); 
        }

        public WaveForm WaveForm { get; private set; }
        public bool On => ((CheckBox) this.Controls["OscillatorOn"]).Checked;

        private void WaveButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            this.WaveForm = (WaveForm) Enum.Parse(typeof(WaveForm), button.Text);
            foreach(Button b in this.Controls.OfType<Button>()) {
                if (b == button)
                {
                    b.BackColor = Color.Green;
                } else
                {
                    b.UseVisualStyleBackColor = true;
                }
            }
        }

    }
}
