using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMRando
{
    public partial class fItemSelect : Form
    {
        private static string[] ITEM_NAMES = fLogicEdit.DEFAULT_ITEM_NAMES.ToArray();

        public static void AddItem(string itemName)
        {
            var newList = ITEM_NAMES.ToList();
            newList.Add(itemName);
            ITEM_NAMES = newList.ToArray();
        }

        public static void ResetItems()
        {
            ITEM_NAMES = fLogicEdit.DEFAULT_ITEM_NAMES.ToArray();
        }

        public static List<int> ReturnItems;

        public fItemSelect(List<int> selectedItems = null, bool checkboxes = true, List<int> highlightedItems = null)
        {
            InitializeComponent();
            for (int i = 0; i < ITEM_NAMES.Length; i++)
            {
                var item = new ListViewItem(ITEM_NAMES[i]);
                item.Checked = selectedItems?.Contains(i) ?? false;
                lItems.Items.Add(item);
                if (highlightedItems != null)
                {
                    item.ForeColor = highlightedItems.Contains(i)
                        ? Color.Black
                        : Color.LightGray;
                }
            };
            this.ActiveControl = textBoxFilter;
            lItems.CheckBoxes = checkboxes;
        }

        private void bDone_Click(object sender, EventArgs e)
        {
            ReturnItems = new List<int>();
            foreach (ListViewItem l in lItems.Items)
            {
                if (l.Checked)
                {
                    ReturnItems.Add(l.Index);
                };
            };
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            var filter = textBoxFilter.Text.ToLower();
            foreach (var item in lItems.Items.Cast<ListViewItem>())
            {
                item.ForeColor = item.Text.ToLower().Contains(filter)
                    ? Color.Black
                    : Color.LightGray;
            }
        }

        private void lItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lItems.CheckBoxes)
            {
                return;
            }
            ReturnItems = lItems.SelectedIndices.Cast<int>().ToList();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
