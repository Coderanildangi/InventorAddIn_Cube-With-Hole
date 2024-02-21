using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorAddIn_Assignment
{
    public class Button
    {
        public Button() { }


        private static ButtonDefinition m_buttonDefinition;
        public Inventor.Application application;


        // Method to add Button.
        public void AddButton()
        {
            application = StandardAddInServer.m_inventorApplication;
            UserInterfaceManager uiMgr = StandardAddInServer.m_inventorApplication.UserInterfaceManager;

            // Create a button definition
            m_buttonDefinition = StandardAddInServer.m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition(
                "PartToJPG", "PartToJPGCmd", CommandTypesEnum.kNonShapeEditCmdType, "{29c7d6b5-88fe-4052-9b8b-a8fd3f54df31}", "Creates part model and export its drawing to JPG file");

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
            //m_helloWorldButtonEventHandler = new EventHandler(HelloWorldButton_OnExecute);
            m_buttonDefinition.OnExecute += ButtonDefinition_OnExecute;
        }

        private void ButtonDefinition_OnExecute(NameValueMap context)
        {
            ExtrudePart extrude = new ExtrudePart();
            extrude.extrude_Part();
        }

    }
}
