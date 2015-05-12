using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace DifficultyEditor
{
    public partial class difficultyEditor : Form
    {
        public difficultyEditor()
        {
            InitializeComponent();
        }

        #region Aggression
        private void aggressionLabel_MouseEnter(object sender, EventArgs e)
        {
            informationLabel.Text = "The zest with which an enemy will attack you.";
            informationLabel.ForeColor = Color.Black;
        }

        private void aggressionTrackBar_MouseEnter(object sender, EventArgs e)
        {
            informationLabel.Text = "The zest with which an enemy will attack you.";
            informationLabel.ForeColor = Color.Black;
        }

        private void aggressionPercentLabel_MouseEnter(object sender, EventArgs e)
        {
            informationLabel.Text = "The zest with which an enemy will attack you.";
            informationLabel.ForeColor = Color.Black;
        }

        private void aggressionLabel_MouseLeave(object sender, EventArgs e)
        {
            informationLabel.Text = "";
        }

        private void aggressionTrackBar_MouseLeave(object sender, EventArgs e)
        {
            informationLabel.Text = "";
        }

        private void aggressionPercentLabel_MouseLeave(object sender, EventArgs e)
        {
            informationLabel.Text = "";
        }

        private void aggressionTrackBar_ValueChanged(object sender, EventArgs e)
        {
            aggressionPercentLabel.Text = (aggressionTrackBar.Value * 10) + "%";
        }
        #endregion

        #region Defense
        private void defenseLabel_MouseEnter(object sender, EventArgs e)
        {
            informationLabel.Text = "The enemy's ability to fend off oncoming attacks.";
            informationLabel.ForeColor = Color.Black;
        }

        private void defenseTrackBar_MouseEnter(object sender, EventArgs e)
        {
            informationLabel.Text = "The enemy's ability to fend off oncoming attacks.";
            informationLabel.ForeColor = Color.Black;
        }

        private void defensePercentLabel_MouseEnter(object sender, EventArgs e)
        {
            informationLabel.Text = "The enemy's ability to fend off oncoming attacks.";
            informationLabel.ForeColor = Color.Black;
        }

        private void defenseLabel_MouseLeave(object sender, EventArgs e)
        {
            informationLabel.Text = "";
        }

        private void defenseTrackBar_MouseLeave(object sender, EventArgs e)
        {
            informationLabel.Text = "";
        }

        private void defensePercentLabel_MouseLeave(object sender, EventArgs e)
        {
            informationLabel.Text = "";
        }

        private void defenseTrackBar_ValueChanged(object sender, EventArgs e)
        {
            defensePercentLabel.Text = (defenseTrackBar.Value * 10) + "%";
        }
        #endregion

        #region Abilities
        private void abilitiesLabel_MouseEnter(object sender, EventArgs e)
        {
            informationLabel.Text = "The frequency at which enemies will use their abilities.";
            informationLabel.ForeColor = Color.Black;
        }

        private void abilitiesTrackBar_MouseEnter(object sender, EventArgs e)
        {
            informationLabel.Text = "The frequency at which enemies will use their abilities.";
            informationLabel.ForeColor = Color.Black;
        }

        private void abilitiesPercentLabel_MouseEnter(object sender, EventArgs e)
        {
            informationLabel.Text = "The frequency at which enemies will use their abilities.";
            informationLabel.ForeColor = Color.Black;
        }

        private void abilitiesLabel_MouseLeave(object sender, EventArgs e)
        {
            informationLabel.Text = "";
        }

        private void abilitiesTrackBar_MouseLeave(object sender, EventArgs e)
        {
            informationLabel.Text = "";
        }

        private void abilitiesPercentLabel_MouseLeave(object sender, EventArgs e)
        {
            informationLabel.Text = "";
        }

        private void abilitiesTrackBar_ValueChanged(object sender, EventArgs e)
        {
            abilitiesPercentLabel.Text = (abilitiesTrackBar.Value * 10) + "%";
        }
        #endregion

        #region Cowardice
        private void cowardiceLabel_MouseEnter(object sender, EventArgs e)
        {
            informationLabel.Text = "The chance that an enemy will flee when their health gets low.";
            informationLabel.ForeColor = Color.Black;
        }

        private void cowardiceTrackBar_MouseEnter(object sender, EventArgs e)
        {
            informationLabel.Text = "The chance that an enemy will flee when their health gets low.";
            informationLabel.ForeColor = Color.Black;
        }

        private void cowardicePercentLabel_MouseEnter(object sender, EventArgs e)
        {
            informationLabel.Text = "The chance that an enemy will flee when their health gets low.";
            informationLabel.ForeColor = Color.Black;
        }

        private void cowardiceLabel_MouseLeave(object sender, EventArgs e)
        {
            informationLabel.Text = "";
        }

        private void cowardiceTrackBar_MouseLeave(object sender, EventArgs e)
        {
            informationLabel.Text = "";
        }

        private void cowardicePercentLabel_MouseLeave(object sender, EventArgs e)
        {
            informationLabel.Text = "";
        }

        private void cowardiceTrackBar_ValueChanged(object sender, EventArgs e)
        {
            cowardicePercentLabel.Text = (cowardiceTrackBar.Value * 10) + "%";
        }
        #endregion

        private void createButton_Click(object sender, EventArgs e)
        {
            informationLabel.Text = "Successfully created!";
            informationLabel.ForeColor = Color.Green;

            Faction characterFaction;
            if (factionButton1.Checked)
                characterFaction = Faction.Good;
            else if (factionButton2.Checked)
                characterFaction = Faction.Rich;
            else if (factionButton3.Checked)
                characterFaction = Faction.Thief;
            else
                characterFaction = Faction.Tribal;


            CustomCharacter character = new CustomCharacter(characterFaction, nameTextBox.Text, (aggressionTrackBar.Value / 20.0), (defenseTrackBar.Value / 20.0), (abilitiesTrackBar.Value / 20.0), (cowardiceTrackBar.Value / 20.0));

            //Create and write to a text file using JSON

            string json = JsonConvert.SerializeObject(character);
            System.IO.File.WriteAllText("../../../SwagSword/SwagSword/Content/CustomEnemies/" + nameTextBox.Text + "_CustomCharacter.txt", json);
        }

        private void factionButton1_CheckedChanged(object sender, EventArgs e)
        {
            Stream picStream = new FileStream("../../sprites/goodGuy.png", FileMode.Open);
            factionPic.Image = Image.FromStream(picStream);
            picStream.Close();
        }

        private void factionButton2_CheckedChanged(object sender, EventArgs e)
        {
            Stream picStream = new FileStream("../../sprites/richGuy.png", FileMode.Open);
            factionPic.Image = Image.FromStream(picStream);
            picStream.Close();
        }

        private void factionButton3_CheckedChanged(object sender, EventArgs e)
        {
            Stream picStream = new FileStream("../../sprites/banditGuy.png", FileMode.Open);
            factionPic.Image = Image.FromStream(picStream);
            picStream.Close();
        }

        private void factionButton4_CheckedChanged(object sender, EventArgs e)
        {
            Stream picStream = new FileStream("../../sprites/tribalGuy.png", FileMode.Open);
            factionPic.Image = Image.FromStream(picStream);
            picStream.Close();
        }
    }
}
