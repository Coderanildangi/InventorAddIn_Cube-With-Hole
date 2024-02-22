using Inventor;
using InventorAddIn_Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorAddIn_Assignment
{
    /// <summary>
    /// Represents a class for extruding a part within Autodesk Inventor.
    /// </summary>
    public class ExtrudePart
    {
        // Constructor..
        public ExtrudePart()  {}

        /// <summary>
        /// Extrudes a part within Autodesk Inventor.
        /// It extrudes a cube and hole in center of one of the face.
        /// </summary>
        public void extrude_Part()
        {
            try
            {
                // Create a new part document
                PartDocument partDoc = StandardAddInServer.m_inventorApplication.Documents.Add(DocumentTypeEnum.kPartDocumentObject, StandardAddInServer.m_inventorApplication.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject)) as PartDocument;

                // Get the component definition
                PartComponentDefinition compDef = partDoc.ComponentDefinition;

                // Create a sketch
                PlanarSketch sketch = compDef.Sketches.Add(compDef.WorkPlanes[3]);

                // Draw a rectangle
                Point2d startPoint = StandardAddInServer.m_inventorApplication.TransientGeometry.CreatePoint2d(0, 0);
                Point2d endPoint = StandardAddInServer.m_inventorApplication.TransientGeometry.CreatePoint2d(10, 10);
                sketch.SketchLines.AddAsTwoPointRectangle(startPoint, endPoint);

                // Create Centerpoint of circle.
                Point2d centerPoint = StandardAddInServer.m_inventorApplication.TransientGeometry.CreatePoint2d((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);

                // Draw a circle
                sketch.SketchCircles.AddByCenterRadius(centerPoint, 2);

                // Create an extrude feature
                Profile profile = sketch.Profiles.AddForSolid();
                ExtrudeDefinition extrudeDef = compDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kNewBodyOperation);
                extrudeDef.SetDistanceExtent(10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
                ExtrudeFeature extrude = compDef.Features.ExtrudeFeatures.Add(extrudeDef);

                // Saving part model.
                partDoc.SaveAs(@"D:\Incubation\Tutorials\newPart.ipt", false);
                partDoc.Update();

                // Calling ExportToJPG Method.
                ExportDrawingToJPG.exportToJPG();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                // Logging to txt file.
                Logger.LogException(ex);
            }
            
        }
    }
}
