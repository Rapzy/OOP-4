using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net.Http;

namespace Lab4
{
    public partial class Form1 : Form
    {
        public static class SelectedType
        {
            public static GunType Type;
            public static PropertyInfo[] Properties;
        }
        Gun selectedGun;
        List<Gun> serialization_list = new List<Gun>();
        List<Type> subclasses = new List<Type>();
        int offset = 20;
        const int margin = 40;
        public Form1()
        {
            InitializeComponent();
            comboBox1.DisplayMember = "Value";
            comboBox2.DisplayMember =  "Name";
            comboBox2.Format += (s, e) => {
                e.Value = ((Gun)e.Value).Info.Name;
            };
            FillTypeComboBox();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            object selectedItem = comboBox1.SelectedItem;
            offset = 20;
            if (selectedItem is GunType) {
                SelectedType.Type = (selectedItem as GunType);
                Type tempType = Type.GetType(SelectedType.Type.AssemblyQualifiedName);
                if (tempType != null)
                {
                    Gun tempGun = (Gun)Activator.CreateInstance(tempType);
                    SelectedType.Properties = tempGun.Info.GetType().GetProperties();
                    if (SelectedType.Properties != null)
                    {
                        foreach (PropertyInfo property in SelectedType.Properties)
                        {
                            AddField(property, panel1, 1);
                        }
                    }
                }
            }
            else
            {
                return;
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<object> input = new List<object>();
            TextBox[] textBoxes = panel1.Controls.OfType<TextBox>().ToArray();
            PropertyInfo[] properties = SelectedType.Properties;
            if (properties != null)
            {
                for (int i = 0; i < properties.Count(); i++)
                {
                    input.Add(Convert.ChangeType(textBoxes[i].Text, properties[i].PropertyType));
                }
                object[] args = input.ToArray();
                Gun new_gun = (Gun)Activator.CreateInstance(Type.GetType(SelectedType.Type.AssemblyQualifiedName), args);
                UpdateGunList();
                foreach (Control obj in panel1.Controls)
                {
                    if (obj is TextBox)
                    {
                        obj.ResetText();
                    }
                }
                UpdateInfo();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedGun is FireArm && (selectedGun.Info as FireArmInfo).Ammo > 0)
            {
                (selectedGun as FireArm).Shoot();
                UpdateInfo();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedGun is FireArm)
            {
                (selectedGun as FireArm).Reload();
                UpdateInfo();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            foreach(PropertyInfo property in selectedGun.Info.GetType().GetProperties())
            {
                string txt = panel2.Controls[GetTextBoxName(TransformPropName(property.Name), 2)].Text;
                property.SetValue(selectedGun.Info, Convert.ChangeType(txt ,property.PropertyType));
            }
            UpdateInfo();
            UpdateGunList();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            int idx = serialization_list.IndexOf(selectedGun); //index of object in @serialization_list
            if (idx == -1) //check if object already in @serialization_list
            {
                serialization_list.Add(selectedGun);
                textBox1.Text += selectedGun.Info.GetType().GetProperty("Name").GetValue(selectedGun.Info) + Environment.NewLine;
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream("Guns.dat", FileMode.OpenOrCreate);
            formatter.Serialize(fs, serialization_list.ToArray());
            fs.Close();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream("Guns.dat", FileMode.Open);
            Gun[] loaded_guns = (Gun[])formatter.Deserialize(fs);
            foreach (Gun loaded_gun in loaded_guns)
            {
                Gun.GunList.Add(loaded_gun);
            }
            UpdateGunList();
            fs.Close();
        }
        public void UpdateInfo()
        {
            selectedGun = (comboBox2.SelectedItem as Gun);
            panel2.Controls.Clear();
            offset = 20;
            PropertyInfo[] properties = selectedGun.Info.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                AddField(property, panel2, 2, property.GetValue(selectedGun.Info).ToString());
            }
        }
        public void UpdateGunList()
        {
            comboBox2.Items.Clear();
            foreach (Gun gun in Gun.GunList)
            {
                comboBox2.Items.Add(gun);
            }
            comboBox2.SelectedIndex = 0;
        }
        public class TypeInfo
        {
            public Type type;
            public ParameterInfo[] parameters;
            public TypeInfo() { }
        }
        public void AddField(PropertyInfo property, Panel parent, int id, string value="")
        {
            string name = TransformPropName(property.Name);
            TextBox txt = new TextBox();
            txt.Name = GetTextBoxName(name, id);
            txt.Height = 15;
            txt.Width = 150;
            txt.Top = offset;
            txt.Text = value;
            txt.Enabled = property.SetMethod.IsPublic;
            parent.Controls.Add(txt);

            Label lbl = new Label();
            lbl.Text = name;
            lbl.Top = offset - 15;
            parent.Controls.Add(lbl);
            offset += margin;
        }
        public string TransformPropName(string prop)
        {
            return prop.First().ToString().ToUpper() + prop.Substring(1).Replace("_", " ");
        }
        public string GetTextBoxName(string prop_name, int id = 0)
        {
            return prop_name.Replace(" ", "") + "TextBox" + id.ToString();
        }
        public void FillTypeComboBox()
        {
            comboBox1.Items.Clear();
            foreach(GunType gunType in GunType.TypeList)
            {
                comboBox1.Items.Add(gunType);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void LoadModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Assembly asm = Assembly.LoadFrom("Nade.dll");
            foreach (Type type in asm.GetTypes())
            {
                if (type.IsSubclassOf(Type.GetType("Lab4.Gun")))
                    new GunType(type.Name, type.AssemblyQualifiedName);
            }
            FillTypeComboBox();
        }
    }
}
