using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace DynamoPDF
{
    /// <summary>
    /// 
    /// </summary>
    public static class CustomProperties
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdfPath"></param>
        /// <param name="propertyKey"></param>
        /// <param name="propertyValue"></param>
        public static string AddMetadata(string pdfPath, string propertyKey, string propertyValue)

        {
            try
            {
                PdfDocument document = PdfReader.Open(pdfPath);

                var properties = document.Info.Elements;

                Dictionary<string, string> paramNameValue = new Dictionary<string, string>
            {
                { propertyKey, propertyValue },

            };

                foreach (var element in paramNameValue)
                {

                    if (properties.ContainsKey("/" + element.Key))
                    {
                        properties.SetValue("/" + element.Key, new PdfString(element.Value));
                    }
                    else
                    {
                        properties.Add(new KeyValuePair<String, PdfItem>("/" + element.Key, new PdfString(element.Value)));
                    }
                }


                document.Save(pdfPath);

                document.Close();

                return $"Custom properties added to {pdfPath}";
            }
            catch
            {
                return $"Can't process file {pdfPath}";
            }
        }


        /// <summary>
        /// Extract the custom properties from the pdf file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static List<string[]> GetCustomProperties(string filepath)
        {
            if (!System.IO.File.Exists(filepath))
                throw new Exception(Properties.Resources.FileNotFoundError);

            PdfDocument document = PdfReader.Open(filepath);

            var properties = document.Info.Elements;

            List<string[]> values = new List<string[]>();

            foreach (var property in properties)
            {
                
                string p = property.Value.ToString().Remove(0, 1);
                values.Add(new string[] { property.Key.Remove(0, 1), p.Remove(p.Length - 1, 1) });
            }

            document.Close();

            return values;
        }
    }
}
