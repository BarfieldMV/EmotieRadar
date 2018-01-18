using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace EmotionDetector
{
    public partial class EmotionDetectorForm : MetroFramework.Forms.MetroForm
    {
        private string imageFilePath = "";
        private List<RootObject> results;

        public EmotionDetectorForm()
        {
            InitializeComponent();
            facePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void selectPhotoButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                var file = openFileDialog.ShowDialog();

                if (file == DialogResult.OK)
                {
                    imageFilePath = openFileDialog.FileName;
                    var slee = Bitmap.FromFile(openFileDialog.FileName);
                    facePictureBox.Image = slee;

                    Cursor = Cursors.WaitCursor;
                    emotionHandlerBackgroundWorker.RunWorkerAsync();

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Fout bij openen plaatje:\n" + exception.ToString());
            }

        }

        byte[] GetImageAsByteArray()
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        List<RootObject> MakeRequest()
        {
            var client = new HttpClient();

            // Request headers - replace this example key with your valid key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "getakeyonline");

            // NOTE: You must use the same region in your REST call as you used to obtain your subscription keys.
            //   For example, if you obtained your subscription keys from westcentralus, replace "westus" in the 
            //   URI below with "westcentralus".
            string uri = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize?";
            HttpResponseMessage response;
            string responseContent;

            // Request body. Try this sample with a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray();

            using (var content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = client.PostAsync(uri, content).GetAwaiter().GetResult();
                responseContent = response.Content.ReadAsStringAsync().Result;
            }

            //A peak at the JSON response.
            Console.WriteLine(responseContent);
            var result = JsonConvert.DeserializeObject<List<RootObject>>(responseContent);
            return result;
        }

        private void emotionHandlerBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            results = MakeRequest();
        }

        private void emotionHandlerBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Fout bij bepalen emoties:\n" + e.Error.ToString());
                return;
            }
            if (results != null)
            {
                ComboFaces.Items.Clear();
                foreach (var rootObject in results)
                {
                    ComboFaces.Items.Add($"Gezicht nr {results.IndexOf(rootObject)}");
                }

                ComboFaces.SelectedIndex = 0;
            }
            else
            {
                propertyGrid1.SelectedObject = new Simple();
            }

            Cursor = Cursors.Default;
        }

        private void ComboFaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = ComboFaces.SelectedIndex;
            if (index < 0) return;

            var result = results[index];
            propertyGrid1.SelectedObject = new ScoreWrapper(result.scores);

            var rect = result.faceRectangle;

            var image = Bitmap.FromFile(openFileDialog.FileName);

            Graphics g = Graphics.FromImage(image);
            Pen pen = new Pen(Color.Red, 6);
            g.DrawRectangle(pen, new Rectangle(rect.left, rect.top, rect.width, rect.height));
            facePictureBox.Image = image;
        }
    }

    public class Simple
    {
        public string bericht = "geen resultaat";
    }

    public class FaceRectangle
    {
        public int height { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
    }

    public class Scores
    {
        public double anger { get; set; }
        public double contempt { get; set; }
        public double disgust { get; set; }
        public double fear { get; set; }
        public double happiness { get; set; }
        public double neutral { get; set; }
        public double sadness { get; set; }
        public double surprise { get; set; }
    }

    public class ScoreWrapper
    {
        private Scores inter;

        public ScoreWrapper(Scores inter)
        {
            this.inter = inter;
            string format = "n3";
            anger = inter.anger.ToString(format);
            contempt = inter.contempt.ToString(format);
            disgust = inter.disgust.ToString(format);
            fear = inter.fear.ToString(format);
            happiness = inter.happiness.ToString(format);
            neutral = inter.neutral.ToString(format);
            sadness = inter.sadness.ToString(format);
            surprise = inter.surprise.ToString(format);
        }

        public string anger { get; set; }
        public string contempt { get; set; }
        public string disgust { get; set; }
        public string fear { get; set; }
        public string happiness { get; set; }
        public string neutral { get; set; }
        public string sadness { get; set; }
        public string surprise { get; set; }
    }

    public class RootObject
    {
        public FaceRectangle faceRectangle { get; set; }
        public Scores scores { get; set; }
    }
}
