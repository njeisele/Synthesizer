
namespace Synthesizer
{
    partial class Synth
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.oscillator1 = new Synthesizer.Oscillator();
            this.oscillator2 = new Synthesizer.Oscillator();
            this.oscillator3 = new Synthesizer.Oscillator();
            this.SuspendLayout();
            // 
            // oscillator1
            // 
            this.oscillator1.Location = new System.Drawing.Point(12, 12);
            this.oscillator1.Name = "oscillator1";
            this.oscillator1.Size = new System.Drawing.Size(382, 104);
            this.oscillator1.TabIndex = 0;
            this.oscillator1.TabStop = false;
            // 
            // oscillator2
            // 
            this.oscillator2.Location = new System.Drawing.Point(12, 122);
            this.oscillator2.Name = "oscillator2";
            this.oscillator2.Size = new System.Drawing.Size(382, 84);
            this.oscillator2.TabIndex = 1;
            this.oscillator2.TabStop = false;
            this.oscillator2.Enter += new System.EventHandler(this.oscillator2_Enter);
            // 
            // oscillator3
            // 
            this.oscillator3.Location = new System.Drawing.Point(12, 212);
            this.oscillator3.Name = "oscillator3";
            this.oscillator3.Size = new System.Drawing.Size(382, 95);
            this.oscillator3.TabIndex = 2;
            this.oscillator3.TabStop = false;
            // 
            // Synth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.oscillator3);
            this.Controls.Add(this.oscillator2);
            this.Controls.Add(this.oscillator1);
            this.KeyPreview = true;
            this.Name = "Synth";
            this.Text = "Synthesizer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SynthKeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Oscillator oscillator1;
        private Oscillator oscillator2;
        private Oscillator oscillator3;
    }
}

