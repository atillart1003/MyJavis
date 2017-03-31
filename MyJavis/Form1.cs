using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Diagnostics;

namespace MyJavis
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer synThes = new SpeechSynthesizer();
        PromptBuilder pBuilder = new PromptBuilder();

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            btnDisable.Enabled = true;

        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
            btnDisable.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "hello", "print my name" , "open chrome",
                                        "open chrome", "open facebook", "what is the current time",
                                        "close"});
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammar = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += RecEngine_SpeechRecognized;
        }

        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch(e.Result.Text)
            {
                case "hello":
                    //MessageBox.Show("Hello Boss. How are you?");
                    //richTextBox1.Text += "\nHello Boss. How are you?";
                    synThes.SpeakAsync("Hello Boss. How are you");
                    break;

                case "print my name":
                    richTextBox1.Text += "\nTum";
                    break;

                case "open chrome":
                    Process.Start("chrome", "https://www.google.co.th/");
                    break;

                case "open facebook":
                    Process.Start("facebook", "https://www.facebook.com/");
                    break;

                case "What is the current time":
                    synThes.SpeakAsync("current time is " + DateTime.Now.ToLongTimeString());
                    break;

                case "close":
                    Application.Exit();
                    break;
            }
        }
    }
}
