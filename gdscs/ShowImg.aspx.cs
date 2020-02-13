using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace gds
{
    public partial class ShowImg : System.Web.UI.Page
    {

        const int thumbWidth = 80;
        const int thumbHeight = 80;
        bool bErr;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request.Params["t"]))
                ShowDocument(Int32.Parse(Request.Params["id"]), true);
            else
                ShowDocument(Int32.Parse(Request.Params["id"]), false);

            if (!string.IsNullOrEmpty(Request.Params["f"]))
            {
                if (Request.Params["t"] != "")
                    ShowDocument(Request.Params["f"], true);
                else
                    ShowDocument(Request.Params["f"], false);

            }

        }


        public void ShowDocument(string filename, bool showAsThumbnail) // For Upload temp Preview
        {
            string path;
            string extension;
            path = Server.MapPath(commonModule.FILETEMPVIRTUALPATH + filename);
            var fi = new System.IO.FileInfo(path);
            extension = path.Split('.')[path.Split('.').Length - 1];
            var switchExpr = extension.ToUpper();
            switch (switchExpr)
            {
                case "BMP":
                case "GIF":
                case "JPG":
                case "JPEG":
                case "JPE":
                case "ICO":
                case "PNG":
                case "TIF":
                    {
                        System.Drawing.Image img;
                        var fs = default(FileStream);
                        try
                        {
                            fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            img = System.Drawing.Image.FromStream(fs);
                            if (showAsThumbnail)
                            {
                                int w = img.Width;
                                int h = img.Height;
                                int wT, hT;
                                if (w > h)
                                {
                                    wT = w / (w / thumbWidth);
                                    hT = h / (w / thumbWidth);
                                }
                                else
                                {
                                    wT = w / (h / thumbHeight);
                                    hT = h / (h / thumbHeight);
                                }

                                img = img.GetThumbnailImage(wT, hT, null, IntPtr.Zero);
                            }

                            var switchExpr1 = extension.ToUpper();
                            switch (switchExpr1)
                            {
                                case "BMP":
                                case "PCX":
                                    {
                                        Response.ContentType = "image/bmp";
                                        Response.AddHeader("content-disposition", "inline; filename=" + filename);
                                        img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Bmp);
                                        break;
                                    }

                                case "GIF":
                                    {
                                        Response.ContentType = "image/gif";
                                        Response.AddHeader("content-disposition", "inline; filename=" + filename);
                                        img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
                                        break;
                                    }

                                case "JPG":
                                case "JPEG":
                                case "JPE":
                                    {
                                        Response.ContentType = "image/jpeg";
                                        Response.AddHeader("content-disposition", "inline; filename=" + filename);
                                        img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                        break;
                                    }

                                case "ICO":
                                    {
                                        Response.ContentType = "image/x-icon";
                                        Response.AddHeader("content-disposition", "inline; filename=" + filename);
                                        img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Icon);
                                        break;
                                    }

                                case "PNG":
                                    {
                                        Response.ContentType = "image/png";
                                        Response.AddHeader("content-disposition", "inline; filename=" + filename);
                                        img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
                                        break;
                                    }

                                case "TIF":
                                    {
                                        Response.ContentType = "image/tiff";
                                        Response.AddHeader("content-disposition", "inline; filename=" + filename);
                                        img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Tiff);
                                        break;
                                    }
                            }
                        }
                        catch (Exception ex)
                        {
                            bErr = true;
                        }
                        finally
                        {
                            fs.Close();
                            Response.Flush();
                            Response.End();
                            if (bErr)
                            {
                                Response.WriteFile(commonModule.IMGNAVIRTUALPATH);
                            }
                        }

                        break;
                    }

                default:
                    {
                        Response.WriteFile(commonModule.IMGNAVIRTUALPATH);
                        break;
                    }
            }
        }
        
        public void ShowDocument(int documentId, bool showAsThumbnail) // For General Preview
        {
            string path;
            string extension;
            string filename;
            var gdsDoc = new gdsDocuments();
            System.Drawing.Image img;
            gdsDoc.GetDocumentById(documentId);
            path = Server.MapPath(gdsDoc.Url);
            var fi = new System.IO.FileInfo(path);
            extension = path.Split('.')[path.Split('.').Length - 1];
            filename = fi.Name;
            var switchExpr = extension.ToUpper();
            switch (switchExpr)
            {
                case "BMP":
                case "GIF":
                case "JPG":
                case "JPEG":
                case "JPE":
                case "ICO":
                case "PNG":
                case "TIF":
                    {
                        var fs = default(FileStream);
                        try
                        {
                            fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            img = System.Drawing.Image.FromStream(fs);
                            if (showAsThumbnail)
                            {
                                int w = img.Width;
                                int h = img.Height;
                                int wT, hT;
                                if (w > h)
                                {
                                    wT = w / (w / thumbWidth);
                                    hT = h / (w / thumbWidth);
                                }
                                else
                                {
                                    // wT = w / (h / c)
                                    // hT = h / (h / c)
                                    wT = w / (h / thumbHeight);
                                    hT = h / (h / thumbHeight);
                                }

                                img = img.GetThumbnailImage(wT, hT, null, IntPtr.Zero);
                            }

                            var switchExpr1 = extension.ToUpper();
                            switch (switchExpr1)
                            {
                                case "BMP":
                                case "PCX":
                                    {
                                        Response.ContentType = "image/bmp";
                                        Response.AddHeader("content-disposition", "inline; filename=" + filename);
                                        img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Bmp);
                                        break;
                                    }

                                case "GIF":
                                    {
                                        Response.ContentType = "image/gif";
                                        Response.AddHeader("content-disposition", "inline; filename=" + filename);
                                        img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
                                        break;
                                    }

                                case "JPG":
                                case "JPEG":
                                case "JPE":
                                    {
                                        Response.ContentType = "image/jpeg";
                                        Response.AddHeader("content-disposition", "inline; filename=" + filename);
                                        img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                        break;
                                    }

                                case "ICO":
                                    {
                                        Response.ContentType = "image/x-icon";
                                        Response.AddHeader("content-disposition", "inline; filename=" + filename);
                                        img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Icon);
                                        break;
                                    }

                                case "PNG":
                                    {
                                        Response.ContentType = "image/png";
                                        Response.AddHeader("content-disposition", "inline; filename=" + filename);
                                        img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
                                        break;
                                    }

                                case "TIF":
                                case "TIFF":
                                    {
                                        Response.ContentType = "image/tiff";
                                        Response.AddHeader("content-disposition", "inline; filename=" + filename);
                                        img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Tiff);
                                        break;
                                    }
                            }
                        }
                        catch (Exception ex)
                        {
                            bErr = true;
                        }
                        finally
                        {
                            fs.Close();
                            Response.Flush();
                            Response.End();
                            if (bErr)
                            {
                                Response.WriteFile(commonModule.IMGNAVIRTUALPATH);
                            }
                        }

                        break;
                    }

                default:
                    {
                        Response.WriteFile(commonModule.IMGNAVIRTUALPATH);
                        break;
                    }
            }
        }

    }
}