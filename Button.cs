using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualBasic.Compatibility.VB6;

namespace InventorAddIn_Assignment
{
    /// <summary>
    /// Represents a class for adding a custom button to the Autodesk Inventor user interface.
    /// </summary>
    public class Button
    {
        // Constructor.
        public Button() { }

        private static ButtonDefinition m_buttonDefinition;
        public Inventor.Application application;

        /// <summary>
        /// Adds a custom button to the Autodesk Inventor user interface.
        /// </summary>
        public void AddButton()
        {
            try
            {
                application = StandardAddInServer.m_inventorApplication;
                UserInterfaceManager uiMgr = StandardAddInServer.m_inventorApplication.UserInterfaceManager;

                //load image icons for UI items
                string filename = @"../../Resources/icon.ico";

                Icon commandIcon = new Icon(filename);

                // Standard Icon.
                Icon standardIcon = new Icon(commandIcon, 16, 16);

                // Large Icon.
                Icon largeIcon = new Icon(commandIcon, 32, 32);

                //Image icon = Image.FromFile(@"../../Resources/cube1.png");
                stdole.IPictureDisp standardIconIPictureDisp;
                standardIconIPictureDisp = (stdole.IPictureDisp)Support.IconToIPicture(standardIcon);

                stdole.IPictureDisp largeIconIPictureDisp;
                largeIconIPictureDisp = (stdole.IPictureDisp)Support.IconToIPicture(largeIcon);

                // Create a button definition
                m_buttonDefinition = StandardAddInServer.m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition(
                    "PartToJPG", 
                    "PartToJPGCmd", 
                    CommandTypesEnum.kNonShapeEditCmdType, 
                    "{29c7d6b5-88fe-4052-9b8b-a8fd3f54df31}", 
                    "Creates part model and export its drawing to JPG file", 
                    "Creates cube with hole" , 
                    standardIconIPictureDisp, 
                    largeIconIPictureDisp, 
                    ButtonDisplayEnum.kDisplayTextInLearningMode);

                // Get the Ribbon from the mentioned Document.
                Ribbon toolsRibbon = uiMgr.Ribbons["Part"];

                //Get the tools tab
                RibbonTab toolsTab = toolsRibbon.RibbonTabs["id_TabTools"];

                // Get the tools panel within the tools tab
                RibbonPanel toolsPanel = toolsTab.RibbonPanels["id_PanelP_ShowPanels"];

                // Add the button to the panel
                CommandControl commandControl = toolsPanel.CommandControls.AddButton(
                    m_buttonDefinition,
                    false,
                    true,
                    "",
                    false);

                // Wire up the event handler
                m_buttonDefinition.OnExecute += ButtonDefinition_OnExecute;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                // Logging to txt file.
                Logger.LogException(ex);
            }
            
        }

        /// <summary>
        /// Event handler for when the custom button is executed.
        /// </summary>
        /// <param name="context">The context in which the event was triggered.</param>
        private void ButtonDefinition_OnExecute(NameValueMap context)
        {
            ExtrudePart extrude = new ExtrudePart();
            extrude.extrude_Part();
        }

    }
}
