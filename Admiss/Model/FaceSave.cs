using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
//using Emgu.CV;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using CascadeClassifier = OpenCvSharp.CascadeClassifier;
using Rect = OpenCvSharp.Rect;
using Admiss.View;



namespace Admiss.Model
{
    public class FaceSave
    {
        public VideoCapture videoCapture;
        CascadeClassifier face_cascade;
        FrameSource frameSource;
        //Initialise the image matrix
        Mat img = new Mat();
        public static FaceSave faceSave;

        public void Init()
        {
            //Initialise the video capture module
            videoCapture = new VideoCapture(0);
            videoCapture.Set(3, 640); //Set the frame width
            videoCapture.Set(4, 480); //Set the frame height

            //Define the face and eyes classifies using Haar-cascade xml
            face_cascade = new CascadeClassifier
                       (
                        "./haarcascades/haarcascade_frontalface_default.xml"
                        );
            frameSource = Cv2.CreateFrameSource_Camera(0);
        }
        public class FaceFeature
        {
            public Rect Face { get; set; }
        }

        public List<FaceFeature> features = new List<FaceFeature>();

        public Mat GrabFrame()
        {
            //Grab the frame to the img variable
            frameSource.NextFrame(img);
            return img;
        }

        public void ShotImage()
        {
            string imagePath = string.Format("{0}\\cam.jpg", AppDomain.CurrentDomain.BaseDirectory);
            //Save the captured image
            img.SaveImage(imagePath);
            //Release();
        }

        private Mat ConvertGrayScale(Mat image)
        {
            Mat gray = new Mat();
            Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);
            return gray;
        }

        private Rect[] DetectFaces(Mat image)
        {
            Rect[] faces = face_cascade.DetectMultiScale(image, 1.3, 5);
            return faces;
        }

        private void MarkFeatures(Mat image)
        {
            foreach (FaceFeature feature in features)
            {
                Cv2.Rectangle(image, feature.Face, new Scalar(0, 255, 0), thickness: 1);
                var face_region = image[feature.Face];
            }
        }
        public void DetectFeatures()
        {
            Mat image;
            while (true)
            {
                //Grab the current frame
                image = GrabFrame();
                //Convert to gray scale to improve the image processing
                Mat gray = ConvertGrayScale(image);

                //Detect faces using Cascase classifier
                Rect[] faces = DetectFaces(gray);
                if (image.Empty())
                    continue;
                //Loop through detected faces
                foreach (var item in faces)
                {
                    //Get the region of interest where you can find facial features
                    Mat face_roi = gray[item];


                    //Record the facial features in a list
                    features.Add(new FaceFeature()
                    {
                        Face = item
                    });
                    if (features.Count() > 1)
                    {
                        features.Clear();
                    }
                }
                //Mark the detected feature on the original frame
                MarkFeatures(image);
                //Cv2.ImShow("frame", image);
                PageCamera.mainWindow.imagedata.Source = image.ToWriteableBitmap();
                //MainWindowCamera.mainWindow.Show();
                //Cv2.WaitKey(10);
                //    break;
            }

        }
        
        public Mat Manipulate(Mat image)
        {
            //Initialise a new Mat variable to store the edge detected image
            Mat edgeDetection = new Mat();

            //Run Canny algorithm to detect the edges with two threshold values. 
            //Learn about Canny: http://dasl.unlv.edu/daslDrexel/alumni/bGreen/www.pages.drexel.edu/_weg22/can_tut.html
            Cv2.Canny(image, edgeDetection, 100, 200);
            return edgeDetection;
        }

        public void ShowImage(Mat image)
        {
            //Cv2.ImShow("img", image);
            MessageBox.Show("Удачный кадр!");
          Cv2.WaitKey(0);
        }

        public void Release()
        {
            videoCapture.Release();
            Cv2.DestroyAllWindows();
        }

    }
}