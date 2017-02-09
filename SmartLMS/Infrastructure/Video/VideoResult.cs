using System;
using System.IO;
using System.Web.Mvc;

namespace SmartLMS.Infrastructure.Video
{
    public class VideoResult : ActionResult
    {
        private string _path;
        public VideoResult(string path)
        {
            _path = path;
        }
        /// <summary> 
        /// The below method will respond with the Video file 
        /// </summary> 
        /// <param name="context"></param> 
        public override void ExecuteResult(ControllerContext context)
        {
            //The File Path 
            var videoFilePath = Path.GetFullPath(_path);
            //The header information 
            context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(_path));
            var file = new FileInfo(videoFilePath);
            //Check the file exist,  it will be written into the response 
            if (file.Exists)
            {
                var stream = file.OpenRead();
                var bytesinfile = new byte[stream.Length];
                stream.Read(bytesinfile, 0, (int)file.Length);
                context.HttpContext.Response.BinaryWrite(bytesinfile);
            }
        }
    }
}