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
    public partial class FaceDetectorForm : MetroFramework.Forms.MetroForm
    {
        private string subscriptionKey = "getakeyonline";
        private string imageFilePath = "";
        private List<RootObject> results;
        const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect";

        public FaceDetectorForm()
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
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters. A third optional parameter is "details".
            string requestParameters =
                "returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

            // Assemble the URI for the REST API Call.
            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response;

            // Request body. Posts a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray();

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = client.PostAsync(uri, content).GetAwaiter().GetResult();

                // Get the JSON response.
                string contentString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                // Display the JSON response.
                if (contentString.Contains("Image size is too big")) throw new ArgumentException("Plaatje te groot");
                if (contentString.Contains("Access denied due to invalid subscription key")) throw new ArgumentException("Ongeldige key");
                var result = JsonConvert.DeserializeObject<List<RootObject>>(contentString);
                return result;
            }
        }


        private void emotionHandlerBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            results = MakeRequest();
        }

        private void emotionHandlerBackgroundWorker_RunWorkerCompleted(
            object sender,
            System.ComponentModel.RunWorkerCompletedEventArgs e)
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
                if(results.Any())
                ComboFaces.SelectedIndex = 0;
            }
            else
            {
                propertyGrid1.SelectedObject =  null;
            }

            Cursor = Cursors.Default;
        }

        private void ComboFaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = ComboFaces.SelectedIndex;
            if (index < 0) return;

            var result = results[index];
            propertyGrid1.SelectedObject = new InformationDetection(result);

                var rect = result.faceRectangle;

            var image = Bitmap.FromFile(openFileDialog.FileName);

            Graphics g = Graphics.FromImage(image);
            Pen pen = new Pen(Color.Red, 6);
            g.DrawRectangle(pen, new Rectangle(rect.left, rect.top, rect.width, rect.height));
            facePictureBox.Image = image;
        }
    }

    public class FaceRectangle
    {
        public int top { get; set; }
        public int left { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class HeadPose
    {
        public double pitch { get; set; }
        public double roll { get; set; }
        public double yaw { get; set; }
    }

    public class FacialHair
    {
        public double moustache { get; set; }
        public double beard { get; set; }
        public double sideburns { get; set; }
    }

    public class Emotion
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

    public class Blur
    {
        public string blurLevel { get; set; }
        public double value { get; set; }
    }

    public class Exposure
    {
        public string exposureLevel { get; set; }
        public double value { get; set; }
    }

    public class Noise
    {
        public string noiseLevel { get; set; }
        public double value { get; set; }
    }

    public class Makeup
    {
        public bool eyeMakeup { get; set; }
        public bool lipMakeup { get; set; }
    }

    public class Occlusion
    {
        public bool foreheadOccluded { get; set; }
        public bool eyeOccluded { get; set; }
        public bool mouthOccluded { get; set; }
    }

    public class HairColor
    {
        public string color { get; set; }
        public double confidence { get; set; }
    }

    public class Hair
    {
        public double bald { get; set; }
        public bool invisible { get; set; }
        public List<HairColor> hairColor { get; set; }
    }

    public class FaceAttributes
    {
        public double smile { get; set; }
        public HeadPose headPose { get; set; }
        public string gender { get; set; }
        public double age { get; set; }
        public FacialHair facialHair { get; set; }
        public string glasses { get; set; }
        public Emotion emotion { get; set; }
        public Blur blur { get; set; }
        public Exposure exposure { get; set; }
        public Noise noise { get; set; }
        public Makeup makeup { get; set; }
        public List<object> accessories { get; set; }
        public Occlusion occlusion { get; set; }
        public Hair hair { get; set; }
    }

    public class RootObject
    {
        public string faceId { get; set; }
        public FaceRectangle faceRectangle { get; set; }
        public FaceAttributes faceAttributes { get; set; }
    }

    public class InformationDetection
    {
        private RootObject inte;

        public InformationDetection(RootObject inte)
        {
            this.inte = inte;
            FaceAttributes fa = inte.faceAttributes;

            gender = fa.gender;
            age = fa.age.ToString("n1");
            glasses = fa.glasses;
            if (!string.IsNullOrEmpty(Intensity(fa.facialHair.beard)))
                facialHair = Intensity(fa.facialHair.beard) + "baard ";
            if (!string.IsNullOrEmpty(Intensity(fa.facialHair.moustache)))
                facialHair += Intensity(fa.facialHair.moustache) + "snor";

            makeUp = fa.makeup.eyeMakeup ? "eye makeup " : "";
            makeUp += fa.makeup.lipMakeup ? "lip makeup" : "";

            if (fa.hair.bald > 0.5) hairColor = "bijna kaal " ;
            if(fa.hair.hairColor.Any())
                hairColor += fa.hair.hairColor.OrderBy(c=>c.confidence).First().color;

            happy = fa.emotion.happiness.ToString("n3");
        }

        private string Intensity(double d)
        {
            if (d >= 0.7) return "veel ";
            else if (d >0.4) return "beetje ";
            else if (d >0.2) return "weinig ";
            

            return string.Empty;
        }

        public string gender { get; set; }
        public string age { get; set; }
        public string glasses { get; set; }
        public string facialHair { get; set; }
        public string makeUp { get; set; }
        public string hairColor { get; set; }
        public string happy { get; set; }
    }

}