using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Admiss.Model.FaceVerificaton
{
    public class Face2FaceVerification : INotify
    {
        public static ResultViewModel resultViewModel;
        public Face2FaceVerification()
        {
            ResultViewModel.face2FaceVerification = this;
        }
        /// <summary>
        /// Face detection result container for image on the left
        /// </summary>
        private ObservableCollection<Face> _leftResultCollection = new ObservableCollection<Face>();

        /// <summary>
        /// Face detection result container for image on the right
        /// </summary>
        private ObservableCollection<Face> _rightResultCollection = new ObservableCollection<Face>();

        private string linkleft = string.Format("{0}\\cam.jpg", AppDomain.CurrentDomain.BaseDirectory);

        private string linkright = string.Format("{0}\\cam2.jpg", AppDomain.CurrentDomain.BaseDirectory);

        /// <summary>
        /// Gets face detection results for image on the left
        /// </summary>
        public ObservableCollection<Face> LeftResultCollection
        {
            get
            {
                return _leftResultCollection;
            }
        }

        /// <summary>
        /// Gets face detection results for image on the right
        /// </summary>
        public ObservableCollection<Face> RightResultCollection
        {
            get
            {
                return _rightResultCollection;
            }
        }

        /// <summary>
        /// Gets or sets selected face verification result
        /// </summary>

        private string _FaceVerifyResult;
        public string FaceVerifyResult
        {
            get => _FaceVerifyResult;
            set
            {
                _FaceVerifyResult = value;
                OnPropertyChanged(nameof(FaceVerifyResult));
            }
        }

        private static bool _FaceResult;

        public static bool FaceResult
        {
            get { return _FaceResult; }
            set { _FaceResult = value; }
        }

        /// <summary>
        /// Gets max image size for UI rendering
        /// </summary>
        public int MaxImageSize
        {
            get
            {
                return 3300;
            }
        }

        #region Methods
        public async void LeftImagePicker_Metod()
        {
            FaceVerifyResult = string.Empty;
            var pickedImagePath = linkleft;
            var renderingImage = UIHelper.LoadImageAppliedOrientation(pickedImagePath);
            var imageInfo = UIHelper.GetImageInfoForRendering(renderingImage);
            LeftResultCollection.Clear();
            var sw = Stopwatch.StartNew();

            // Call detection REST API, detect faces inside the image
            using (var fileStream = File.OpenRead(pickedImagePath))
            {
                try
                {
                    string subscriptionKey = "4e787ab1c1244ed7b17c95b6238768c3";
                    string endpoint = "https://eastus.api.cognitive.microsoft.com/face/v1.0";
                    var faceServiceClient = new FaceServiceClient(subscriptionKey, endpoint);
                    var faces = await faceServiceClient.DetectAsync(fileStream);

                    // Handle REST API calling error
                    if (faces == null)
                    {
                        return;
                    }

                    foreach (var face in UIHelper.CalculateFaceRectangleForRendering(faces, MaxImageSize, imageInfo))
                    {
                        LeftResultCollection.Add(face);
                    }
                }
                catch (FaceAPIException ex)
                {
                    return;
                }
            }
            GC.Collect();
        }

        public async void RightImagePicker_Metod()
        {
            FaceVerifyResult = string.Empty;

            // User already picked one image
            var pickedImagePath = linkright;
            //var pickedImagePath = dlg.FileName;
            var renderingImage = UIHelper.LoadImageAppliedOrientation(pickedImagePath);
            var imageInfo = UIHelper.GetImageInfoForRendering(renderingImage);
            RightResultCollection.Clear();
            var sw = Stopwatch.StartNew();

            using (var fileStream = File.OpenRead(pickedImagePath))
            {
                try
                {
                    string subscriptionKey = "4e787ab1c1244ed7b17c95b6238768c3";
                    string endpoint = "https://eastus.api.cognitive.microsoft.com/face/v1.0";
                    var faceServiceClient = new FaceServiceClient(subscriptionKey, endpoint);

                    var faces = await faceServiceClient.DetectAsync(fileStream);
                    if (faces == null)
                    {
                        return;
                    }
                    foreach (var face in UIHelper.CalculateFaceRectangleForRendering(faces, MaxImageSize, imageInfo))
                    {
                        RightResultCollection.Add(face);
                    };
                }
                catch (FaceAPIException ex)
                {
                    return;
                }
            }
            GC.Collect();
        }

        public async void Face2FaceVerification_Metod()
        {
            if (LeftResultCollection.Count == 1 && RightResultCollection.Count == 1)
            {
                FaceVerifyResult = "Verifying...";
                var faceId1 = LeftResultCollection[0].FaceId;
                var faceId2 = RightResultCollection[0].FaceId;

                // Call verify REST API with two face id
                try
                {
                    string subscriptionKey = "4e787ab1c1244ed7b17c95b6238768c3";
                    string endpoint = "https://eastus.api.cognitive.microsoft.com/face/v1.0";
                    var faceServiceClient = new FaceServiceClient(subscriptionKey, endpoint);

                    var res = await faceServiceClient.VerifyAsync(Guid.Parse(faceId1), Guid.Parse(faceId2));

                    FaceResult = res.Confidence > 0.5 ? true : false;
                    FaceVerifyResult = string.Format("Confidence = {0:0.00}, {1}", res.Confidence, res.IsIdentical ? "two faces belong to same person" : "two faces not belong to same person");
                    //MessageBox.Show(FaceVerifyResult);
                    resultViewModel.FinishText();

                }
                catch (FaceAPIException ex)
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Verification accepts two faces as input, please pick images with only one detectable face in it.", "Warning", MessageBoxButton.OK);
            }
            GC.Collect();

        }

        #endregion Metods

    }
}
