using DirtyDandD.Classes;
using DirtyDandD.Handlers;
using System;
using System.Data;
using System.Windows.Forms;
using static DirtyDandD.Globals.GlobalVariables;


namespace DirtyDandD
{
    public partial class ClassSearchForm : Form
    {
        DataTable spellTable;
        MainForm main;

        public ClassSearchForm(MainForm main)
        {
            this.main = main;
            InitializeComponent();
            InitializeSpells();
        }

        private void InitializeSpells()
        {

            spellTable = new DataTable();
            spellTable.Columns.Add("Spell Name", typeof(string));
            spellTable.Columns.Add("Level", typeof(int));
            spellTable.Columns.Add("School", typeof(School));
            spellTable.Columns.Add("Ritual", typeof(bool));
            spellTable.Columns.Add("Cast Time", typeof(string));
            spellTable.Columns.Add("Range", typeof(string));
            spellTable.Columns.Add("Con.", typeof(bool));
            spellTable.Columns.Add("Duration", typeof(string));
            spellTable.Columns.Add("Source", typeof(Source));
            foreach (Spell spell in SpellHandler.GetAllSpells())
            {
                spellTable.Rows.Add(spell.spellName, spell.level, spell.school, spell.ritual, spell.GetCastTime(), spell.GetRange(), spell.concentration, spell.duration, spell.source);
            }
            dataGridViewSpells.DataSource = spellTable;
            dataGridViewSpells.ReadOnly = true;

            dataGridViewSpells.AutoResizeColumns();
            dataGridViewSpells.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewSpells.RowHeadersVisible = false;

        }

        private void UpdateTable(string search)
        {
            spellTable.Clear();
            foreach (Spell spell in SpellHandler.SpellSearch(search))
            {
                spellTable.Rows.Add(spell.spellName, spell.level, spell.school, spell.ritual, spell.GetCastTime(), spell.GetRange(), spell.concentration, spell.duration, spell.source);
            }
            dataGridViewSpells.DataSource = spellTable;
            dataGridViewSpells.ClearSelection();

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            UpdateTable(textBoxSearchBar.Text);
        }

        private void dataGridViewSpells_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridViewSpells.SelectedRows;
            if (rows.Count == 1)
                main.OpenSpellForm(SpellHandler.SpellSelect(rows[0].Cells[0].Value.ToString()));

        }

        private void textBoxSearchBar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                UpdateTable(textBoxSearchBar.Text);
        }
    }



}
