using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorAddIn_Assignment
{
    /// <summary>
    /// Represents a class for exporting drawings to JPG format within Autodesk Inventor.
    /// </summary>
    internal class ExportDrawingToJPG
    {
        // Constructor..
        public ExportDrawingToJPG() { }

        /// <summary>
        /// Exports the active drawing document to JPG format.
        /// </summary>
        public static void exportToJPG()
        {
            try
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

                Inventor.View view = StandardAddInServer.m_inventorApplication.ActiveView;

                // Saves Drawing to JPG file in given location.
                view.SaveAsBitmap(@"D:\Incubation\Tutorials\DrawingImage.jpg", 0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // Logging to txt file.
                Logger.LogException(ex);
            }

        }
    }
}

