using Inventor;
using InventorAddIn_Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorAddIn_Assignment
{
    internal class ExportDrawingToJPG
    {
        // Constructor..
        public ExportDrawingToJPG() 
        {
        }

        // Method..
        public static void exportToJPG()
        {
            StandardAddInServer.m_inventorApplication.ActiveDocument.Update();
            // Create a drawing document
            DrawingDocument drawingDoc = StandardAddInServer.m_inventorApplication.Documents.Add(DocumentTypeEnum.kDrawingDocumentObject) as DrawingDocument;

            // Get the active sheet of the drawing document
            Sheet activeSheet = drawingDoc.ActiveSheet;

            // Opening part document.
            PartDocument partDoc = (PartDocument)StandardAddInServer.m_inventorApplication.Documents.Open(@"D:\Incubation\Tutorials\newPart.ipt", false);


            // Create a base view of the part on the sheet
            DrawingView drawingView = activeSheet.DrawingViews.AddBaseView((_Document)(partDoc as Document),
                StandardAddInServer.m_inventorApplication.TransientGeometry.CreatePoint2d(10, 10),
                1,
                ViewOrientationTypeEnum.kIsoTopRightViewOrientation,
                DrawingViewStyleEnum.kHiddenLineRemovedDrawingViewStyle);


            View view = StandardAddInServer.m_inventorApplication.ActiveView;

            view.SaveAsBitmap(@"D:\Incubation\Tutorials\DrawingImage.jpg", 0, 0);

        }
    }
}

