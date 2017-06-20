using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace KinXub.Framework
{
    public class FileHelper
    {
        /// <summary>
        /// 圖片壓縮存檔加轉成jpg
        /// </summary>
        /// <param name="path">路徑</param>
        /// <param name="file">檔案</param>
        /// <returns></returns>
        public static ResultViewModel SavePhoto(string path, HttpPostedFileBase file)
        {
            ResultViewModel r = new ResultViewModel();
            r.IsSuccess = false;
            if (null != file && file.ContentLength > 0)
            {
                Dictionary<string, string> ImageTypes = new Dictionary<string, string>() { { "FFD8", ".jpg" }, { "89504E470D0A1A0A", ".png" } };
                string builtHex = string.Empty;
                using (Stream S = file.InputStream)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        //檢查原始副檔名
                        builtHex += S.ReadByte().ToString("X2");
                        if (ImageTypes.ContainsKey(builtHex))
                        {
                            r.IsSuccess = true;
                            break;
                        }
                    }

                    if (r.IsSuccess)
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromStream(S);
                        //壓縮 轉檔 存檔
                        ImageCodecInfo myImageCodecInfo = GetEncoderInfo("image/jpeg");
                        System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                        EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 90L);
                        EncoderParameters myEncoderParameters = new EncoderParameters(1);
                        myEncoderParameters.Param[0] = myEncoderParameter;

                        string NewFileName = Guid.NewGuid().ToString();
                        r.Msg = NewFileName;
                        string NewPath = Path.Combine(path, NewFileName + ".jpg");
                        img.Save(NewPath, myImageCodecInfo, myEncoderParameters);
                    }
                    else
                        r.Msg = "圖片格式錯誤！";
                }
            }
            return r;
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        /// <summary>
        /// Base64ToPDF
        /// </summary>
        /// <param name="photoBase64"></param>
        /// <returns></returns>
        public static byte[] GeneratePDF(string photoBase64)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                System.Drawing.Image pdfPHoto = Base64ToImage(photoBase64);

                var pSize = new Rectangle(-20, -50, pdfPHoto.Width / 2, pdfPHoto.Height / 2);
                Document doc = new Document(pSize);//PageSize.A4, 0.75F, 0.75F, 0.75F, 0.75F
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                Paragraph para = new Paragraph();
                para.Leading = 15;

                //將圖片加入到paragraph中
                iTextSharp.text.Image googleJPG = iTextSharp.text.Image.GetInstance(pdfPHoto, ImageFormat.Png);
                //調整圖片大小
                googleJPG.ScalePercent(50f);
                googleJPG.Alignment = Element.ALIGN_CENTER;//Image在paragraph中的話，可以設定left, center, right
                doc.Add(para);
                doc.Add(googleJPG);
                doc.Close();
                return ms.ToArray();
            }

        }

        private static System.Drawing.Image Base64ToImage(string base64String)
        {
            base64String = base64String.Replace("data:image/png;base64,", "");
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                return image;
            }
        }
    }
}
