using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.DeviceService
{
    public class LPRAPI
    {
        string _webPath;
        public LPRAPI(string webPath)
        {
            _webPath = webPath;
        }

        [DllImport("LPRPInvoke.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateGVCLPRInstance(byte[] licenseFile);

        [DllImport("LPRPInvoke.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Recognize(IntPtr instance, byte[] buff, int size, int w, int h);

        [DllImport("LPRPInvoke.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetNumberOfImage(IntPtr result);

        [DllImport("LPRPInvoke.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetPlateInfo(IntPtr result, int index, out int imgSize, out int numberSize);

        [DllImport("LPRPInvoke.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetImage(IntPtr result, int index,
                                           byte[] plateImg, int imgLen, byte[] plateNumber, int numberLen);

        [DllImport("LPRPInvoke.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeleteInstance(IntPtr instance, IntPtr result);

        public bool PlateLicenseReconigze(byte[] buff, 
                                          out List<LPRResult> lprResults)
        {
            IntPtr ptrInstance, ptrResult;
            LPRResult tmpResult;
            bool ret, plateInfo, getImg;
            Image srcImg;
            int count, imgSize, numberSize;

            srcImg = Image.FromStream(new MemoryStream(buff));

            // Recognize
            string orgIIS = System.IO.Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(_webPath);
            ptrInstance = CreateGVCLPRInstance(PLR_LICENSE_FILE);
            ptrResult = Recognize(ptrInstance, buff, buff.Count(), srcImg.Width, srcImg.Height);
            Directory.SetCurrentDirectory(orgIIS);

            // Get number result
            count = GetNumberOfImage(ptrResult);

            if (count <= 0)
            {
                ret = false;
                lprResults = null;
            }
            else
            {
                lprResults = new List<LPRResult>();
                for(int i =0; i< count; i++)
                {
                    plateInfo = GetPlateInfo(ptrResult, i, out imgSize, out numberSize);
                    if (plateInfo == true)
                    {
                        tmpResult = new LPRResult();
                        tmpResult.plateImg = new byte[imgSize];
                        tmpResult.plateText = new byte[numberSize];
                        getImg = GetImage(ptrResult, i,
                                            tmpResult.plateImg, imgSize,
                                            tmpResult.plateText, numberSize);

                        if (getImg == true)
                        {
                            Image plateImg = Image.FromStream(new MemoryStream(tmpResult.plateImg));
                            tmpResult.imgFmt = plateImg.RawFormat;
                            lprResults.Add(tmpResult);
                        }
                    }
                }

                if(lprResults.Count() > 0)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }

            DeleteInstance(ptrInstance, ptrResult);

            return ret;
        }

        public static readonly byte[] PLR_LICENSE_FILE = System.Text.Encoding.ASCII.GetBytes("GVCLPRKey_Demo.gvclprkey");
    }

    public class LPRResult
    {
        public byte[] plateImg;
        public byte[] plateText;
        public ImageFormat imgFmt;
    }
}
