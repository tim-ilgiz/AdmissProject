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
using Admiss.View;
using Admiss.ViewModel;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using CascadeClassifier = OpenCvSharp.CascadeClassifier;
using Rect = OpenCvSharp.Rect;

namespace Admiss.Model
{
    public class CameraCapture : INotify
    {
        public VideoCapture videoCapture;
        private CascadeClassifier face_cascade;
        public FrameSource frameSource;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public bool Face_Detect_Flag { get; set; } = false;
        public bool Flag { get; set; } = true;
        public bool FoundFirstFace { get; set; } = true;

        public bool GoOut { get; set; } = true;

        private string link = string.Format("{0}\\cam.jpg", AppDomain.CurrentDomain.BaseDirectory);

        //Initialise the image matrix
        Mat img = new Mat();

        public void Init()
        {
            M:
            Flag = true;
            FoundFirstFace = true;
            Face_Detect_Flag = false;
            ReloadButtonEnable = false;

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

            dispatcherTimer.Tick += Start_Detect_Face;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            //dispatcherTimer.Start();

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
            //string imagePath = string.Format("{0}\\cam.jpg", AppDomain.CurrentDomain.BaseDirectory);
            //Save the captured image
            img.SaveImage(link);
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
            M:
            if (Flag)
            {
                Mat image;
                while (true)
                {
                    //Grab the current frame
                    image = GrabFrame();
                    //Convert to gray scale to improve the image processing
                    Mat gray;
                    try
                    {
                        gray = ConvertGrayScale(image);
                    }
                    catch
                    {
                        GoOut = false;
                        if (!GoOut)
                        {
                            Face_Detect_Flag = false;
                            GoOut = true;
                            break;
                        }
                        goto M;   
                    }
                    

                    //Detect faces using Cascase classifier
                    Rect[] faces = DetectFaces(gray);
                    if (image.Empty())
                        continue;
                    Mat face_roi = null;
                    //Loop through detected faces
                    foreach (var item in faces)
                    {
                        //Get the region of interest where you can find facial features
                        face_roi = gray[item];

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
                    VideoImage = WriteableBitmapConverter.ToWriteableBitmap(image);

                    //if (Cv2.WaitKey(1) == (int) ConsoleKey.Enter)
                    //    break;

                    Cv2.WaitKey(0);
                    if (face_roi != null && FoundFirstFace == true)
                    {
                        dispatcherTimer.Start();
                        FoundFirstFace = false;
                    }
                    else if (face_roi != null && Face_Detect_Flag == true)
                    {
                        ShotImage();
                        features.Clear();
                        Flag = false;
                        dispatcherTimer.Stop();
                        FoundFirstFace = true;
                        ReloadButtonEnable = true;
                        break;
                    }
                }
            }
        }

        private ImageSource _VideoImage;
        public ImageSource VideoImage
        {
            get { return _VideoImage; }
            set
            {
                _VideoImage = value;
                OnPropertyChanged(nameof(VideoImage));
            }
        }
        
        private void Start_Detect_Face(object sender, EventArgs e)
        {
            Face_Detect_Flag = true;
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
            //MessageBox.Show("Удачный кадр!");
            //ScreenImage = WriteableBitmapConverter.ToWriteableBitmap(image);
            Cv2.WaitKey(0);
        }

        public void Release()
        {
            videoCapture.Release();
            Cv2.DestroyAllWindows();
            frameSource.Reset();
            frameSource.Dispose();
        }

        public void DispatcherTimer_Tick()
        {
            var capturedImage = GrabFrame();
            ShowImage(capturedImage);
            var manipulatedImage = Manipulate(capturedImage);
            ShowImage(manipulatedImage);
            Release();

        }


        private bool _ReloadButtonEnable;
        public bool ReloadButtonEnable
        {
            get => _ReloadButtonEnable;
            set
            {
                _ReloadButtonEnable = value;
                OnPropertyChanged(nameof(ReloadButtonEnable));
            }
        }
    }
}