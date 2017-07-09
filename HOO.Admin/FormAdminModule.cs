using HOO.Core.Model;
using HOO.DB;
using System;
using System.Windows.Forms;

namespace HOO.Admin
{
    public partial class FormAdminModule : Form
    {
        public FormAdminModule()
        {
            InitializeComponent();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsNewWorld_Click(object sender, EventArgs e)
        {
            FormUniverseGeneration fug = new FormUniverseGeneration();
            fug.MdiParent = this;
            fug.Show();
        }

        private void tsbAddProduct_Click(object sender, EventArgs e)
        {
            MongoDBHelper m = new MongoDBHelper();
            
            //Capitol - Only one per Player. Automatically built on the first colony. Once destroyed or demolished can be built on any other planet.
            Product Capitol = new Product() { Name = "Capitol", Type = ProductType.OwnPlanetBuilding };
            Capitol.Description = "Only one per Player. Automatically built on the first colony. Once destroyed or demolished can be built on any other planet.";
            Capitol.Reqs.Add(new OAttribute() { Attribute = ObjectAttribute.Capitol, AttributeType = AttributeType.FiniteRequirement, Value = 1 });
            Capitol.Attributes.Add(new OAttribute() { Attribute = ObjectAttribute.BaseFarming, AttributeType = AttributeType.AtributeMultiplierBonus, Value = 1.2 });
            Capitol.Attributes.Add(new OAttribute() { Attribute = ObjectAttribute.BasePopulation, AttributeType = AttributeType.AtributeMultiplierBonus, Value = 1.2 });
            Capitol.Attributes.Add(new OAttribute() { Attribute = ObjectAttribute.BaseProduction, AttributeType = AttributeType.AtributeMultiplierBonus, Value = 1.2 });
            Capitol.Attributes.Add(new OAttribute() { Attribute = ObjectAttribute.BaseResearch, AttributeType = AttributeType.AtributeMultiplierBonus, Value = 1.2 });
            Capitol.Attributes.Add(new OAttribute() { Attribute = ObjectAttribute.Morale, AttributeType = AttributeType.AttributeFlatBonus, Value = 0.2 });

            //Initial Resources - Basic set of Colonists and Native Credits for the first colony.
            Product FirstColony = new Product() { Name = "Initial Resources", Type = ProductType.BaseResourceGain };
            FirstColony.Description = "Basic set of Colonists and Native Credits for the first colony.";
            FirstColony.Reqs.Add(new OAttribute() { Attribute = ObjectAttribute.DummyObject, AttributeType = AttributeType.NoDirectAccess, Value = 1 });
            FirstColony.Attributes.Add(new OAttribute() { Attribute = ObjectAttribute.BasePopulation, AttributeType = AttributeType.AttributeFlatBonus, Value = 3 });
            FirstColony.Attributes.Add(new OAttribute() { Attribute = ObjectAttribute.NativeCredits, AttributeType = AttributeType.AttributeFlatBonus, Value = 100 });
            FirstColony.Attributes.Add(new OAttribute() { Attribute = ObjectAttribute.Morale, AttributeType = AttributeType.AttributeFlatBonus, Value = 1 });

            m.AddNewProduct(Capitol);
            m.AddNewProduct(FirstColony);
        }
    }
}
