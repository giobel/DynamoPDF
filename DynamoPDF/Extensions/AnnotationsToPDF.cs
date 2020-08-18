using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Autodesk.DesignScript.Geometry;

namespace DynamoPDF
{
    [Autodesk.DesignScript.Runtime.IsVisibleInDynamoLibrary(false)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class ToPDF
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {


        [Autodesk.DesignScript.Runtime.IsVisibleInDynamoLibrary(false)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static PdfAnnotation ToPDFFreeText(this string content, PdfWriter writer, float x, float y)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(x, y);
            var app = new PdfContentByte(writer);
            var anno = PdfAnnotation.CreateFreeText(writer, rect, content, app);
            return anno;
        }

        [Autodesk.DesignScript.Runtime.IsVisibleInDynamoLibrary(false)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static PDFCoords ToPDFCoords(this Point point)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return new PDFCoords(point.X, point.Y);
        }

        [Autodesk.DesignScript.Runtime.IsVisibleInDynamoLibrary(false)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static PdfAnnotation ToPDFLine(this Autodesk.DesignScript.Geometry.Line line, string content, PdfWriter writer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var start = line.StartPoint.ToPDFCoords();
            var end = line.EndPoint.ToPDFCoords();

            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(start.X, start.Y);

            var app = new PdfContentByte(writer);
            var anno = PdfAnnotation.CreateLine(writer, rect, content, start.X, start.Y, end.X, end.Y);
            return anno;
        }

        [Autodesk.DesignScript.Runtime.IsVisibleInDynamoLibrary(false)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static PdfAnnotation ToPDFPolygon(this Autodesk.DesignScript.Geometry.PolyCurve polycurve, string content, PdfWriter writer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            List<float> points = new List<float>();
            foreach (var curve in polycurve.Curves())
            {
                PDFCoords coords = curve.StartPoint.ToPDFCoords();
                points.Add(coords.X);
                points.Add(coords.Y);
            }

            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(0, 0);

            var app = new PdfContentByte(writer);
            var anno = PdfAnnotation.CreatePolygonPolyline(writer, rect, content, false, new PdfArray(points.ToArray()));
            return anno;
        }

        [Autodesk.DesignScript.Runtime.IsVisibleInDynamoLibrary(false)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static PdfAnnotation ToPDFPolygon(this Autodesk.DesignScript.Geometry.Polygon polygon, string content, PdfWriter writer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            List<float> points = new List<float>();
            foreach (var pt in polygon.Points)
            {
                PDFCoords coords = pt.ToPDFCoords();
                points.Add(coords.X);
                points.Add(coords.Y);
            }

            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(0, 0);

            var app = new PdfContentByte(writer);
            var anno = PdfAnnotation.CreatePolygonPolyline(writer, rect, content, true, new PdfArray(points.ToArray()));
            return anno;
        }

        [Autodesk.DesignScript.Runtime.IsVisibleInDynamoLibrary(false)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static PdfAnnotation ToPDFCircle(this Autodesk.DesignScript.Geometry.Circle circle, string content, PdfWriter writer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            
            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(
                (float)circle.BoundingBox.MinPoint.X, (float)circle.BoundingBox.MinPoint.Y,
                (float)circle.BoundingBox.MaxPoint.X, (float)circle.BoundingBox.MaxPoint.Y
              );

            var app = new PdfContentByte(writer);
            var anno = PdfAnnotation.CreateSquareCircle(writer, rect, content, false);
            return anno;
        }
    }


}
