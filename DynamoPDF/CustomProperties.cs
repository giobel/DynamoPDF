using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
