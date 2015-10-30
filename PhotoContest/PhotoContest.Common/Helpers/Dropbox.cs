namespace PhotoContest.Common.Helpers
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using DropNet;

    public class Dropbox
    {
        private static DropNetClient client;

        static Dropbox()
        {
            client = new DropNetClient(
                ConfigurationManager.AppSettings.Get("DropboxClientID"),
                ConfigurationManager.AppSettings.Get("DropboxSecret"),
                ConfigurationManager.AppSettings.Get("DropboxAccessToken"));
        }

        public static string Upload(string fileName, string authorName, Stream fileStream)
        {
            string fullFileName = authorName + "_" + DateTime.Now.ToString("o") + "_" + fileName;
            client.UploadFile("/", fullFileName, fileStream);
            return fullFileName;
        }

        public static string Download(string path)
        {
            return client.GetMedia(path).Url;
        }
    }
}
