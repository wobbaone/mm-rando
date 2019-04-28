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
    public partial class fItemEdit : Form
    {
        string[] ITEM_NAMES = fLogicEdit.DEFAULT_ITEM_NAMES;

        bool updating = false;
        public static List<int> selected_items = new List<int>();

        public fItemEdit()
        {
            InitializeComponent();
            for (int i = 0; i < ITEM_NAMES.Length; i++)
            {
                lItems.Items.Add(ITEM_NAMES[i]);
            };
            tSetting.Text = "0-0-0-0";
        }

        private void fItemEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            };
        }

        private void UpdateString(List<int> s)
        {
            int[] n = new int[8];
            string[] ns = new string[8];
            for (int i = 0; i < s.Count; i++)
            {
                int j = s[i] / 32;
                int k = s[i] % 32;
                n[j] |= (int)(1 << k);
                ns[j] = Convert.ToString(n[j], 16);
            };
            tSetting.Text = ns[7] + "-" + ns[6] + "-" + ns[5] + "-" + ns[4] + "-"
                + ns[3] + "-" + ns[2] + "-" + ns[1] + "-" + ns[0];
        }

        private void UpdateChecks(string c)
        {
            selected_items = new List<int>();
            string[] v = c.Split('-');
            int[] vi = new int[8];
            for (int i = 0; i < 8; i++)
            {
                if (v[7 - i] != "")
                {
                    vi[i] = Convert.ToInt32(v[7 - i], 16);
                };
            };
            for (int i = 0; i < mmrMain.ItemNameDictionary.Count; i++)
            {
                int j = i / 32;
                int k = i % 32;
                if (((vi[j] >> k) & 1) > 0)
                {
                    selected_items.Add(i);
                };
            };
            foreach (ListViewItem l in lItems.Items)
            {
                if (selected_items.Contains(l.Index))
                {
                    l.Checked = true;
                }
                else
                {
                    l.Checked = false;
                };
            };
        }

        private void tSetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                updating = true;
                UpdateChecks(tSetting.Text);
                updating = false;
            };
        }

        private void lItems_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (updating)
            {
                return;
            };
            updating = true;
            selected_items = new List<int>();
            foreach (ListViewItem l in lItems.Items)
            {
                if (l.Checked)
                {
                    selected_items.Add(l.Index);
                };
            };
            UpdateString(selected_items);
            updating = false;
        }

    }
}
