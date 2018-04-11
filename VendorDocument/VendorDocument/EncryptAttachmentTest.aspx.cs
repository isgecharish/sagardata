using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendorDocument
{
    public partial class EncryptAttachmentTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected void btnEncrypt_Click(object sender, EventArgs e)
        //{
        //    if (FileUpload.HasFile)
        //    {
        //        //Get the Input File Name and Extension.  
        //        string fileName = Path.GetFileNameWithoutExtension(FileUpload.PostedFile.FileName);
        //        string fileExtension = Path.GetExtension(FileUpload.PostedFile.FileName);

        //        //Build the File Path for the original (input) and the encrypted (output) file.  
        //        string input = Server.MapPath("~/Files/") + fileName + fileExtension;
        //        string output = Server.MapPath("~/Files/") + fileName + System.DateTime.Now.ToString("yyyy-MM-dd") + fileExtension;

        //        //Save the Input File, Encrypt it and save the encrypted file in output path.  
        //        FileUpload.SaveAs(input);
        //        this.Encrypt(input, output);
        //        Vendor objV = new Vendor();
        //        objV.Insertdata(output);
        //        //Download the Encrypted File.  
        //        //Response.ContentType = FileUpload.PostedFile.ContentType;
        //        //Response.Clear();
        //        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(output));
        //        //Response.WriteFile(output);
        //        //Response.Flush();

        //        ////Delete the original (input) and the encrypted (output) file.  
        //        //File.Delete(input);
        //        //File.Delete(output);

        //        // Response.End();
        //    }
        //}

        protected void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                int filecount = 0;
              //int fileuploadcount = 0;
                //check No of Files Selected  
                filecount = FileUpload.PostedFiles.Count();
                if (filecount >0)
                {
                    foreach (HttpPostedFile PostedFile in FileUpload.PostedFiles)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(PostedFile.FileName);
                        string fileExtension = Path.GetExtension(PostedFile.FileName);
                        try
                        {
                            FileUpload.SaveAs("\\\\192.9.200.44\\d$\\testlib1\\" + fileName + fileExtension);
                            string input = "\\\\192.9.200.44\\d$\\testlib1\\" + fileName + fileExtension;
                            //  string output = "\\\\192.9.200.44\\d$\\testlib1\\" + fileName+ System.DateTime.Now.ToString("yyyy -MM-dd")+ fileExtension;
                            // this.Encrypt(input, output);
                            Vendor objV = new Vendor();
                            objV.Insertdata(input);
                        }
                        catch (WebException ex)
                        {
                            throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
                        }
                    }
                }
            }
        }
        private void Encrypt(string inputFilePath, string outputfilePath)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                {
                    using (CryptoStream cs = new CryptoStream(fsOutput, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                        {
                            int data;
                            while ((data = fsInput.ReadByte()) != -1)
                            {
                                cs.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }
        protected void btnDecrypt_Click(object sender, EventArgs e)
        {
            Vendor objV = new Vendor();
            DataTable dt = objV.GetPAthTest();

            //Get the Input File Name and Extension  
            string fileName = System.IO.Path.GetFileNameWithoutExtension(dt.Rows[0][0].ToString());
            string fileExtension = Path.GetExtension(dt.Rows[0][0].ToString());

            //Build the File Path for the original (input) and the decrypted (output) file  
            //  string input = Server.MapPath("~/Files/") + fileName + fileExtension;
            //  string output = Server.MapPath("~/Files/") + fileName + "_dec" + fileExtension;

            string input = "\\\\192.9.200.44\\d$\\testlib1\\" + fileName + fileExtension;
            string output = "\\\\192.9.200.44\\d$\\testlib1\\" + fileName + "d" + fileExtension;

            //Save the Input File, Decrypt it and save the decrypted file in output path.  

            this.Decrypt(input, output);

            //Download the Decrypted File.  
            Response.Clear();
            //Response.ContentType = dt.Rows[0][0].ToString().co;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(output));
            Response.WriteFile(output);
            Response.Flush();

            //Delete the original (input) and the decrypted (output) file.  
            //File.Delete(input);
            //File.Delete(output);

            Response.End();
        }
        private void Decrypt(string inputFilePath, string outputfilePath)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                {
                    using (CryptoStream cs = new CryptoStream(fsInput, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                        {
                            int data;
                            while ((data = cs.ReadByte()) != -1)
                            {
                                fsOutput.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }
    }
}